import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import apiRoutes from '../../routes/apis'
import axios from 'axios'
import { formatDateString, getValue } from '../../utils'

class QuyDinhForView extends Component {
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
        const url = apiRoutes.quyDinh.getById + '/' + id

        try {
            const response = await axios.get(url)

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

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
                    <span className='breadcrumb'><Link to='/quy-dinh'>Quản lý quy định</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Xem thông tin quy định</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Xem thông tin quy định</h3>
                            <p className='section__subtitle'>Xem thông tin chi tiết của quy định</p>
                        </div>
                    </header>

                    <div className='section__body'>
                        <div className='section-body-main'>
                            <form autoComplete='off'>
                                <div className='form-group'>
                                    <label className='form-label'>Mã quy định</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.maQuyDinh) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Tên quy định</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.tenQuyDinh) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Thời gian bắt đầu hiệu lực</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(formatDateString(this.state.data.thoiGianBatDauHieuLuc)) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số sinh viên thực hiện đồ án tối thiểu</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soSVTHToiThieu) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số sinh viên thực hiện đồ án tối đa</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soSVTHToiDa) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số giảng viên hướng dẫn đồ án tối thiểu</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soGVHDToiThieu) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số giảng viên hướng dẫn đồ án tối đa</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soGVHDToiDa) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số giảng viên phản biện đồ án tối thiểu</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soGVPBToiThieu) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số giảng viên phản biện đồ án tối đa</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soGVPBToiDa) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số thành viên hội đồng bảo vệ tối thiểu</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soTVHDToiThieu) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số thành viên hội đồng bảo vệ đồ án tối đa</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soTVHDToiDa) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số chủ tịch hội đồng bảo vệ đồ án tối đa</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soCTHDToiThieu) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số chủ tịch hội đồng bảo vệ đồ án tối thiểu</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soCTHDToiDa) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số thư ký hội đồng bảo vệ đồ án tối đa</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soTKHDToiThieu) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số thư ký hội đồng bảo vệ đồ án tối thiểu</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soTKHDToiDa) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số ủy viên hội đồng bảo vệ đồ án tối đa</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soUVHDToiThieu) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số ủy viên hội đồng bảo vệ đồ án tối thiểu</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soUVHDToiDa) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số chữ số thập phân</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.soChuSoThapPhan) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Điểm số tối thiểu</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.diemSoToiThieu) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Điểm số tối đa</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.diemSoToiDa) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Hệ số điểm của giảng viên hướng dẫn đồ án</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.heSoGVHD) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Hệ số điểm của giảng viên phản biện đồ án</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.heSoGVPB) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Hệ số điểm của thành viên hội đồng bảo vệ đồ án</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.heSoTVHD) } disabled />
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
                                <Link to='/quy-dinh'>
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
  
export default connect(mapStateToProps, null)(QuyDinhForView)