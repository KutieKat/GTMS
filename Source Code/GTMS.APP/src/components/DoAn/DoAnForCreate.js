import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import axios from 'axios'
import apiRoutes from '../../routes/apis'
import moment from 'moment'
import { formatDateString, standardizeScore } from '../../utils'

const ChucVuHDBV = ['Chủ tịch hội đồng', 'Thư ký hội đồng', 'Ủy viên hội đồng']

class DoAnForCreate extends Component {
    constructor (props) {
      super(props)
      this.state = {
          editingData: {
              tenDoAn: '',
              moTa: '',
              thoiGianBaoCao: moment().format('YYYY-MM-DD'),
              maHuongNghienCuu: 0,
              maHocKy: 0,
              sinhVienThucHien: [],
              giangVienHuongDan: [],
              giangVienPhanBien: [],
              hoiDongBaoVe: [],
              nhanXetChung: '',
              diemTongKet: 0,
              diaDiemBaoCao: '',
              lienKetTaiDoAn: '',
              nhanXetChung: '',

              // Các bảng chính thức
              thucHienDoAn: [],
              huongDanDoAn: [],
              phanBienDoAn: [],
              baoVeDoAn: [],
              diemDoAn: []
          },
          huongNghienCuu: [],
          hocKy: [],
          sinhVien: [],
          giangVien: [],
          errors: null,
          showPopup: {
              selectSVTH: false,
              selectGVHD: false,
              selectGVPB: false,
              selectHDBV: false
          },
          svthKeyword: '',
          gvhdKeyword: '',
          gvpbKeyword: '',
          hdbvKeyword: '',
          quyDinh: {}
      }
    }

    componentDidMount() {
        window.scrollTo(0, 0)
        axios.defaults.headers.common['Authorization'] = 'AUTH_TOKEN'
        this.fetchData()
    }

    fetchData() {
        this.fetchHuongNghienCuu()
        this.fetchHocKy()
        this.fetchSinhVien()
        this.fetchGiangVien()
        this.fetchQuyDinh()
    }

    async fetchHuongNghienCuu() {
        const url = apiRoutes.huongNghienCuu.getAll
        const params = {}
        params['pageSize'] = 1000
        params['trangThai'] = 1

        try {
            const response = await axios.get(url, { params })

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

                this.setState({ huongNghienCuu: data, editingData: { ...this.state.editingData, maHuongNghienCuu: data[0].maHuongNghienCuu } })
            }
            else {
                throw new Error(response.errors)
            }
        }
        catch (error) {
            console.error(error)
        }
    }

    async fetchHocKy() {
        const url = apiRoutes.hocKy.getAll
        const params = {}
        params['pageSize'] = 1000
        params['trangThai'] = 1

        try {
            const response = await axios.get(url, { params })

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

                this.setState({ hocKy: data, editingData: { ...this.state.editingData, maHocKy: data[0].maHocKy } })
            }
            else {
                throw new Error(response.errors)
            }
        }
        catch (error) {
            console.error(error)
        }
    }

    async fetchSinhVien() {
        const url = apiRoutes.sinhVien.getAll
        const params = {}
        params['pageSize'] = 1000
        params['trangThai'] = 1
        params['keyword'] = this.state.svthKeyword

        try {
            const response = await axios.get(url, { params })

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

                this.setState({ sinhVien: data })
            }
            else {
                throw new Error(response.errors)
            }
        }
        catch (error) {
            console.error(error)
        }
    }

    async fetchGiangVien() {
        const url = apiRoutes.giangVien.getAll
        const params = {}
        params['pageSize'] = 1000
        params['trangThai'] = 1
        params['keyword'] = this.state.gvhdKeyword

        try {
            const response = await axios.get(url, { params })

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result
                this.setState({ giangVien: data })
            }
            else {
                throw new Error(response.errors)
            }
        }
        catch (error) {
            console.error(error)
        }
    }

    fetchQuyDinh() {
        this.setState({
            quyDinh: {
                soSVTHToiThieu: 1,
                soSVTHToiDa: 2,
                soGVHDToiThieu: 1,
                soGVHDToiDa: 1,
                soGVPBToiThieu: 1,
                soGVPBToiDa: 1,
                soTVHDToiThieu: 3,
                soTVHDToiDa: 5,
                heSoGVHD: 2,
                heSoGVPB: 2,
                heSoTVHD: 1,
                diemSoToiThieu: 0,
                diemSoToiDa: 10
            }
        })
    }

    changeEditingData(e, fieldName) {
        const value = e.target.value
        const editingData = { ...this.state.editingData }

        if (typeof value === 'number') {
            value = parseInt(value)
        }

        editingData[fieldName] = value

        this.setState({ editingData })
    }

    submit() {
        // const { tenDoAn, moTa, thucHienDoAn, huongDanDoAn, phanBienDoAn, baoVeDoAn, diemDoAn, diaDiemBaoCao, lienKetTaiDoAn, nhanXetChung } = this.state.editingData
        // const soCTHD = this.state.editingData.baoVeDoAn.filter(r => r.chucVu = 'Chủ tịch hội đồng').length
        // const soTKHD = this.state.editingData.baoVeDoAn.filter(r => r.chucVu = 'Thư ký hội đồng').length
        // const soUVHD = this.state.editingData.baoVeDoAn.filter(r => r.chucVu = 'Ủy viên hội đồng').length
        // const isValidDiem = !this.state.editingData.diemDoAn.some(r => r.diem >= this.state.quyDinh.diemSoToiThieu && r.diem <= this.state.quyDinh.diemSoToiDa)

        // if (
        //     tenDoAn !== '' && 
        //     moTa !== '' && 
        //     thucHienDoAn.length >= this.state.quyDinh.soSVTHToiThieu &&
        //     thucHienDoAn.length <= this.state.quyDinh.soSVTHToiDa &&
        //     huongDanDoAn.length >= this.state.quyDinh.soGVHDToiThieu &&
        //     huongDanDoAn.length <= this.state.quyDinh.soGVHDToiDa &&
        //     phanBienDoAn.length >= this.state.quyDinh.soGVPBToiThieu &&
        //     phanBienDoAn.length <= this.state.quyDinh.soGVPBToiDa &&
        //     baoVeDoAn.length >= this.state.quyDinh.soTVHDToiThieu &&
        //     baoVeDoAn.length <= this.state.quyDinh.soTVHDToiDa &&
        //     diemDoAn.length > 0 &&
        //     diaDiemBaoCao.length !== '' && 
        //     lienKetTaiDoAn !== '' &&
        //     nhanXetChung !== '' &&
        //     soCTHD >= this.state.quyDinh.soCTHDToiThieu &&
        //     soCTHD <= this.state.quyDinh.soCTHDToiDa &&
        //     soTKHD >= this.state.quyDinh.soTKHDToiThieu &&
        //     soTKHD <= this.state.quyDinh.soTKHDToiDa &&
        //     soUVHD >= this.state.quyDinh.soUVHDToiThieu &&
        //     soUVHD <= this.state.quyDinh.soUVHDToiDa &&
        //     isValidDiem
        // ) {
        //     const url = apiRoutes.doAn.create
        //     const data = {
        //         tenDoAn: this.state.editingData.tenDoAn,
        //         moTa: this.state.editingData.moTa,
        //         thoiGianBaoCao: this.state.editingData.thoiGianBaoCao,
        //         maHuongNghienCuu: this.state.editingData.maHuongNghienCuu,
        //         maHocKy: this.state.editingData.maHocKy,
        //         thucHienDoAn: this.state.editingData.thucHienDoAn || [],
        //         huongDanDoAn: this.state.editingData.huongDanDoAn || [],
        //         phanBienDoAn: this.state.editingData.phanBienDoAn || [],
        //         baoVeDoAn: this.state.editingData.baoVeDoAn || [],
        //         diemDoAn: this.state.editingData.diemDoAn || [],
        //         diaDiemBaoCao: this.state.editingData.diaDiemBaoCao,
        //         lienKetTaiDoAn: this.state.editingData.lienKetTaiDoAn,
        //         nhanXetChung: this.state.editingData.nhanXetChung,
        //         diemTongKet: this.state.diemTongKet
        //     }

        //     return axios.post(url, data)
        //     .then(response => {
        //         this.props.history.push('/do-an')
        //     })
        //     .catch(error => {
        //         const { errors } = error.response.data.result
        //         this.setState({ errors })
        //     })
        // }
        // else {
        //     const errors = {}

        //     if (tenDoAn === '') {
        //         errors['tenDoAn'] = ['tenDoAn is required!']
        //     }

        //     if (moTa === '') {
        //         errors['moTa'] = ['moTa is required!']
        //     }

        //     if (nhanXetChung === '') {
        //         errors['nhanXetChung'] = ['nhanXetChung is required!']
        //     }

        //     if (lienKetTaiDoAn === '') {
        //         errors['lienKetTaiDoAn'] = ['lienKetTaiDoAn is required!']
        //     }

        //     if (diaDiemBaoCao === '') {
        //         errors['diaDiemBaoCao'] = ['diaDiemBaoCao is required!']
        //     }

        //     if (!(thucHienDoAn.length >= this.state.quyDinh.soSVTHToiThieu && thucHienDoAn.length <= this.state.quyDinh.soSVTHToiDa)) {
        //         errors['soSVTH'] = ['soSVTH is not valid!']
        //     }

        //     if (!(huongDanDoAn.length >= this.state.quyDinh.soGVHDToiThieu && huongDanDoAn.length <= this.state.quyDinh.soGVHDToiDa)) {
        //         errors['soGVHD'] = ['soGVHD is not valid!']
        //     }

        //     if (!(phanBienDoAn.length >= this.state.quyDinh.soGVPBToiThieu && phanBienDoAn.length <= this.state.quyDinh.soGVPBToiDa)) {
        //         errors['soGVPB'] = ['soGVPB is not valid!']
        //     }

        //     if (!(baoVeDoAn.length >= this.state.quyDinh.soTVHDToiThieu && baoVeDoAn.length <= this.state.quyDinh.soTVHDToiDa)) {
        //         errors['soTVHD'] = ['soTVHD is not valid!']
        //     }

        //     if (!(soCTHD >= this.state.quyDinh.soCTHDToiThieu && soCTHD <= this.state.quyDinh.soCTHDToiDa)) {
        //         errors['soCTHD'] = ['soCTHD is not valid!']
        //     }

        //     if (!(soTKHD >= this.state.quyDinh.soTKHDToiThieu && soTKHD <= this.state.quyDinh.soTKHDToiDa)) {
        //         errors['soTKHD'] = ['soTKHD is not valid!']
        //     }

        //     if (!(soUVHD >= this.state.quyDinh.soUVHDToiThieu && soUVHD <= this.state.quyDinh.soUVHDToiDa)) {
        //         errors['soUVHD'] = ['soUVHD is not valid!']
        //     }

        //     if (!isValidDiem) {
        //         errors['diem'] = ['diem is not valid!']
        //     }

        //     this.setState({ errors })
        // }

        const url = apiRoutes.doAn.create
        const data = {
            tenDoAn: this.state.editingData.tenDoAn,
            moTa: this.state.editingData.moTa,
            thoiGianBaoCao: this.state.editingData.thoiGianBaoCao,
            maHuongNghienCuu: this.state.editingData.maHuongNghienCuu,
            maHocKy: this.state.editingData.maHocKy,
            thucHienDoAn: this.state.editingData.thucHienDoAn || [],
            huongDanDoAn: this.state.editingData.huongDanDoAn || [],
            phanBienDoAn: this.state.editingData.phanBienDoAn || [],
            baoVeDoAn: this.state.editingData.baoVeDoAn || [],
            diemDoAn: this.state.editingData.diemDoAn || [],
            diaDiemBaoCao: this.state.editingData.diaDiemBaoCao,
            lienKetTaiDoAn: this.state.editingData.lienKetTaiDoAn,
            nhanXetChung: this.state.editingData.nhanXetChung,
            diemTongKet: this.state.diemTongKet
        }

        return axios.post(url, data)
        .then(response => {
            this.props.history.push('/do-an')
        })
        .catch(error => {
            const { errors } = error.response.data.result
            this.setState({ errors })
        })
    }

    transformErrorMessage(rawMessage) {
        let subject = rawMessage.split(' ')[0].toLowerCase()
        let message = ''

        switch (subject) {
            case 'tendoan':
                subject = 'Tên đồ án'
                break
            
            case 'mota':
                subject = 'Mô tả của đồ án'
                break

            case 'nhanxetchung':
                subject = 'Nhận xét chung của đồ án'
                break

            case 'lienkettaidoan':
                subject = 'Liên kết tải đồ án'
                break

            case 'diadiembaocao':
                subject = 'Địa điểm báo cáo'
                break

            case 'sosvth':
                subject = 'Số sinh viên thực hiện'
                break

            case 'sogvhd':
                subject = 'Số giảng viên hướng dẫn'
                break
            
            case 'sogvpb':
                subject = 'Số giảng viên phản biện'
                break
            
            case 'sotvhd':
                subject = 'Số thành viên hội đồng'
                break
            
            case 'socthd':
                subject = 'Số chủ tịch hội đồng'
                break
            
            case 'sotkhd':
                subject = 'Số thư ký hội đồng'
                break
            
            case 'souvhd':
                subject = 'Số ủy viên hội đồng'
                break
            
            case 'diem':
                subject = 'Điểm đồ án'
                break
        }

        if (rawMessage.includes('require')) {
            message = subject + ' là bắt buộc và không được bỏ trống!'
        }

        if (rawMessage.includes('duplicate')) {
            message = subject + ' đã bị trùng!'
        }

        if (rawMessage.includes('is not valid')) {
            message = subject + ' không hợp lệ!'
        }

        return message
    }

    hideAlert() {
        this.setState({ errors: null })
    }

    isValidInputField(inputFieldName) {
        return this.state.errors && Object.keys(this.state.errors).map(key => key.toLowerCase()).indexOf(inputFieldName.toLowerCase()) > -1
    }

    showPopup(name) {
        const showPopup = { ...this.state.showPopup }
        showPopup[name] = true

        this.setState({ showPopup }, this.fetchData)
    }

    closePopup(name) {
        const showPopup = { ...this.state.showPopup }
        showPopup[name] = false

        this.setState({ 
            showPopup, 
            showPopupErrorMessage: false,
            svthKeyword: '',
            gvhdKeyword: '',
            gvpbKeyword: '',
            hdbvKeyword: ''
        })
    }

    selectSVTH(record) {
        let sinhVienThucHien = [...this.state.editingData.sinhVienThucHien]
        let thucHienDoAn = [...this.state.editingData.thucHienDoAn]
        let isDuplicatedIndex = sinhVienThucHien.findIndex(r => r.maSinhVien === record.maSinhVien)

        if (sinhVienThucHien.length === 0 || isDuplicatedIndex === -1) {
            sinhVienThucHien.push(record)
            thucHienDoAn.push(record.maSinhVien)
        }

        this.setState({ editingData: { ... this.state.editingData, sinhVienThucHien, thucHienDoAn }})
        this.closePopup('selectSVTH')
    }

    removeSVTH(e, record) {
        e.stopPropagation()
        let sinhVienThucHien = [...this.state.editingData.sinhVienThucHien]
        let thucHienDoAn = [...this.state.editingData.thucHienDoAn]
        let index = sinhVienThucHien.findIndex(r => r.maSinhVien === record.maSinhVien)

        sinhVienThucHien.splice(index, 1)
        thucHienDoAn.splice(index, 1)
        this.setState({ editingData: { ...this.state.editingData, sinhVienThucHien, thucHienDoAn }})
    }

    selectGVHD(record) {
        let giangVienHuongDan = [...this.state.editingData.giangVienHuongDan]
        let huongDanDoAn = [...this.state.editingData.huongDanDoAn]
        let diemDoAn = [...this.state.editingData.diemDoAn]
        let isDuplicatedIndex = giangVienHuongDan.findIndex(r => r.maGiangVien === record.maGiangVien)

        if (giangVienHuongDan.length === 0 || isDuplicatedIndex === -1) {
            giangVienHuongDan.push({ ...record })
            huongDanDoAn.push({ maGiangVien: record.maGiangVien, nhanXet: '' })
            diemDoAn.push({ maGiangVien: record.maGiangVien, diem: 0, heSo: this.state.quyDinh.heSoGVHD })
        }

        this.setState({ editingData: { ... this.state.editingData, giangVienHuongDan, huongDanDoAn, diemDoAn }})
        this.closePopup('selectGVHD')
    }

    changeNhanXetGVHD(e, record) {
        const nhanXet = e.target.value
        let giangVienHuongDan = [...this.state.editingData.giangVienHuongDan]
        let huongDanDoAn = [...this.state.editingData.huongDanDoAn]
        let index = giangVienHuongDan.findIndex(r => r.maGiangVien === record.maGiangVien)

        huongDanDoAn[index]['nhanXet'] = nhanXet
        this.setState({ editingData: { ...this.state.editingData, huongDanDoAn }})
    }

    removeGVHD(e, record) {
        e.stopPropagation()
        let giangVienHuongDan = [...this.state.editingData.giangVienHuongDan]
        let huongDanDoAn = [...this.state.editingData.huongDanDoAn]
        let diemDoAn = [...this.state.editingData.diemDoAn]
        let index = giangVienHuongDan.findIndex(r => r.maGiangVien === record.maGiangVien)

        giangVienHuongDan.splice(index, 1)
        huongDanDoAn.splice(index, 1)
        diemDoAn.splice(index, 1)
        this.setState({ editingData: { ...this.state.editingData, giangVienHuongDan, huongDanDoAn, diemDoAn }})
    }

    selectGVPB(record) {
        let giangVienPhanBien = [...this.state.editingData.giangVienPhanBien]
        let phanBienDoAn = [...this.state.editingData.phanBienDoAn]
        let diemDoAn = [...this.state.editingData.diemDoAn]
        let isDuplicatedIndex = giangVienPhanBien.findIndex(r => r.maGiangVien === record.maGiangVien)

        if (giangVienPhanBien.length === 0 || isDuplicatedIndex === -1) {
            giangVienPhanBien.push({ ...record })
            phanBienDoAn.push({ maGiangVien: record.maGiangVien, nhanXet: '' })
            diemDoAn.push({ maGiangVien: record.maGiangVien, diem: 0, heSo: this.state.quyDinh.heSoGVPB })
        }

        this.setState({ editingData: { ... this.state.editingData, giangVienPhanBien, phanBienDoAn, diemDoAn }})
        this.closePopup('selectGVPB')
    }

    changeNhanXetGVPB(e, record) {
        const nhanXet = e.target.value
        let giangVienPhanBien = [...this.state.editingData.giangVienPhanBien]
        let phanBienDoAn = [...this.state.editingData.phanBienDoAn]
        let index = giangVienPhanBien.findIndex(r => r.maGiangVien === record.maGiangVien)

        phanBienDoAn[index]['nhanXet'] = nhanXet
        this.setState({ editingData: { ...this.state.editingData, phanBienDoAn }})
    }

    removeGVPB(e, record) {
        e.stopPropagation()
        let giangVienPhanBien = [...this.state.editingData.giangVienPhanBien]
        let phanBienDoAn = [...this.state.editingData.phanBienDoAn]
        let diemDoAn = [...this.state.editingData.diemDoAn]
        let index = giangVienPhanBien.findIndex(r => r.maGiangVien === record.maGiangVien)

        giangVienPhanBien.splice(index, 1)
        phanBienDoAn.splice(index, 1)
        diemDoAn.splice(index, 1)
        this.setState({ editingData: { ...this.state.editingData, giangVienPhanBien, phanBienDoAn, diemDoAn }})
    }

    selectHDBV(record) {
        let hoiDongBaoVe = [...this.state.editingData.hoiDongBaoVe]
        let baoVeDoAn = [...this.state.editingData.baoVeDoAn]
        let diemDoAn = [...this.state.editingData.diemDoAn]
        let isDuplicatedIndex = hoiDongBaoVe.findIndex(r => r.maGiangVien === record.maGiangVien)

        if (hoiDongBaoVe.length === 0 || isDuplicatedIndex === -1) {
            hoiDongBaoVe.push({ ...record })
            baoVeDoAn.push({ maGiangVien: record.maGiangVien, chucVu: ChucVuHDBV[0] })

            if (!diemDoAn.some(r => r.maGiangVien === record.maGiangVien)) {
                diemDoAn.push({ maGiangVien: record.maGiangVien, diem: 0, heSo: this.state.quyDinh.heSoTVHD })
            }
        }

        this.setState({ editingData: { ... this.state.editingData, hoiDongBaoVe, baoVeDoAn, diemDoAn }})
        this.closePopup('selectHDBV')
    }

    changeChucVuHDBV(e, record) {
        const chucVu = e.target.value
        let hoiDongBaoVe = [...this.state.editingData.hoiDongBaoVe]
        let baoVeDoAn = [...this.state.editingData.baoVeDoAn]
        let index = hoiDongBaoVe.findIndex(r => r.maGiangVien === record.maGiangVien)

        baoVeDoAn[index]['chucVu'] = chucVu
        this.setState({ editingData: { ...this.state.editingData, baoVeDoAn }})
    }

    removeHDBV(e, record) {
        e.stopPropagation()
        let hoiDongBaoVe = [...this.state.editingData.hoiDongBaoVe]
        let baoVeDoAn = [...this.state.editingData.baoVeDoAn]
        let diemDoAn = [...this.state.editingData.diemDoAn]
        let index = hoiDongBaoVe.findIndex(r => r.maGiangVien === record.maGiangVien)

        hoiDongBaoVe.splice(index, 1)
        baoVeDoAn.splice(index, 1)
        diemDoAn.splice(index, 1)
        this.setState({ editingData: { ...this.state.editingData, hoiDongBaoVe, baoVeDoAn, diemDoAn }})
    }

    changeDiemHDBV(e, record) {
        const diem = Number(e.target.value)

        let diemDoAn = [...this.state.editingData.diemDoAn]
        let index = diemDoAn.findIndex(r => r.maGiangVien === record.maGiangVien)

        diemDoAn[index].diem = diem

        this.setState({ editingData: { ...this.state.editingData, diemDoAn }}, this.calculateDiemTongKet)
    }

    calculateDiemTongKet() {
        let tongDiem = 0;
        let tongHeSo = 0;

        this.state.editingData.diemDoAn.forEach(record => {
            tongDiem += Number(record.diem) * Number(record.heSo)
            tongHeSo += Number(record.heSo)
        })

        let diemTongKet = (tongDiem / tongHeSo).toFixed(this.state.quyDinh.soChuSoThapPhan)

        this.setState({ editingData: { ...this.state.editingData, diemTongKet } })
    }

    render() {
        let result = []

        this.state.editingData.diemDoAn.forEach(record => {
            const fullRecord1 = this.state.giangVien.find(i => i.maGiangVien === record.maGiangVien)

            result.push({
                maGiangVien: fullRecord1.maGiangVien,
                hoVaTen: fullRecord1.hoVaTen,
                vaiTro: (() => {
                    let roles = []

                    if (this.state.editingData.huongDanDoAn.some(r => r.maGiangVien === record.maGiangVien)) {
                        roles.push('Giảng viên hướng dẫn đồ án')
                    }

                    if (this.state.editingData.phanBienDoAn.some(r => r.maGiangVien === record.maGiangVien)) {
                        roles.push('Giảng viên phản biện đồ án')
                    }

                    if (this.state.editingData.baoVeDoAn.some(r => r.maGiangVien === record.maGiangVien)) {
                        let role = this.state.editingData.baoVeDoAn.find(r => r.maGiangVien === record.maGiangVien)
                        roles.push(role.chucVu)
                    }

                    return roles.join(', ')
                })(),
                heSo: record.heSo,
                diem: record.diem
            })
        })

        // const obj = {
        //     tenDoAn: this.state.editingData.tenDoAn,
        //     moTa: this.state.editingData.moTa,
        //     thoiGianBaoCao: this.state.editingData.thoiGianBaoCao,
        //     maHuongNghienCuu: this.state.editingData.maHuongNghienCuu,
        //     maHocKy: this.state.editingData.maHocKy,
        //     thucHienDoAn: this.state.editingData.thucHienDoAn || [],
        //     huongDanDoAn: this.state.editingData.huongDanDoAn || [],
        //     phanBienDoAn: this.state.editingData.phanBienDoAn || [],
        //     baoVeDoAn: this.state.editingData.baoVeDoAn || [],
        //     diemDoAn: this.state.editingData.diemDoAn || [],
        //     diaDiemBaoCao: this.state.editingData.diaDiemBaoCao,
        //     lienKetTaiDoAn: this.state.editingData.lienKetTaiDoAn,
        //     nhanXetChung: this.state.editingData.nhanXetChung,
        //     diemTongKet: this.state.diemTongKet
        // }

        // Popup data
        const gvhd = this.state.giangVien.filter(record => {
            return !this.state.editingData.huongDanDoAn.some(r => r.maGiangVien === record.maGiangVien) && !this.state.editingData.phanBienDoAn.some(r => r.maGiangVien === record.maGiangVien)
        })

        const gvpb = this.state.giangVien.filter(record => {
            return !this.state.editingData.huongDanDoAn.some(r => r.maGiangVien === record.maGiangVien) && !this.state.editingData.phanBienDoAn.some(r => r.maGiangVien === record.maGiangVien)
        })

        return (
            <Fragment>
                <section className='breadcrumbs'>
                <span className='breadcrumb-home'><Link to='/'>Graduation Thesis Management System (GTMS)</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb'><Link to='/do-an'>Quản lý đồ án</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Thêm đồ án mới</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Thêm đồ án mới</h3>
                            <p className='section__subtitle'>Thêm đồ án mới vào hệ thống</p>
                        </div>
                    </header>

                    { this.state.errors && <div className='section__alert'>
                        <ul>
                            {
                                Object.keys(this.state.errors).map((error, index) => {
                                    return this.state.errors[error].map(message => (
                                        <li key={ index }><i className="fas fa-exclamation-triangle"></i>&nbsp;&nbsp;{ this.transformErrorMessage(message) }</li>
                                    ))
                                })
                            }
                        </ul>
                        { this.state.message }
                    </div> }

                    <div className='section__body'>
                        <div className='section-body-main'>
                            <form autoComplete='off'>
                                <h4>Thông tin chung về đồ án</h4>
                                <div className='form-group'>
                                    <label className='form-label'>Tên đồ án</label>
                                    <input type='text' className={ this.isValidInputField('tenDoAn') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập tên của đồ án' value={ this.state.editingData.tenDoAn } onChange={ e => this.changeEditingData(e, 'tenDoAn') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Mô tả về đồ án</label>
                                    <textarea className={ this.isValidInputField('moTa') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập mô tả về đồ án' value={ this.state.editingData.moTa } onChange={ e => this.changeEditingData(e, 'moTa') } onFocus={ () => this.hideAlert() }></textarea>
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Hướng nghiên cứu</label>
                                    <select className={this.isValidInputField('maHuongNghienCuu') ? 'form-input-alert' : 'form-input-outline'} value={this.state.editingData.maHuongNghienCuu} onChange={e => this.changeEditingData(e, 'maHuongNghienCuu')} onFocus={() => this.hideAlert()}>
                                        {
                                            this.state.huongNghienCuu.map((record, index) => (
                                                <option key={ index } value={ record.maHuongNghienCuu }>{ record.tenHuongNghienCuu }</option>
                                            ))
                                        }
                                    </select>
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Học kỳ</label>
                                    <select className={this.isValidInputField('maHocKy') ? 'form-input-alert' : 'form-input-outline'} value={this.state.editingData.maHocKy} onChange={e => this.changeEditingData(e, 'maHocKy')} onFocus={() => this.hideAlert()}>
                                        {
                                            this.state.hocKy.map((record, index) => (
                                                <option key={ index } value={ record.maHocKy }>{ record.tenHocKy }</option>
                                            ))
                                        }
                                    </select>
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Thời gian báo cáo</label>
                                    <input type='date' className={ this.isValidInputField('thoiGianBaoCao') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập thời gian báo cáo đồ án' value={ moment(this.state.editingData.thoiGianBaoCao).format('YYYY-MM-DD') } onChange={ e => this.changeEditingData(e, 'thoiGianBaoCao') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Địa điểm báo cáo</label>
                                    <input type='text' className={ this.isValidInputField('diaDiemBaoCao') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập địa điểm báo cáo đồ án' value={ this.state.editingData.diaDiemBaoCao } onChange={ e => this.changeEditingData(e, 'diaDiemBaoCao') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Liên kết tải đồ án</label>
                                    <input type='text' className={ this.isValidInputField('lienKetTaiDoAn') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập liên kết tải đồ án' value={ this.state.editingData.lienKetTaiDoAn } onChange={ e => this.changeEditingData(e, 'lienKetTaiDoAn') } onFocus={ () => this.hideAlert() } />
                                </div>

                                {/* Sinh viên thực hiện */}

                                <div className='subsection__header'>
                                    <h4>Danh sách sinh viên thực hiện đồ án</h4>
                                    { 
                                        (this.state.editingData.sinhVienThucHien.length === 0 || this.state.editingData.sinhVienThucHien.length < this.state.quyDinh.soSVTHToiDa) &&
                                        <span className='button' onClick={ () => this.showPopup('selectSVTH') }>Thêm mới</span> 
                                    }
                                </div>

                                <table className='table'>
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>STT</th>
                                            <th>MSSV</th>
                                            <th>Họ và tên</th>
                                            <th>Giới tính</th>
                                            <th>Ngày sinh</th>
                                            <th>Lớp</th>
                                            <th>Khóa đào tạo</th>
                                            <th>Hệ đào tạo</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {
                                            this.state.editingData.sinhVienThucHien.length > 0 ? this.state.editingData.sinhVienThucHien.map((record, index) => (
                                                <tr key={ index }>
                                                    <td className='table-dropdown-menu-wrapper'>
                                                        <span className='table-dropdown-menu-toggle' onClick={ (e) => this.removeSVTH(e, record) }><i className="fas fa-times"></i></span>
                                                    </td>
                                                    <td>{ index + 1 }</td>
                                                    <td>{ record.maSinhVien }</td>
                                                    <td>{ record.hoVaTen }</td>
                                                    <td>{ record.gioiTinh }</td>
                                                    <td>{ formatDateString(record.ngaySinh, false) }</td>
                                                    <td>{ record.lop.tenLop }</td>
                                                    <td>{ record.lop.khoa.tenKhoa }</td>
                                                    <td>{ record.lop.heDaoTao }</td>
                                                </tr>
                                            )) :
                                            <tr>
                                                <td className='text-center' colSpan={ 9 }>(Chưa có dữ liệu)</td>
                                            </tr>
                                        }
                                    </tbody>
                                </table>

                                {/* Giảng viên hướng dẫn */}

                                <div className='subsection__header'>
                                    <h4>Danh sách giảng viên hướng dẫn đồ án</h4>
                                    {
                                        (this.state.editingData.giangVienHuongDan.length === 0 || this.state.editingData.giangVienHuongDan.length < this.state.quyDinh.soGVHDToiDa) &&
                                        <span className='button' onClick={ () => this.showPopup('selectGVHD') }>Thêm mới</span> 
                                    }
                                </div>

                                <table className='table'>
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>STT</th>
                                            <th>MSCB</th>
                                            <th>Họ và tên</th>
                                            <th>Học hàm</th>
                                            <th>Học vị</th>
                                            <th>Khoa</th>
                                            <th>Đơn vị công tác</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {
                                            this.state.editingData.giangVienHuongDan.length > 0 ? this.state.editingData.giangVienHuongDan.map((record, index) => (
                                                <tr key={ index }>
                                                    <td className='table-dropdown-menu-wrapper'>
                                                        <span className='table-dropdown-menu-toggle' onClick={ (e) => this.removeGVHD(e, record) }><i className="fas fa-times"></i></span>
                                                    </td>
                                                    <td>{ index + 1 }</td>
                                                    <td>{ record.maGiangVien }</td>
                                                    <td>{ record.hoVaTen }</td>
                                                    <td>{ record.hocHam }</td>
                                                    <td>{ record.hocVi}</td>
                                                    <td>{ record.khoa && record.khoa.tenKhoa }</td>
                                                    <td>{ record.donViCongTac }</td>
                                                </tr>
                                            )) :
                                            <tr>
                                                <td className='text-center' colSpan={ 8 }>(Chưa có dữ liệu)</td>
                                            </tr>
                                        }
                                    </tbody>
                                </table>

                                {/* Giảng viên phản biện */}

                                <div className='subsection__header'>
                                    <h4>Danh sách giảng viên phản biện đồ án</h4>
                                    {
                                        (this.state.editingData.giangVienPhanBien.length === 0 || this.state.editingData.giangVienPhanBien.length < this.state.quyDinh.soGVPBToiDa) &&
                                        <span className='button' onClick={ () => this.showPopup('selectGVPB') }>Thêm mới</span> 
                                    }
                                </div>

                                <table className='table'>
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>STT</th>
                                            <th>MSCB</th>
                                            <th>Họ và tên</th>
                                            <th>Học hàm</th>
                                            <th>Học vị</th>
                                            <th>Khoa</th>
                                            <th>Đơn vị công tác</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {
                                            this.state.editingData.giangVienPhanBien.length > 0 ? this.state.editingData.giangVienPhanBien.map((record, index) => (
                                                <tr key={ index }>
                                                    <td className='table-dropdown-menu-wrapper'>
                                                        <span className='table-dropdown-menu-toggle' onClick={ (e) => this.removeGVPB(e, record) }><i className="fas fa-times"></i></span>
                                                    </td>
                                                    <td>{ index + 1 }</td>
                                                    <td>{ record.maGiangVien }</td>
                                                    <td>{ record.hoVaTen }</td>
                                                    <td>{ record.hocHam }</td>
                                                    <td>{ record.hocVi}</td>
                                                    <td>{ record.khoa && record.khoa.tenKhoa }</td>
                                                    <td>{ record.donViCongTac }</td>
                                                </tr>
                                            )) :
                                            <tr>
                                                <td className='text-center' colSpan={ 8 }>(Chưa có dữ liệu)</td>
                                            </tr>
                                        }
                                    </tbody>
                                </table>

                                {/* Hội đồng bảo vệ */}

                                <div className='subsection__header'>
                                    <h4>Danh sách hội đồng bảo vệ đồ án</h4>
                                    {
                                        (this.state.editingData.hoiDongBaoVe.length === 0 || this.state.editingData.hoiDongBaoVe.length < this.state.quyDinh.soTVHDToiDa) &&
                                        <span className='button' onClick={ () => this.showPopup('selectHDBV') }>Thêm mới</span> 
                                    }
                                </div>

                                <table className='table'>
                                <thead>
                                        <tr>
                                            <th></th>
                                            <th>STT</th>
                                            <th>MSCB</th>
                                            <th>Họ và tên</th>
                                            <th>Học hàm</th>
                                            <th>Học vị</th>
                                            <th>Khoa</th>
                                            <th>Đơn vị công tác</th>
                                            <th>Chức vụ trong hội đồng</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {
                                            this.state.editingData.hoiDongBaoVe.length > 0 ? this.state.editingData.hoiDongBaoVe.map((record, index) => (
                                                <tr key={ index }>
                                                    <td className='table-dropdown-menu-wrapper'>
                                                        <span className='table-dropdown-menu-toggle' onClick={ (e) => this.removeHDBV(e, record) }><i className="fas fa-times"></i></span>
                                                    </td>
                                                    <td>{ index + 1 }</td>
                                                    <td>{ record.maGiangVien }</td>
                                                    <td>{ record.hoVaTen }</td>
                                                    <td>{ record.hocHam }</td>
                                                    <td>{ record.hocVi}</td>
                                                    <td>{ record.khoa && record.khoa.tenKhoa }</td>
                                                    <td>{ record.donViCongTac }</td>
                                                    <td>
                                                        <select className='form-input-outline' value={record.chucVu} onChange={e => this.changeChucVuHDBV(e, record) }>
                                                            {
                                                                ChucVuHDBV.map((record, index) => (
                                                                    <option key={ index } value={ record }>{ record }</option>
                                                                ))
                                                            }
                                                        </select>
                                                    </td>
                                                </tr>
                                            )) :
                                            <tr>
                                                <td className='text-center' colSpan={ 9 }>(Chưa có dữ liệu)</td>
                                            </tr>
                                        }
                                    </tbody>
                                </table>

                                {/* Kết quả đánh giá */}

                                <div className='subsection__header'>
                                    <h4>Kết quả đánh giá đồ án</h4>
                                </div>

                                <h5 className='section-h5'>Nhận xét</h5>
                                <h6>Nhận xét của giảng viên hướng dẫn đồ án</h6>
                                <table className='table'>
                                    <thead>
                                        <tr>
                                            <th>STT</th>
                                            <th>MSCB</th>
                                            <th>Họ và tên</th>
                                            <th>Nội dung nhận xét</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {
                                            this.state.editingData.giangVienHuongDan.length > 0 ? this.state.editingData.giangVienHuongDan.map((record, index) => (
                                                <tr key={ index }>
                                                    <td>{ index + 1 }</td>
                                                    <td>{ record.maGiangVien }</td>
                                                    <td>{ record.hoVaTen }</td>
                                                    <td>
                                                        <textarea className='form-input-outline' value={record.nhanXet} onChange={ e => this.changeNhanXetGVHD(e, record) }></textarea>
                                                    </td>
                                                </tr>
                                            )) :
                                            <tr>
                                                <td className='text-center' colSpan={ 4 }>(Chưa có dữ liệu)</td>
                                            </tr>
                                        }
                                    </tbody>
                                </table>

                                <h6>Nhận xét của giảng viên phản biện đồ án</h6>
                                <table className='table'>
                                    <thead>
                                        <tr>
                                            <th>STT</th>
                                            <th>MSCB</th>
                                            <th>Họ và tên</th>
                                            <th>Nội dung nhận xét</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {
                                            this.state.editingData.giangVienPhanBien.length > 0 ? this.state.editingData.giangVienPhanBien.map((record, index) => (
                                                <tr key={ index }>
                                                    <td>{ index + 1 }</td>
                                                    <td>{ record.maGiangVien }</td>
                                                    <td>{ record.hoVaTen }</td>
                                                    <td>
                                                        <textarea className='form-input-outline' value={record.nhanXet} onChange={ e => this.changeNhanXetGVPB(e, record) }></textarea>
                                                    </td>
                                                </tr>
                                            )) :
                                            <tr>
                                                <td className='text-center' colSpan={ 4 }>(Chưa có dữ liệu)</td>
                                            </tr>
                                        }
                                    </tbody>
                                </table>

                                <h6>Nhận xét chung của hội đồng bảo vệ đồ án</h6>
                                <div className='form-group'>
                                    <textarea className={ this.isValidInputField('nhanXetChung') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập nhận xét chung của hội đồng bảo vệ đồ án' value={ this.state.editingData.nhanXetChung } onChange={ e => this.changeEditingData(e, 'nhanXetChung') } onFocus={ () => this.hideAlert() }></textarea>
                                </div>

                                <h5 className='section-h5'>Điểm số</h5>
                                <h6>Điểm số thành phần</h6>
                                <table className='table'>
                                    <thead>
                                        <tr>
                                            <th>STT</th>
                                            <th>MSCB</th>
                                            <th>Họ và tên</th>
                                            <th>Vai trò</th>
                                            <th>Điểm</th>
                                            <th>Hệ số</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {
                                            result.length > 0 ? result.map((record, index) => (
                                                <tr key={ index }>
                                                    <td>{ index + 1 }</td>
                                                    <td>{ record.maGiangVien }</td>
                                                    <td>{ record.hoVaTen }</td>
                                                    <td>{ record.vaiTro }</td>
                                                    <td>
                                                        <input type='number' className='form-input-outline' min="1" max="7" value={record.diem} onChange={ e => this.changeDiemHDBV(e, record) } />
                                                    </td>
                                                    <td>{ record.heSo }</td>
                                                </tr>
                                            )) :
                                            <tr>
                                                <td className='text-center' colSpan={ 6 }>(Chưa có dữ liệu)</td>
                                            </tr>
                                        }
                                    </tbody>
                                </table>

                                <h6>Điểm tổng kết bằng số (Đã làm tròn)</h6>
                                <div className='form-group'>
                                    <input type='text' className='form-input-disabled' value={ this.state.editingData.diemTongKet } disabled />
                                </div>

                                <h6>Điểm tổng kết bằng chữ</h6>
                                <div className='form-group'>
                                    <input type='text' className='form-input-disabled' value={ standardizeScore(this.state.editingData.diemTongKet) } disabled />
                                </div>
                            </form>
                        </div>
                    </div>

                    <footer className='section__footer'>
                        <div className='section__header-right'>
                            <span className='button'>
                                <Link to='/do-an'>
                                    <i className='fas fa-arrow-left'></i>&nbsp;&nbsp;Trở về
                                </Link>
                            </span>

                            <span className='button' onClick={ () => this.submit() }>
                                <i className='fas fa-save'></i>&nbsp;&nbsp;Lưu lại
                            </span>
                        </div>
                    </footer>

                    { this.state.showPopup.selectSVTH && <div className='popup-wrapper'>
                        <div className='popup'>
                            <header className='popup__header'>
                                <div className='popup-header__top-section'>
                                    <i className='fas fa-times close-button' onClick={ () => this.closePopup('selectSVTH') }></i>
                                </div>
                            </header>

                            <div className='popup__body'>
                                <div className='form-group'>
                                    <input type='text' className='form-input-outline' placeholder='Nhập từ khóa cần tìm kiếm' value={ this.state.svthKeyword } onChange={ e => this.setState({ svthKeyword: e.target.value }, this.fetchSinhVien) } />
                                </div>

                                <table className='table' style={{ color: '#111' }}>
                                    <thead>
                                        <tr>
                                            <th>MSSV</th>
                                            <th>Họ và tên</th>
                                            <th>Ngày sinh</th>
                                            <th>Lớp</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {
                                            this.state.sinhVien.map((record, index) => (
                                                <tr key={ index } onClick={ () => this.selectSVTH(record) }>
                                                    <td>{ record.maSinhVien }</td>
                                                    <td>{ record.hoVaTen }</td>
                                                    <td>{ formatDateString(record.ngaySinh, false) }</td>
                                                    <td>{ record.lop.tenLop }</td>
                                                </tr>
                                            ))
                                        }
                                    </tbody>
                                </table>
                            </div>

                            <footer className='popup__footer'>
                                <span className='popup-button-default' onClick={ () => this.closePopup('selectSVTH') }>Hủy bỏ</span>
                            </footer>
                        </div>
                    </div> }

                    { this.state.showPopup.selectGVHD && <div className='popup-wrapper'>
                        <div className='popup'>
                            <header className='popup__header'>
                                <div className='popup-header__top-section'>
                                    <i className='fas fa-times close-button' onClick={ () => this.closePopup('selectGVHD') }></i>
                                </div>
                            </header>

                            <div className='popup__body'>
                                <div className='form-group'>
                                    <input type='text' className='form-input-outline' placeholder='Nhập từ khóa cần tìm kiếm' value={ this.state.gvhdKeyword } onChange={ e => this.setState({ gvhdKeyword: e.target.value }, this.fetchGiangVien) } />
                                </div>

                                <table className='table' style={{ color: '#111' }}>
                                    <thead>
                                        <tr>
                                            <th>MSCB</th>
                                            <th>Họ và tên</th>
                                            <th>Khoa</th>
                                            <th>Đơn vị công tác</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {
                                            gvhd.map((record, index) => (
                                                <tr key={ index } onClick={ () => this.selectGVHD(record) }>
                                                    <td>{ record.maGiangVien }</td>
                                                    <td>{ record.hoVaTen }</td>
                                                    <td>{ record.khoa && record.khoa.tenKhoa }</td>
                                                    <td>{ record.donViCongTac }</td>
                                                </tr>
                                            ))
                                        }
                                    </tbody>
                                </table>
                            </div>

                            <footer className='popup__footer'>
                                <span className='popup-button-default' onClick={ () => this.closePopup('selectGVHD') }>Hủy bỏ</span>
                            </footer>
                        </div>
                    </div> }

                    { this.state.showPopup.selectGVPB && <div className='popup-wrapper'>
                        <div className='popup'>
                            <header className='popup__header'>
                                <div className='popup-header__top-section'>
                                    <i className='fas fa-times close-button' onClick={ () => this.closePopup('selectGVPB') }></i>
                                </div>
                            </header>

                            <div className='popup__body'>
                                <div className='form-group'>
                                    <input type='text' className='form-input-outline' placeholder='Nhập từ khóa cần tìm kiếm' value={ this.state.gvpbKeyword } onChange={ e => this.setState({ gvpbKeyword: e.target.value }, this.fetchGiangVien) } />
                                </div>

                                <table className='table' style={{ color: '#111' }}>
                                    <thead>
                                        <tr>
                                            <th>MSCB</th>
                                            <th>Họ và tên</th>
                                            <th>Khoa</th>
                                            <th>Đơn vị công tác</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {
                                            gvpb.map((record, index) => (
                                                <tr key={ index } onClick={ () => this.selectGVPB(record) }>
                                                    <td>{ record.maGiangVien }</td>
                                                    <td>{ record.hoVaTen }</td>
                                                    <td>{ record.khoa && record.khoa.tenKhoa }</td>
                                                    <td>{ record.donViCongTac }</td>
                                                </tr>
                                            ))
                                        }
                                    </tbody>
                                </table>
                            </div>

                            <footer className='popup__footer'>
                                <span className='popup-button-default' onClick={ () => this.closePopup('selectGVPB') }>Hủy bỏ</span>
                            </footer>
                        </div>
                    </div> }

                    { this.state.showPopup.selectHDBV && <div className='popup-wrapper'>
                        <div className='popup'>
                            <header className='popup__header'>
                                <div className='popup-header__top-section'>
                                    <i className='fas fa-times close-button' onClick={ () => this.closePopup('selectHDBV') }></i>
                                </div>
                            </header>

                            <div className='popup__body'>
                                <div className='form-group'>
                                    <input type='text' className='form-input-outline' placeholder='Nhập từ khóa cần tìm kiếm' value={ this.state.hdbvKeyword } onChange={ e => this.setState({ hdbvKeyword: e.target.value }, this.fetchGiangVien) } />
                                </div>

                                <table className='table' style={{ color: '#111' }}>
                                    <thead>
                                        <tr>
                                            <th>MSCB</th>
                                            <th>Họ và tên</th>
                                            <th>Khoa</th>
                                            <th>Đơn vị công tác</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {
                                            this.state.giangVien.map((record, index) => (
                                                <tr key={ index } onClick={ () => this.selectHDBV(record) }>
                                                    <td>{ record.maGiangVien }</td>
                                                    <td>{ record.hoVaTen }</td>
                                                    <td>{ record.khoa && record.khoa.tenKhoa }</td>
                                                    <td>{ record.donViCongTac }</td>
                                                </tr>
                                            ))
                                        }
                                    </tbody>
                                </table>
                            </div>

                            <footer className='popup__footer'>
                                <span className='popup-button-default' onClick={ () => this.closePopup('selectGVPB') }>Hủy bỏ</span>
                            </footer>
                        </div>
                    </div> }
                </section>
            </Fragment>
        )
    }
}

const mapStateToProps = state => ({
    user: state.user
})
  
export default connect(mapStateToProps, null)(DoAnForCreate)