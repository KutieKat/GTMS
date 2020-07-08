import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import apiRoutes from '../../routes/apis'
import axios from 'axios'
import { getValue, formatDateString } from '../../utils'

class GiangVienForView extends Component {
    constructor (props) {
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
        const url = apiRoutes.giangVien.getById + '/' + id

        try {
            const response = await axios.get(url)

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

                console.log(formatDateString(data.ngaySinh, false))

                this.setState({ data })
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
            <Fragment>
                <section className='breadcrumbs'>
                <span className='breadcrumb-home'><Link to='/'>Graduation Thesis Management System (GTMS)</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb'><Link to='/giang-vien'>Quản lý giảng viên</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Xem thông tin giảng viên</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Xem thông tin giảng viên</h3>
                            <p className='section__subtitle'>Xem thông tin chi tiết của giảng viên</p>
                        </div>
                    </header>

                    <div className='section__body'>
                        <div className='section-body-main'>
                            <form autoComplete='off'>
                                <div className='form-group'>
                                    <label className='form-label'>Mã giảng viên</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.maGiangVien) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Họ và tên</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.hoVaTen) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Giới tính</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(this.state.data.gioiTinh) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Ngày sinh</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(formatDateString(this.state.data.ngaySinh, false)) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Email</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(this.state.data.email) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số điện thoại</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(this.state.data.soDienThoai) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'> Quê quán</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(this.state.data.queQuan) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Địa chỉ</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(this.state.data.diaChi) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Đơn vị công tác</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(this.state.data.donViCongTac) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Học hàm</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(this.state.data.hocHam, 'Chưa có học hàm') } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Học vị</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(this.state.data.hocVi, 'Chưa có học vị') } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Ngày tạo</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(formatDateString(this.state.data.thoiGianTao)) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Ngày cập nhật</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(formatDateString(this.state.data.thoiGianCapNhat)) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Trạng thái</label>
                                    <select className='form-select-disabled' value={ getValue(this.state.data.trangThai) } disabled>
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
                                <Link to='/giang-vien'>
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
  
export default connect(mapStateToProps, null)(GiangVienForView)