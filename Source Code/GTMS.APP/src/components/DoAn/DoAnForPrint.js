import React, { Component } from 'react'
import moment from 'moment'
import { formatDateString } from '../../utils'
import apiRoutes from '../../routes/apis'
import axios from 'axios'

class DoAnForPrint extends Component {
    constructor(props) {
        super(props)
        this.state = {
            settings: {},
            sinhVien: [],
            giangVien: []
        }
    }

    componentDidMount() {
        this.fetchData()
    }

    async fetchData() {
        this.fetchSettings()
        this.fetchSinhVien()
        this.fetchGiangVien()
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

    render() {
        console.log('DATA: ', this.props.data)
        return (
            <div className='printing-page'>
                <div className='printing-page__header'>
                    <div className='printing-page__header-top'>
                        <div className='print-page__header-top-left'>
                        <p>{ this.state.settings && this.state.settings.tenDonViChuQuan }</p>
                            <p>{ this.state.settings && this.state.settings.tenKhoa }</p>
                            <div className='print-page__splitter'></div>
                        </div>

                        <div className='printing-page__header-top-right'>
                            <p>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</p>
                            <p>Độc lập - Tự do - Hạnh phúc</p>
                            <div className='print-page__splitter'></div>
                        </div>
                    </div>

                    <div className='printing-page__main'>
                        <h1 className='printing-page-title'>DANH SÁCH CÁC ĐỒ ÁN HIỆN CÓ TRONG HỆ THỐNG</h1>
                        <h2 className='printing-page-subtitle'>Graduation Thesis Management System (GTMS)</h2>
                    </div>
                </div>

                <div className='printing-page__body'>
                    <table className='printing-page-table'>
                        <thead>
                            <tr>
                                <th>STT</th>
                                <th>Tên đồ án</th>
                                <th>Hướng nghiên cứu</th>
                                <th>Nhóm sinh viên thực hiện</th>
                                <th>Thời gian báo cáo</th>
                                <th>Địa điểm báo cáo</th>
                                <th>GVHD</th>
                                <th>GVPB</th>
                                <th>HDBV</th>
                                <th>Điểm tổng kết</th>
                            </tr>
                        </thead>

                        <tbody>
                            {
                                this.props.data.length > 0 && this.props.data.map((record, index) => (
                                    <tr key={ index }>
                                        <td>{ index + 1 }</td>
                                        <td>{ record.tenDoAn }</td>
                                        <td>{ record.huongNghienCuu.tenHuongNghienCuu }</td>
                                        <td>
                                            {
                                                this.state.sinhVien.map(r => {
                                                    if (r.doAn && r.doAn.maDoAn === record.maDoAn) {
                                                        return <div>{ r.hoVaTen } ({ r.maSinhVien })</div>
                                                    }
                                                })
                                            }
                                        </td>
                                        <td>{ formatDateString(record.thoiGianBaoCao, false) }</td>
                                        <td>{ record.diaDiemBaoCao }</td>
                                        <td>
                                            {
                                                record.huongNghienCuu.doAn[0].huongDanDoAn.map((d, index) => {
                                                    let record = this.state.giangVien.find(r => r.maGiangVien === d.maGiangVien)
                                                    return <div key={ index }>{ record.hoVaTen }</div>
                                                })
                                            }
                                        </td>
                                        <td>
                                            {
                                                record.huongNghienCuu.doAn[0].phanBienDoAn.map((d, index) => {
                                                    let record = this.state.giangVien.find(r => r.maGiangVien === d.maGiangVien)
                                                    return <div key={ index }>{ record.hoVaTen }</div>
                                                })
                                            }
                                        </td>
                                        <td>
                                            {
                                                record.huongNghienCuu.doAn[0].thanhVienHDBV.map((d, index) => {
                                                    let record = this.state.giangVien.find(r => r.maGiangVien === d.maGiangVien)
                                                    record['chucVu'] = d.chucVu

                                                    return <div key={ index }><strong>{ record.chucVu }:</strong> { record.hoVaTen }</div>
                                                })
                                            }
                                        </td>
                                        <td className='text-center'>{ record.diemTongKet }</td>
                                    </tr>
                                ))
                            }
                        </tbody>
                    </table>
                </div>

                <div className='printing-page__footer'>
                    <div className='printing-page__footer-top'>
                        <div className='printing-page__footer-top-left'>
                            <p>Danh sách có tất cả { this.props.data.length } kết quả</p>
                        </div>

                        <div className='printing-page__footer-top-right'>
                            <p>Thành phố Hồ Chí Minh, ngày { moment().format('DD') } tháng { moment().format('MM') } năm { moment().format('YYYY') }</p>
                        </div>
                    </div>

                    <div className='printing-page__footer-main'>
                        <div className='printing-page__footer-main-left'>
                            <p className='printing-page__job-title'>NGƯỜI DUYỆT</p>
                            <p className='printing-page__signature'>(Ký và ghi rõ họ tên)</p>
                        </div>

                        <div className='printing-page__footer-main-center'>
                        </div>

                        <div className='printing-page__footer-main-right'>
                            <p className='printing-page__job-title'>NGƯỜI LẬP</p>
                            <p className='printing-page__signature'>(Ký và ghi rõ họ tên)</p>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

export default DoAnForPrint