import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import apiRoutes from '../../routes/apis'
import axios from 'axios'
import { getValue, formatDateString } from '../../utils'

class HocKyForView extends Component {
    constructor(props) {
        super(props)
        this.state = {
            data: {}
        }
    }

    componentDidMount() {
        window.scrollTo(0, 0)
        this.fetchData()
    }

    async fetchData() {
        const { id } = this.props.match.params
        const url = apiRoutes.hocKy.getById + '/' + id

        try {
            const response = await axios.get(url)

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

                this.setState({ data })
            } else {
                throw new Error(response.errors)
            }
        } catch (error) {
            console.error(error)
        }
    }

    render() {
        return (
            <Fragment>
                <section className='breadcrumbs'>
                    <span className='breadcrumb-home'><Link to='/'>Graduation Thesis Management System (GTMS)</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb'><Link to='/hoc-ky'>Quản lý học kỳ</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Xem thông tin học kỳ</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Xem thông tin học kỳ</h3>
                            <p className='section__subtitle'>Xem thông tin chi tiết của học kỳ</p>
                        </div>
                    </header>

                    <div className='section__body'>
                        <div className='section-body-main'>
                            <form autoComplete='off'>
                                <div className='form-group'>
                                    <label className='form-label'>Mã học kỳ</label>
                                    <input type='text' className='form-input-disabled' value={getValue(this.state.data.maHocKy)} disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Tên học kỳ</label>
                                    <input type='text' className='form-input-disabled' value={getValue(this.state.data.tenHocKy)} disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Năm học</label>
                                    <input type='text' className='form-input-disabled' value={getValue(this.state.data.namHoc)} disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Thời gian bắt đầu</label>
                                    <input className='form-input-disabled' type='text' value={getValue(formatDateString(this.state.data.thoiGianBatDau))} disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Thời gian kết thúc</label>
                                    <input className='form-input-disabled' type='text' value={getValue(formatDateString(this.state.data.thoiGianKetThuc))} disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Ngày tạo</label>
                                    <input className='form-input-disabled' type='text' value={getValue(formatDateString(this.state.data.thoiGianTao))} disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Ngày cập nhật</label>
                                    <input className='form-input-disabled' type='text' value={getValue(formatDateString(this.state.data.thoiGianCapNhat))} disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Trạng thái</label>
                                    <select className='form-select-disabled' value={getValue(this.state.data.trangThai)} disabled>
                                        <option value='-1'>Đã bị xóa</option>
                                        <option value='1'>Đang được hiển thị</option>
                                    </select>
                                </div>
                            </form>
                        </div>
                    </div>

                    <footer className='section__footer'>
                        <div className='section__header-right'>
                            <span className='button'>
                                <Link to='/hoc-ky'>
                                    <i className='fas fa-arrow-left'></i>&nbsp;&nbsp;Trở về
                                </Link>
                            </span>
                        </div>
                    </footer>
                </section>
            </Fragment>
        )
    }
}


const mapStateToProps = state => ({
    user: state.user
})

export default connect(mapStateToProps, null)(HocKyForView)