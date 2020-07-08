import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import moment from 'moment'
import { Link } from 'react-router-dom'
import apiRoutes from '../../routes/apis'
import axios from 'axios'
import { debounce } from 'debounce'
import exportFromJSON from 'export-from-json'
import SinhVienForPrint from './SinhVienForPrint'
import Excel from 'exceljs'
import { saveAs } from 'file-saver'
import { excelFormat } from '../../utils'
import { formatDateString } from '../../utils'

const csvjson = require('csvjson')
const PageSizes = [10, 25, 50, 100, 250, 500, 1000, 2500, 5000]
const ExportDataOptions = [
    {
        id: 'CURRENT_PAGE',
        value: 'Dữ liệu của trang hiện tại'
    },
    {
        id: 'ALL_PAGE',
        value: 'Dữ liệu của tất cả các trang'
    },
    {
        id: 'ALL_DATA',
        value: 'Dữ liệu hiện có trong hệ thống'
    }
]

class SinhVienForList extends Component {
    constructor (props) {
      super(props)
      this.state = {
          data: [],
          sortField: 'ThoiGianTao',
          sortOrder: 'DESC',
          pageSize: 10,
          pageNumber: 1,
          totalPages: 1,
          totalItems: 0,
          hasNextPage: false,
          keyword: '',
          status: 0,
          showPopup: {
            temporarilyDelete: false,
            permanentlyDelete: false,
            restore: false,
            import: false,
            export: false,
            exportDocument: false
          },
          statusStatistics: {
            all: 0,
            active: 0,
            inactive: 0
          },
          activeRecord: {},
          confirmationValue: '',
          validConfirmationValue: false,
          fileTypeToExport: '',
          dataToExport: [],
          dataSourceToExport: ExportDataOptions[0].id,
          fileNameToExport: moment().format('DD-MM-YYYY'),
          fileToImport: null,
          fileTypeToExportDocument: '',
          dataToExportDocument: [],
          dataSourceToExportDocument: ExportDataOptions[0].id,
          fileNameToExportDocument: moment().format('DD-MM-YYYY'),
          fileTypeToImport: '',
          dataToImport: [],
          showPopupErrorMessage: false
      }

      this.uploadFileToImportDialog = React.createRef()
      this.fetchData = debounce(this.fetchData, 50)
      this.fileReader = new FileReader()
      this.fileReader.onload = (e) => {
        try {
            const fileType = this.state.fileTypeToImport.toUpperCase()
            const fieldToParseNumber = ['trangThai']
            let data
    
            switch (fileType) {
                case 'JSON':
                    data = JSON.parse(e.target.result)
                    break
    
                case 'CSV':
                    data = csvjson.toObject(e.target.result, { delimiter: ',', quote: '"' })
                    break
            }
    
            data = data.map(record => {
                fieldToParseNumber.forEach(field => {
                    record[field] = Number(record[field])
                })
                
                return record
            })
    
            this.setState({ dataToImport: data }, this.createMultiple)
        }
        catch (error) {
            this.setState({ showPopupErrorMessage: true })
        }
      }
    }

    componentDidMount() {
        window.scrollTo(0, 0)
        axios.defaults.headers.common['Authorization'] = 'AUTH_TOKEN'
        this.fetchData()
        this.fetchSettings()
    }

    formRequestParams() {
        let params = {}
        const { keyword, sortField, sortOrder, pageNumber, pageSize, status } = this.state

        params['keyword'] = keyword
        params['sortField'] = sortField
        params['sortOrder'] = sortOrder
        params['pageNumber'] = pageNumber
        params['pageSize'] = pageSize
        params['trangThai'] = status

        return params
    }

    async createMultiple() {
        const url = apiRoutes.sinhVien.createMultiple
        const data = this.state.dataToImport

        return axios.post(url, data)
        .then(response => {
            this.closePopup('import')
            this.refresh()
        })
        .catch(error => {
            this.setState({ showPopupErrorMessage: true })
            console.log(error)
        })
    }

    async fetchData() {
        const url = apiRoutes.sinhVien.getAll
        const params = this.formRequestParams()

        try {
            const response = await axios.get(url, { params })

            if (response && response.data.status === 'SUCCESS') {
                const { data, totalItems, totalPages, statusStatistics } = response.data.result

                this.setState({
                    totalItems,
                    totalPages,
                    data,
                    statusStatistics,
                    dataToExportDocument: data
                })
            }
            else {
                throw new Error(response.errors)
            }
        }
        catch (error) {
            console.error(error)
        }
    }

    async fetchSettings() {
        const url = apiRoutes.caiDat.getAll

        try {
            const response = await axios.get(url)

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

                this.setState({
                    settings: data
                })
            }
            else {
                throw new Error(response.errors)
            }
        }
        catch (error) {
            console.error(error)
        }
    }

    refresh() {
        this.setState({
            sortField: 'ThoiGianTao',
            sortOrder: 'DESC',
            pageNumber: 1,
            pageSize: 10,
            keyword: '',
            status: 0
        }, this.fetchData)
    }

    getCurrentStatusText(statusCode) {
        switch (parseInt(statusCode)) {
            case -1:
                return 'Đã bị xóa'

            case 1:
                return 'Đang hiển thị'
        }
    }

    getCurrentStatusColors(statusCode) {
        switch (parseInt(statusCode)) {
            case -1:
                return {
                    backgroundColor: 'lightgray',
                    color: 'gray'
                }
            
            case 1:
                return {
                    backgroundColor: '#0BBE51',
                    color: '#fff'
                }
        }
    }

    formatDateString(dateString) {
        return moment(dateString).format('hh:mm, DD-MM-YYYY')
    }

    sortByColumn(columnName) {
        let { sortField, sortOrder } = this.state

        if (sortField !== columnName) {
            sortField = columnName
            sortOrder = 'ASC'
        }
        else {
            if (sortOrder === 'ASC') {
                sortOrder = 'DESC'
            }
            else {
                sortOrder = 'ASC'
            }
        }

        this.setState({ sortField, sortOrder }, this.fetchData)
    }

    isBeingSorted(columnName) {
        return this.state.sortField === columnName
    }

    changeKeyword(e) {
        const keyword = e.target.value
        this.setState({ keyword }, this.fetchData)
    }

    changePageSize(e) {
        const pageSize = parseInt(e.target.value)
        this.setState({ pageSize, pageNumber: 1 }, this.fetchData)
    }

    changePageNumber(e) {
        const pageNumber = parseInt(e.target.value) || 1
        this.setState({ pageNumber }, this.fetchData)
    }

    changeStatus(status) {
        this.setState({ status }, this.fetchData)
    }

    navigateToNextPage() {
        let pageNumber = this.state.pageNumber
        let totalPages = this.state.totalPages

        if (pageNumber < totalPages) {
            pageNumber++
        }

        this.setState({ pageNumber }, this.fetchData)
    }

    navigateToPreviousPage() {
        let pageNumber = this.state.pageNumber

        if (pageNumber > 1) {
            pageNumber--
        }
        
        this.setState({ pageNumber }, this.fetchData)
    }

    showPopup(name, activeRecord = {}) {
        const showPopup = { ...this.state.showPopup }
        showPopup[name] = true

        this.setState({ showPopup, activeRecord })
    }

    closePopup(name) {
        const showPopup = { ...this.state.showPopup }
        showPopup[name] = false

        this.setState({ 
            showPopup, 
            activeRecord: {}, 
            confirmationValue: '', 
            validConfirmationValue: false,
            fileTypeToExport: '',
            fileToImport: null,
            dataToExport: [],
            dataSourceToExport: ExportDataOptions[0].id,
            fileNameToExport: moment().format('DD-MM-YYYY'),
            fileToImport: null,
            fileTypeToExportDocument: '',
            dataToExportDocument: [],
            dataSourceToExportDocument: ExportDataOptions[0].id,
            fileNameToExportDocument: moment().format('DD-MM-YYYY'),
            dataSourceToImport: [],
            fileTypeToImport: '',
            dataToImport: [],
            showPopupErrorMessage: false
        })
    }

    async onTemporarilyDelete(id) {
        try {
            const url = apiRoutes.sinhVien.temporarilyDeleteById + '/' + id
            const data = { trangThai: -1 }

            const response = await axios.put(url, data)

            if (response && response.data.status === 'SUCCESS') {
                this.closePopup('temporarilyDelete')
                this.refresh()
            }
            else {
                this.setState({ showPopupErrorMessage: true })
                throw new Error(response.errors)
            }
        }
        catch (error) {
            this.setState({ showPopupErrorMessage: true })
            console.error(error)
        }
    }

    async onPermanentlyDelete(id) {
        try {
            const url = apiRoutes.sinhVien.permanentlyDeleteById + '/' + id

            const response = await axios.delete(url)

            if (response && response.data.status === 'SUCCESS') {
                this.closePopup('permanentlyDelete')
                this.refresh()
            }
            else {
                this.setState({ showPopupErrorMessage: true })
                throw new Error(response.errors)
            }
        }
        catch (error) {
            this.setState({ showPopupErrorMessage: true })
            console.error(error)
        }
    }

    async onRestore(id) {
        try {
            const url = apiRoutes.sinhVien.restoreById + '/' + id
            const data = { trangThai: 1 }

            const response = await axios.put(url, data)

            if (response && response.data.status === 'SUCCESS') {
                this.closePopup('restore')
                this.refresh()
            }
            else {
                this.setState({ showPopupErrorMessage: true })
                throw new Error(response.errors)
            }
        }
        catch (error) {
            this.setState({ showPopupErrorMessage: true })
            console.error(error)
        }
    }

    changeConfirmationValue(e) {
        const confirmationValue = e.target.value.toString()
        const acceptedValue = this.state.activeRecord.maSinhVien.toString()

        if (confirmationValue === acceptedValue) {
            this.setState({ confirmationValue, validConfirmationValue: true })
        }
        else {
            this.setState({ confirmationValue, validConfirmationValue: false })
        }
    }

    changeFileTypeToExport(fileTypeToExport) {
        if (this.state.fileTypeToExport === fileTypeToExport) {
            this.setState({ fileTypeToExport: '' })
        }
        else {
            this.setState({ 
                fileTypeToExport,
                dataToExport: [],
                fileNameToExport: moment().format('DD-MM-YYYY'),
            })
        }
    }

    changeFileTypeToExportDocument(fileTypeToExportDocument) {
        if (this.state.fileTypeToExportDocument === fileTypeToExportDocument) {
            this.setState({ fileTypeToExportDocument: '' })
        }
        else {
            this.setState({ 
                fileTypeToExportDocument,
                dataToExportDocument: [],
                fileNameToExportDocument: moment().format('DD-MM-YYYY'),
            })
        }
    }

    changeFileToImport(e) {
        this.setState({
            fileTypeToImport: e.target.files[0].name.split('.')[1]
        })
        this.fileReader.readAsText(e.target.files[0])
    }

    showImportDialog() {
        this.uploadFileToImportDialog.click()
    }

    changeFileNameToExport(e) {
        const fileNameToExport = e.target.value
        this.setState({ fileNameToExport })
    }

    changeFileNameToExportDocument(e) {
        const fileNameToExportDocument = e.target.value
        this.setState({ fileNameToExportDocument })
    }

    changeDataSourceToExport(e) {
        const dataSourceToExport = e.target.value
        this.setState({ dataSourceToExport })
    }

    changeDataSourceToExportDocument(e) {
        const dataSourceToExportDocument = e.target.value
        this.setState({ dataSourceToExportDocument })
    }

    async exportData() {
        const fileName = this.state.fileNameToExport
        const exportType = this.state.fileTypeToExport
        const dataSourceToExport = this.state.dataSourceToExport
        let dataToExport = []

        switch (dataSourceToExport) {
            case 'CURRENT_PAGE': {
                dataToExport = this.state.data
                dataToExport = dataToExport.map(record => {
                    return {
                        maSinhVien: record.maSinhVien,
                        hoVaTen: record.hoVaTen,
                        maLop: record.lop.maLop,
                        gioiTinh: record.gioiTinh,
                        ngaySinh: record.ngaySinh,
                        email: record.email,
                        queQuan: record.queQuan,
                        diaChi: record.diaChi,
                        soDienThoai: record.soDienThoai,
                        thoiGianTao: record.thoiGianTao,
                        trangThai: record.trangThai,
                        maDoAn: record.maDoAn
                    }
                })
                break
            }

            case 'ALL_PAGE': {
                const url = apiRoutes.sinhVien.getAll
                const params = this.formRequestParams()
                params['pageSize'] = 1000
        
                try {
                    const response = await axios.get(url, { params })
        
                    if (response && response.data.status === 'SUCCESS') {
                        dataToExport = response.data.result.data
                        dataToExport = dataToExport.map(record => {
                            return {
                                maSinhVien: record.maSinhVien,
                                hoVaTen: record.hoVaTen,
                                maLop: record.lop.maLop,
                                gioiTinh: record.gioiTinh,
                                ngaySinh: record.ngaySinh,
                                email: record.email,
                                queQuan: record.queQuan,
                                diaChi: record.diaChi,
                                soDienThoai: record.soDienThoai,
                                thoiGianTao: record.thoiGianTao,
                                trangThai: record.trangThai,
                                maDoAn: record.maDoAn
                            }
                        })
                    }
                    else {
                        throw new Error(response.errors)
                    }
                }
                catch (error) {
                    console.error(error)
                }
                break
            }

            case 'ALL_DATA': {
                const url = apiRoutes.sinhVien.getAll
                const params = {}
                params['pageSize'] = this.state.totalItems
        
                try {
                    const response = await axios.get(url, { params })
        
                    if (response && response.data.status === 'SUCCESS') {
                        dataToExport = response.data.result.data
                        dataToExport = dataToExport.map(record => {
                            return {
                                maSinhVien: record.maSinhVien,
                                hoVaTen: record.hoVaTen,
                                maLop: record.lop.maLop,
                                gioiTinh: record.gioiTinh,
                                ngaySinh: record.ngaySinh,
                                email: record.email,
                                queQuan: record.queQuan,
                                diaChi: record.diaChi,
                                soDienThoai: record.soDienThoai,
                                thoiGianTao: record.thoiGianTao,
                                trangThai: record.trangThai,
                                maDoAn: record.maDoAn
                            }
                        })
                    }
                    else {
                        throw new Error(response.errors)
                    }
                }
                catch (error) {
                    console.error(error)
                }
                break
            }
        }

        if (dataToExport.length > 0) {
            await exportFromJSON({ data: dataToExport, fileName, exportType })
            this.closePopup('export')
        }
        else {
            this.setState({ showPopupErrorMessage: true })
        }
    }

    exportDocumentAsXlsx() {
        let workbook = new Excel.Workbook()
        let worksheet = workbook.addWorksheet(moment().format('DD-MM-YYYY'), {
            pageSetup: { fitToPage: true, orientation:'landscape' }
        })
        let currentRowCount = 7
        let self = this

        workbook.Props = {
            Title: this.state.fileNameToExportDocument,
            Subject: this.state.fileNameToExportDocument,
            Author: 'GTMS',
            CreatedDate: moment()
        }

        worksheet.mergeCells('A1:G1')
        worksheet.getCell('A1').value = this.state.settings.tenDonViChuQuan.toUpperCase()
        worksheet.getCell('A1').font = excelFormat.boldFont
        worksheet.getCell('A1').alignment = excelFormat.center

        worksheet.mergeCells('A2:G2')
        worksheet.getCell('A2').value = this.state.settings.tenKhoa.toUpperCase()
        worksheet.getCell('A2').font = excelFormat.boldFont
        worksheet.getCell('A2').alignment = excelFormat.center

        worksheet.mergeCells('N1:T1')
        worksheet.getCell('N1').value = 'CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM'
        worksheet.getCell('N1').font = excelFormat.boldFont
        worksheet.getCell('N1').alignment = excelFormat.center

        worksheet.mergeCells('N2:T2')
        worksheet.getCell('N2').value = 'Độc lập - Tự do - Hạnh phúc'
        worksheet.getCell('N2').font = excelFormat.boldFont
        worksheet.getCell('N2').alignment = excelFormat.center

        worksheet.mergeCells('A4:T4')
        worksheet.getCell('A4').value = 'DANH SÁCH CÁC SINH VIÊN HIỆN CÓ TRONG HỆ THỐNG'
        worksheet.getCell('A4').font = { ...excelFormat.boldFont, size: 18 }
        worksheet.getCell('A4').alignment = excelFormat.center

        worksheet.mergeCells('A5:T5')
        worksheet.getCell('A5').value = 'Graduation Thesis Management System (GTMS)'
        worksheet.getCell('A5').font = excelFormat.boldFont
        worksheet.getCell('A5').alignment = excelFormat.center

        worksheet.getCell('A7').value = 'STT'
        worksheet.getCell('A7').font = excelFormat.boldFont
        worksheet.getCell('A7').alignment = excelFormat.center
        worksheet.getCell('A7').border = excelFormat.border

        worksheet.mergeCells('B7:C7')
        worksheet.getCell('B7').value = 'MSSV'
        worksheet.getCell('B7').font = excelFormat.boldFont
        worksheet.getCell('B7').alignment = excelFormat.center
        worksheet.getCell('B7').border = excelFormat.border

        worksheet.mergeCells('D7:F7')
        worksheet.getCell('D7').value = 'Họ và tên'
        worksheet.getCell('D7').font = excelFormat.boldFont
        worksheet.getCell('D7').alignment = excelFormat.center
        worksheet.getCell('D7').border = excelFormat.border

        worksheet.mergeCells('G7:H7')
        worksheet.getCell('G7').value = 'Giới tính'
        worksheet.getCell('G7').font = excelFormat.boldFont
        worksheet.getCell('G7').alignment = excelFormat.center
        worksheet.getCell('G7').border = excelFormat.border

        worksheet.mergeCells('I7:J7')
        worksheet.getCell('I7').value = 'Ngày sinh'
        worksheet.getCell('I7').font = excelFormat.boldFont
        worksheet.getCell('I7').alignment = excelFormat.center
        worksheet.getCell('I7').border = excelFormat.border

        worksheet.mergeCells('K7:L7')
        worksheet.getCell('K7').value = 'Lớp'
        worksheet.getCell('K7').font = excelFormat.boldFont
        worksheet.getCell('K7').alignment = excelFormat.center
        worksheet.getCell('K7').border = excelFormat.border

        worksheet.mergeCells('M7:N7')
        worksheet.getCell('M7').value = 'Khoa'
        worksheet.getCell('M7').font = excelFormat.boldFont
        worksheet.getCell('M7').alignment = excelFormat.center
        worksheet.getCell('M7').border = excelFormat.border

        worksheet.mergeCells('O7:Q7')
        worksheet.getCell('O7').value = 'Khóa đào tạo'
        worksheet.getCell('O7').font = excelFormat.boldFont
        worksheet.getCell('O7').alignment = excelFormat.center
        worksheet.getCell('O7').border = excelFormat.border

        worksheet.mergeCells('R7:T7')
        worksheet.getCell('R7').value = 'Hệ đào tạo'
        worksheet.getCell('R7').font = excelFormat.boldFont
        worksheet.getCell('R7').alignment = excelFormat.center
        worksheet.getCell('R7').border = excelFormat.border

        this.state.dataToExportDocument.forEach((record, index) => {
            const currentRowIndex = 8 + index
            currentRowCount++

            worksheet.getCell('A' + currentRowIndex).value = index + 1
            worksheet.getCell('A' + currentRowIndex).font = excelFormat.normalFont
            worksheet.getCell('A' + currentRowIndex).alignment = excelFormat.center
            worksheet.getCell('A' + currentRowIndex).border = excelFormat.border

            worksheet.mergeCells('B' + currentRowCount + ':C' + currentRowCount)
            worksheet.getCell('B' + currentRowIndex).value = record.maSinhVien
            worksheet.getCell('B' + currentRowIndex).font = excelFormat.normalFont
            worksheet.getCell('B' + currentRowIndex).alignment = excelFormat.left
            worksheet.getCell('B' + currentRowIndex).border = excelFormat.border

            worksheet.mergeCells('D' + currentRowCount + ':F' + currentRowCount)
            worksheet.getCell('D' + currentRowIndex).value = record.hoVaTen
            worksheet.getCell('D' + currentRowIndex).font = excelFormat.normalFont
            worksheet.getCell('D' + currentRowIndex).alignment = excelFormat.left
            worksheet.getCell('D' + currentRowIndex).border = excelFormat.border

            worksheet.mergeCells('G' + currentRowCount + ':H' + currentRowCount)
            worksheet.getCell('G' + currentRowIndex).value = record.gioiTinh
            worksheet.getCell('G' + currentRowIndex).font = excelFormat.normalFont
            worksheet.getCell('G' + currentRowIndex).alignment = excelFormat.left
            worksheet.getCell('G' + currentRowIndex).border = excelFormat.border

            worksheet.mergeCells('I' + currentRowCount + ':J' + currentRowCount)
            worksheet.getCell('I' + currentRowIndex).value = formatDateString(record.ngaySinh, false)
            worksheet.getCell('I' + currentRowIndex).font = excelFormat.normalFont
            worksheet.getCell('I' + currentRowIndex).alignment = excelFormat.left
            worksheet.getCell('I' + currentRowIndex).border = excelFormat.border

            worksheet.mergeCells('K' + currentRowCount + ':L' + currentRowCount)
            worksheet.getCell('K' + currentRowIndex).value = record.lop.tenLop
            worksheet.getCell('K' + currentRowIndex).font = excelFormat.normalFont
            worksheet.getCell('K' + currentRowIndex).alignment = excelFormat.left
            worksheet.getCell('K' + currentRowIndex).border = excelFormat.border

            worksheet.mergeCells('M' + currentRowCount + ':N' + currentRowCount)
            worksheet.getCell('M' + currentRowIndex).value = record.lop.khoa.tenKhoa
            worksheet.getCell('M' + currentRowIndex).font = excelFormat.normalFont
            worksheet.getCell('M' + currentRowIndex).alignment = excelFormat.left
            worksheet.getCell('M' + currentRowIndex).border = excelFormat.border

            worksheet.mergeCells('O' + currentRowCount + ':Q' + currentRowCount)
            worksheet.getCell('O' + currentRowIndex).value = record.lop.khoaDaoTao.tenKhoaDaoTao
            worksheet.getCell('O' + currentRowIndex).font = excelFormat.normalFont
            worksheet.getCell('O' + currentRowIndex).alignment = excelFormat.left
            worksheet.getCell('O' + currentRowIndex).border = excelFormat.border

            worksheet.mergeCells('R' + currentRowCount + ':T' + currentRowCount)
            worksheet.getCell('R' + currentRowIndex).value = record.lop.heDaoTao
            worksheet.getCell('R' + currentRowIndex).font = excelFormat.normalFont
            worksheet.getCell('R' + currentRowIndex).alignment = excelFormat.left
            worksheet.getCell('R' + currentRowIndex).border = excelFormat.border
        })

        worksheet.mergeCells('A' + (currentRowCount + 2) + ':G' + (currentRowCount + 2))
        worksheet.getCell('A' + (currentRowCount + 2)).value = `Danh sách có tất cả ${ this.state.dataToExportDocument.length } kết quả`
        worksheet.getCell('A' + (currentRowCount + 2)).font = excelFormat.italicFont
        worksheet.getCell('A' + (currentRowCount + 2)).alignment = excelFormat.left

        worksheet.mergeCells('N' + (currentRowCount + 2) + ':T' + (currentRowCount + 2))
        worksheet.getCell('N' + (currentRowCount + 2)).value = `Thành phố Hồ Chí Minh, ngày ${ moment().format('DD') } tháng ${ moment().format('MM') } năm ${ moment().format('YYYY') }`
        worksheet.getCell('N' + (currentRowCount + 2)).font = excelFormat.italicFont
        worksheet.getCell('N' + (currentRowCount + 2)).alignment = excelFormat.right

        worksheet.mergeCells('B' + (currentRowCount + 4) + ':D' + (currentRowCount + 4))
        worksheet.getCell('B' + (currentRowCount + 4)).value = `NGƯỜI DUYỆT`
        worksheet.getCell('B' + (currentRowCount + 4)).font = excelFormat.boldFont
        worksheet.getCell('B' + (currentRowCount + 4)).alignment = excelFormat.center

        worksheet.mergeCells('P' + (currentRowCount + 4) + ':S' + (currentRowCount + 4))
        worksheet.getCell('P' + (currentRowCount + 4)).value = `NGƯỜI LẬP`
        worksheet.getCell('P' + (currentRowCount + 4)).font = excelFormat.boldFont
        worksheet.getCell('P' + (currentRowCount + 4)).alignment = excelFormat.center

        worksheet.mergeCells('A' + (currentRowCount + 5) + ':E' + (currentRowCount + 5))
        worksheet.getCell('A' + (currentRowCount + 5)).value = `(Ký và ghi rõ họ tên)`
        worksheet.getCell('A' + (currentRowCount + 5)).font = excelFormat.italicFont
        worksheet.getCell('A' + (currentRowCount + 5)).alignment = excelFormat.center

        worksheet.mergeCells('O' + (currentRowCount + 5) + ':T' + (currentRowCount + 5))
        worksheet.getCell('O' + (currentRowCount + 5)).value = `(Ký và ghi rõ họ tên)`
        worksheet.getCell('O' + (currentRowCount + 5)).font = excelFormat.italicFont
        worksheet.getCell('O' + (currentRowCount + 5)).alignment = excelFormat.center

        if (this.state.dataToExportDocument.length > 0) {
            var buff = workbook.xlsx.writeBuffer().then(function (data) {
                var blob = new Blob([data], {type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"})
                saveAs(blob, self.state.fileNameToExportDocument)
            })
        }
        else {
            this.setState({ showPopupErrorMessage: true })
        }
    }

    async exportDocument() {
        const fileName = this.state.fileNameToExportDocument
        const exportType = this.state.fileTypeToExportDocument.toUpperCase()
        const dataSourceToExport = this.state.dataSourceToExportDocument
        let data = []

        switch (dataSourceToExport) {
            case 'CURRENT_PAGE': {
                const url = apiRoutes.sinhVien.getAll
                const params = this.formRequestParams()
                params['trangThai'] = 1
        
                try {
                    const response = await axios.get(url, { params })
        
                    if (response && response.data.status === 'SUCCESS') {
                        data = response.data.result.data
                    }
                    else {
                        throw new Error(response.errors)
                    }
                }
                catch (error) {
                    console.error(error)
                }
                break
            }

            case 'ALL_PAGE': {
                const url = apiRoutes.sinhVien.getAll
                const params = this.formRequestParams()
                params['pageSize'] = 1000
                params['trangThai'] = 1
        
                try {
                    const response = await axios.get(url, { params })
        
                    if (response && response.data.status === 'SUCCESS') {
                        data = response.data.result.data
                    }
                    else {
                        throw new Error(response.errors)
                    }
                }
                catch (error) {
                    console.error(error)
                }
                break
            }

            case 'ALL_DATA': {
                const url = apiRoutes.sinhVien.getAll
                const params = {}
                params['pageSize'] = this.state.totalItems
                params['trangThai'] = 1
        
                try {
                    const response = await axios.get(url, { params })
        
                    if (response && response.data.status === 'SUCCESS') {
                        data = response.data.result.data
                    }
                    else {
                        throw new Error(response.errors)
                    }
                }
                catch (error) {
                    console.error(error)
                }
                break
            }
        }

        if (data.length > 0) {
            this.setState({ dataToExportDocument: data }, () => {
                this.closePopup('exportDocument')
    
                switch (exportType) {
                    case 'PDF':
                        window.print()
                        break
    
                    case 'XLSX':
                        this.exportDocumentAsXlsx()
                        break
                }
            })
        }
        else {
            this.setState({ showPopupErrorMessage: true })
        }
    }

    render() {
        let pageNumbers = []

        for(let i = 1; i <= this.state.totalPages; i++) {
            pageNumbers.push(i)
        }

        return (
            <Fragment>
                <section className='breadcrumbs'>
                    <span className='breadcrumb-home'><Link to='/'>Graduation Thesis Management System (GTMS)</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='/sinh-vien'>Quản lý sinh viên</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Danh sách sinh viên</h3>
                            <p className='section__subtitle'>Danh sách tất cả các sinh viên hiện đang được quản lý</p>
                        </div>

                        <div className='section__header-right'>
                            <span className='button' onClick={ () => this.refresh() }>
                                <i className='fas fa-redo'></i>&nbsp;&nbsp;Tải lại
                            </span>
                            <span className='button'>
                                <Link to='/sinh-vien/them-moi'>
                                    <i className='fas fa-plus-circle'></i>&nbsp;&nbsp;Thêm mới
                                </Link>
                            </span>
                            <span className='button' onClick={ () => this.showPopup('import') }>
                                <i className='fas fa-file-import'></i>&nbsp;&nbsp;Nhập dữ liệu
                            </span>
                            { this.state.data.length !== 0 && <span className='button' onClick={ () => this.showPopup('export') }>
                                <i className='fas fa-file-export'></i>&nbsp;&nbsp;Xuất dữ liệu
                            </span> }
                            { this.state.data.length !== 0 && <span className='button' onClick={ () => this.showPopup('exportDocument') }>
                                <i className='fas fa-file-export'></i>&nbsp;&nbsp;Xuất báo cáo
                            </span> }
                        </div>
                    </header>

                    <div className='section__body'>
                        <div className='section-body-main'>
                            <div className='section-body-toolbar'>
                                <div className='search-box__wrapper'>
                                    <span className='search-button'><i className='fas fa-search'></i></span>
                                    <input type='text' className='search-box' placeholder='Nhập từ khóa cần tìm kiếm' value={ this.state.keyword } onChange={ e => this.changeKeyword(e) } />
                                </div>
                                <div className='table-tabs'>
                                    <span className={ this.state.status === 0 ? 'table-tab-active' : 'table-tab' } onClick={ () => this.changeStatus(0) }>Tất cả ({ this.state.statusStatistics.all })</span>
                                    <span className={ this.state.status === 1 ? 'table-tab-active' : 'table-tab' } onClick={ () => this.changeStatus(1) }>Đang hiển thị ({ this.state.statusStatistics.active })</span>
                                    <span className={ this.state.status === -1 ? 'table-tab-active' : 'table-tab' } onClick={ () => this.changeStatus(-1) }>Đã xóa ({ this.state.statusStatistics.inactive })</span>
                                </div>
                            </div>

                            <table className='table'>
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th onClick={ () => this.sortByColumn('MaSinhVien') }>{ this.isBeingSorted('MaSinhVien') && <i className='fas fa-sort sort-button'></i> } Mã sinh viên</th>
                                        <th onClick={ () => this.sortByColumn('HoVaTen') }>{ this.isBeingSorted('HoVaTen') && <i className='fas fa-sort sort-button'></i> } Họ và tên</th>
                                        <th onClick={ () => this.sortByColumn('GioiTinh') }>{ this.isBeingSorted('GioiTinh') && <i className='fas fa-sort sort-button'></i> } Giới tính</th>
                                        <th onClick={ () => this.sortByColumn('NgaySinh') }>{ this.isBeingSorted('NgaySinh') && <i className='fas fa-sort sort-button'></i> } Ngày sinh</th>
                                        <th onClick={ () => this.sortByColumn('MaLop') }>{ this.isBeingSorted('MaLop') && <i className='fas fa-sort sort-button'></i> } Lớp</th>
                                        <th onClick={ () => this.sortByColumn('MaKhoa') }>{ this.isBeingSorted('MaKhoa') && <i className='fas fa-sort sort-button'></i> } Khoa</th>
                                        <th onClick={ () => this.sortByColumn('MaKhoaDaoTao') }>{ this.isBeingSorted('MaKhoaDaoTao') && <i className='fas fa-sort sort-button'></i> } Khóa đào tạo</th>
                                        <th onClick={ () => this.sortByColumn('HeDaoTao') }>{ this.isBeingSorted('HeDaoTao') && <i className='fas fa-sort sort-button'></i> } Hệ đào tạo</th>
                                        <th onClick={ () => this.sortByColumn('TrangThai') }>{ this.isBeingSorted('TrangThai') && <i className='fas fa-sort sort-button'></i> } Trạng thái</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    {
                                        this.state.data.length > 0 ?
                                        this.state.data && this.state.data.map((record, index) => (
                                            <tr key={ index }>
                                                <td className='table-dropdown-menu-wrapper'>
                                                    <span className='table-dropdown-menu-toggle'><i className="fas fa-bars"></i></span>
                                                    <ul className='table-dropdown-menu'>
                                                        <li className='table-dropdown-menu-item'><Link to={ '/sinh-vien/xem-thong-tin/' + record.maSinhVien }>Xem thông tin</Link></li>
                                                        <li className='table-dropdown-menu-item'><Link to={ '/sinh-vien/cap-nhat/' + record.maSinhVien }>Cập nhật thông tin</Link></li>
                                                        <li className='table-dropdown-menu-item'><Link to="#" onClick={ () => this.showPopup('temporarilyDelete', record) }>Xóa tạm thời</Link></li>
                                                        { record.trangThai === -1 && <li className='table-dropdown-menu-item'><Link to="#" onClick={ () => this.showPopup('restore', record) }>Khôi phục lại</Link></li> }
                                                        <li className='table-dropdown-menu-item'><Link to="#" onClick={ () => this.showPopup('permanentlyDelete', record) }>Xóa vĩnh viễn</Link></li>
                                                    </ul>
                                                </td>
                                                { console.log(record) }
                                                <td className='table-text-bold'>{ record.maSinhVien }</td>
                                                <td>{ record.hoVaTen }</td>
                                                <td>{ record.gioiTinh }</td>
                                                <td>{ formatDateString(record.ngaySinh, false) }</td>
                                                <td>{ record.lop.tenLop }</td>
                                                <td>{ record.lop.khoa.tenKhoa }</td>
                                                <td>{ record.lop.khoaDaoTao.tenKhoaDaoTao }</td>
                                                <td>{ record.lop.heDaoTao }</td>
                                                <td><span className='active-badge' style={ this.getCurrentStatusColors(record.trangThai) }>{ this.getCurrentStatusText(record.trangThai) }</span></td>
                                            </tr>
                                        )) :
                                        <tr>
                                            <td className='text-center' colSpan={ 10 }>
                                                { this.state.keyword !== '' ? 'Không tìm thấy kết quả nào' : 'Chưa có dữ liệu' }
                                            </td>
                                        </tr>
                                    }
                                </tbody>
                            </table>

                            <div className='table-pagination'>
                                <div className='table-pagination__left'>
                                    { this.state.totalItems > 0 ? <p>Danh sách có tất cả <strong>{ this.state.totalItems }</strong> kết quả</p> : <p>Không có kết quả nào trong danh sách</p> }
                                </div>

                                <div className='table-pagination__right'>
                                    <label className='table-pagination__label'>Hiển thị dòng</label>
                                    <select className='table-pagination__select' value={ this.state.pageSize } onChange={ e => this.changePageSize(e) }>
                                        {
                                            PageSizes.map((pageSize, index) => (
                                                <option key={ index } value={ pageSize }>{ pageSize }</option>
                                            ))
                                        }
                                    </select>

                                    <label className='table-pagination__label'>Đến trang</label>
                                    <select className='table-pagination__select' onChange={ e => this.changePageNumber(e) }>
                                        {
                                            pageNumbers.map((pageNumber, index) => (
                                                <option key={ index } value={ pageNumber }>{ pageNumber }</option>
                                            ))
                                        }
                                    </select>

                                    <label className='table-pagination__label'> / { this.state.totalPages }</label>

                                    <span className='table-pagination__button' onClick={ () => this.navigateToPreviousPage() }><i className='fas fa-chevron-left'></i></span>
                                    <span className='table-pagination__button' onClick={ () => this.navigateToNextPage() }><i className='fas fa-chevron-right'></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>

                { this.state.showPopup.temporarilyDelete && <div className='popup-wrapper'>
                    <div className='popup'>
                        <header className='popup__header'>
                            <div className='popup-header__top-section'>
                                <i className='fas fa-times close-button' onClick={ () => this.closePopup('temporarilyDelete') }></i>
                            </div>
    
                            <div className='popup-header__main-section'>
                                <img src='/images/delete.png' className='popup__image' />
                                <h4 className='popup-header__title'>Xóa tạm thời khỏi hệ thống</h4>
                            </div>
                        </header>

                        <div className='popup__body'>
                            { !this.state.showPopupErrorMessage && <p>Bạn có chắc chắn muốn xóa tạm thời sinh viên <strong>{ this.state.activeRecord.tenSinhVien }</strong> khỏi hệ thống hay không?</p> }
                            { this.state.showPopupErrorMessage && <p>Xóa tạm thời dữ liệu khỏi hệ thống không thành công!</p> }
                        </div>

                        <footer className='popup__footer'>
                            <span className='popup-button-default' onClick={ () => this.closePopup('temporarilyDelete') }>Hủy bỏ</span>
                            { !this.state.showPopupErrorMessage && <span className='popup-button-danger' onClick={ () => this.onTemporarilyDelete(this.state.activeRecord.maSinhVien) }>Đồng ý</span> }
                        </footer>
                    </div>
                </div> }

                { this.state.showPopup.permanentlyDelete && <div className='popup-wrapper'>
                    <div className='popup'>
                        <header className='popup__header'>
                            <div className='popup-header__top-section'>
                                <i className='fas fa-times close-button' onClick={ () => this.closePopup('permanentlyDelete') }></i>
                            </div>
    
                            <div className='popup-header__main-section'>
                                <img src='/images/delete.png' className='popup__image' />
                                <h4 className='popup-header__title'>Xóa vĩnh viễn khỏi hệ thống</h4>
                            </div>
                        </header>

                        <div className='popup__body'>
                            { !this.state.showPopupErrorMessage && <Fragment>
                                <p className='alert-message'>Bạn có chắc chắn muốn xóa vĩnh viễn sinh viên <strong>{ this.state.activeRecord.tenSinhVien }</strong> khỏi hệ thống hay không?</p>
                                <p>Hành động này đồng nghĩa với việc dữ liệu sẽ bị xóa vĩnh viễn khỏi hệ thống và không thể khôi phục lại được. Để xác nhận, vui lòng nhập <span className='pre'>{ this.state.activeRecord.maSinhVien }</span> vào khung bên dưới</p>
                                <input type='text' className='form-input-outline' placeholder='Nhập nội dung xác nhận' value={ this.state.confirmationValue } onChange={ e => this.changeConfirmationValue(e) } />
                            </Fragment> }

                            { this.state.showPopupErrorMessage && <p>Xóa vĩnh viễn dữ liệu khỏi hệ thống không thành công!</p> }
                        </div>

                        <footer className='popup__footer'>
                            <span className='popup-button-default' onClick={ () => this.closePopup('permanentlyDelete') }>Hủy bỏ</span>
                            { !this.state.showPopupErrorMessage && <Fragment>
                                { this.state.validConfirmationValue ? <span className='popup-button-danger' onClick={ () => this.onPermanentlyDelete(this.state.activeRecord.maSinhVien) }>Đồng ý</span> : <span className='popup-button-default'>Đồng ý</span> }
                            </Fragment> }
                        </footer>
                    </div>
                </div> }

                { this.state.showPopup.restore && <div className='popup-wrapper'>
                    <div className='popup'>
                        <header className='popup__header'>
                            <div className='popup-header__top-section'>
                                <i className='fas fa-times close-button' onClick={ () => this.closePopup('restore') }></i>
                            </div>
    
                            <div className='popup-header__main-section'>
                                <img src='/images/restore.png' className='popup__image' />
                                <h4 className='popup-header__title'>Khôi phục lại dữ liệu đã bị xóa tạm thời</h4>
                            </div>
                        </header>

                        <div className='popup__body'>
                            { !this.state.showPopupErrorMessage && <p>Bạn có chắc chắn muốn khôi phục lại sinh viên <strong>{ this.state.activeRecord.tenSinhVien }</strong> đã bị xóa tạm thời hay không?</p> }
                            { this.state.showPopupErrorMessage && <p>Khôi phục lại dữ liệu đã bị xóa tạm thời không thành công!</p> }
                        </div>

                        <footer className='popup__footer'>
                            <span className='popup-button-default' onClick={ () => this.closePopup('restore') }>Hủy bỏ</span>
                            { !this.state.showPopupErrorMessage && <span className='popup-button-danger' onClick={ () => this.onRestore(this.state.activeRecord.maSinhVien) }>Đồng ý</span> }
                        </footer>
                    </div>
                </div> }

                { this.state.showPopup.import && <div className='popup-wrapper'>
                    <div className='popup'>
                        <header className='popup__header'>
                            <div className='popup-header__top-section'>
                                <i className='fas fa-times close-button' onClick={ () => this.closePopup('import') }></i>
                            </div>
    
                            <div className='popup-header__main-section'>
                                <img src='/images/import.png' className='popup__image' />
                                <h4 className='popup-header__title'>Nhập dữ liệu từ tập tin</h4>
                            </div>
                        </header>

                        <div className='popup__body'>
                            { !this.state.showPopupErrorMessage && <form className='upload-area'>
                                <p>Chọn tập tin mà bạn muốn nhập dữ liệu (.csv, .json)</p>
                                <p className='text-bold'>{ this.state.fileToImport && this.state.fileToImport.name }</p>
                                <input type="file" ref={ input => this.uploadFileToImportDialog = input } accept='.csv,application/JSON' onChange={ e => this.changeFileToImport(e) } />
                                <span className='popup-button-default-light' onClick={ () => this.showImportDialog() }>Chọn tập tin</span>
                            </form> }

                            { this.state.showPopupErrorMessage && <p>Nhập dữ liệu từ tập tin không thành công!</p> }
                        </div>

                        <footer className='popup__footer'>
                            <span className='popup-button-default' onClick={ () => this.closePopup('import') }>Hủy bỏ</span>
                        </footer>
                    </div>
                </div> }

                { this.state.showPopup.export && <div className='popup-wrapper'>
                    <div className='popup'>
                        <header className='popup__header'>
                            <div className='popup-header__top-section'>
                                <i className='fas fa-times close-button' onClick={ () => this.closePopup('export') }></i>
                            </div>
    
                            <div className='popup-header__main-section'>
                                <img src='/images/export.png' className='popup__image' />
                                <h4 className='popup-header__title'>Xuất dữ liệu ra tập tin</h4>
                            </div>
                        </header>

                        <div className='popup__body'>
                            { !this.state.showPopupErrorMessage && <Fragment>
                                <p>Chọn định dạng tập tin mà bạn muốn xuất dữ liệu ra</p>
                                <div className='file-type-selection'>
                                    <div className={ this.state.fileTypeToExport === 'csv' ? 'file-type-selected' : 'file-type' } onClick={ () => this.changeFileTypeToExport('csv') }>
                                        <img src='/images/excel.png' className='file-type-icon' />
                                        <p className='file-type-title'>CSV</p>
                                    </div>
                                    <div className={ this.state.fileTypeToExport === 'json' ? 'file-type-selected' : 'file-type' } onClick={ () => this.changeFileTypeToExport('json') }>
                                        <img src='/images/json.png' className='file-type-icon' />
                                        <p className='file-type-title'>JSON</p>
                                    </div>
                                </div>

                                <div className='form-group'>
                                    <p>Tên của tập tin được xuất ra</p>
                                    <input type='text' className='form-input-outline' placeholder='Nhập tên của tập tin được xuất ra' value={ this.state.fileNameToExport } onChange={ e => this.changeFileNameToExport(e) } />
                                </div>

                                <div className='form-group'>
                                    <p>Nguồn dữ liệu được xuất ra</p>
                                    <select className='form-input-outline' value={this.state.dataSourceToExport} onChange={e => this.changeDataSourceToExport(e)}>
                                        {
                                            ExportDataOptions.map((record, index) => (
                                                <option key={ index } value={ record.id }>{ record.value }</option>
                                            ))
                                        }
                                    </select>
                                </div>
                            </Fragment> }

                            { 
                                this.state.showPopupErrorMessage && 
                                <Fragment>
                                    { this.state.dataToExport.length === 0 ? <p>Chưa có dữ liệu để xuất ra!</p> : <p>Xuất dữ liệu ra tập tin không thành công!</p> }
                                </Fragment>
                            }
                        </div>

                        <footer className='popup__footer'>
                            <span className='popup-button-default' onClick={ () => this.closePopup('export') }>Hủy bỏ</span>
                            { !this.state.showPopupErrorMessage && <Fragment>
                                { this.state.fileTypeToExport !== '' && this.state.dataSourceToExport !== '' && this.state.fileNameToExport !== '' ? <span className='popup-button-danger' onClick={ () => this.exportData() }>Xuất</span> : <span className='popup-button-default'>Xuất</span> }
                            </Fragment>}
                        </footer>
                    </div>
                </div> }

                { this.state.showPopup.exportDocument && <div className='popup-wrapper'>
                    <div className='popup'>
                        <header className='popup__header'>
                            <div className='popup-header__top-section'>
                                <i className='fas fa-times close-button' onClick={ () => this.closePopup('exportDocument') }></i>
                            </div>
    
                            <div className='popup-header__main-section'>
                                <img src='/images/export.png' className='popup__image' />
                                <h4 className='popup-header__title'>Xuất báo cáo ra tập tin</h4>
                            </div>
                        </header>

                        <div className='popup__body'>
                            { !this.state.showPopupErrorMessage && <Fragment>
                                <p>Chọn định dạng tập tin mà bạn muốn xuất báo cáo ra</p>
                                <div className='file-type-selection'>
                                    <div className={ this.state.fileTypeToExportDocument === 'xlsx' ? 'file-type-selected' : 'file-type' } onClick={ () => this.changeFileTypeToExportDocument('xlsx') }>
                                        <img src='/images/excel.png' className='file-type-icon' />
                                        <p className='file-type-title'>XLSX</p>
                                    </div>
                                    <div className={ this.state.fileTypeToExportDocument === 'pdf' ? 'file-type-selected' : 'file-type' } onClick={ () => this.changeFileTypeToExportDocument('pdf') }>
                                        <img src='/images/pdf.png' className='file-type-icon' />
                                        <p className='file-type-title'>PDF</p>
                                    </div>
                                </div>

                                { this.state.fileTypeToExportDocument.toUpperCase() !== 'PDF' && <div className='form-group'>
                                    <p>Tên của tập tin được xuất ra</p>
                                    <input type='text' className='form-input-outline' placeholder='Nhập tên của tập tin được xuất ra' value={ this.state.fileNameToExportDocument } onChange={ e => this.changeFileNameToExportDocument(e) } />
                                </div> }

                                <div className='form-group'>
                                    <p>Nguồn dữ liệu được xuất ra</p>
                                    <select className='form-input-outline' value={this.state.dataSourceToExportDocument} onChange={e => this.changeDataSourceToExportDocument(e)}>
                                        {
                                            ExportDataOptions.map((record, index) => (
                                                <option key={ index } value={ record.id }>{ record.value }</option>
                                            ))
                                        }
                                    </select>
                                </div>
                            </Fragment> }

                            { 
                                this.state.showPopupErrorMessage && 
                                <Fragment>
                                    { this.state.dataToExportDocument.length == 0 ? <p>Chưa có dữ liệu để xuất báo cáo!</p> : <p>Xuất báo cáo ra tập tin không thành công!</p> }
                                </Fragment>
                            }
                        </div>

                        <footer className='popup__footer'>
                            <span className='popup-button-default' onClick={ () => this.closePopup('exportDocument') }>Hủy bỏ</span>
                            { !this.state.showPopupErrorMessage && <Fragment>
                                { this.state.fileTypeToExportDocument !== '' && this.state.dataSourceToExportDocument !== '' && this.state.fileNameToExportDocument !== '' ? <span className='popup-button-danger' onClick={ () => this.exportDocument() }>Xuất</span> : <span className='popup-button-default'>Xuất</span> }
                            </Fragment> }
                        </footer>
                    </div>
                </div> }

                <SinhVienForPrint data={ this.state.dataToExportDocument } />
            </Fragment>
        )
    }
}

const mapStateToProps = state => ({
    user: state.user
})
  
export default connect(mapStateToProps, null)(SinhVienForList)