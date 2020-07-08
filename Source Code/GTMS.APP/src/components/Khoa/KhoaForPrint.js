import React, { Component } from 'react'
import moment from 'moment'
import apiRoutes from '../../routes/apis'
import axios from 'axios'

class KhoaForPrint extends Component {
    constructor(props) {
        super(props)
        this.state = {
            settings: {}
        }
    }

    componentDidMount() {
        this.fetchSettings()
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
                        <h1 className='printing-page-title'>DANH SÁCH CÁC KHOA HIỆN CÓ TRONG HỆ THỐNG</h1>
                        <h2 className='printing-page-subtitle'>Graduation Thesis Management System (GTMS)</h2>
                    </div>
                </div>

                <div className='printing-page__body'>
                    <table className='printing-page-table'>
                        <thead>
                            <tr>
                                <th>STT</th>
                                <th>Tên khoa</th>
                                <th>Tên viết tắt của khoa</th>
                            </tr>
                        </thead>

                        <tbody>
                            {
                                this.props.data.length > 0 && this.props.data.map((record, index) => (
                                    <tr key={ index }>
                                        <td>{ index + 1 }</td>
                                        <td>{ record.tenKhoa }</td>
                                        <td>{ record.tenVietTat }</td>
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
                            {/* <p className='printing-page__full-name'></p> */}
                        </div>

                        <div className='printing-page__footer-main-center'>
                        </div>

                        <div className='printing-page__footer-main-right'>
                            <p className='printing-page__job-title'>NGƯỜI LẬP</p>
                            <p className='printing-page__signature'>(Ký và ghi rõ họ tên)</p>
                            {/* <p className='printing-page__full-name'></p> */}
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

export default KhoaForPrint