import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import apiRoutes from '../../routes/apis'
import axios from 'axios'
import { formatDateString, getValue } from '../../utils'

class CaiDatForView extends Component {
    constructor (props) {
      super(props)
      this.state = {
          data: {},
          showPopup: {
              confirm: false,
          },
          showPopupErrorMessage: false
      }
    }

    componentDidMount() {
        window.scrollTo(0, 0)
        this.fetchData()
    }

    async fetchData() {
        const { id } = this.props.match.params
        const url = apiRoutes.caiDat.getAll

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

    showPopup(name) {
        const showPopup = { ...this.state.showPopup }
        showPopup[name] = true

        this.setState({ showPopup })
    }

    closePopup(name) {
        const showPopup = { ...this.state.showPopup }
        showPopup[name] = false

        this.setState({ 
            showPopup, 
            showPopupErrorMessage: false
        })
    }

    restore() {
        const url = apiRoutes.caiDat.restore

        return axios.put(url)
            .then(response => {
                this.closePopup('confirm')
                this.fetchData()
            })
            .catch(error => {
                this.setState({ showPopupErrorMessage: true })
            })
    }

    render() {
        return (
            <Fragment>
                <section className='breadcrumbs'>
                <span className='breadcrumb-home'><Link to='/'>Graduation Thesis Management System (GTMS)</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Quản lý cài đặt</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Xem thông tin cài đặt</h3>
                            <p className='section__subtitle'>Xem thông tin cài đặt chi tiết</p>
                        </div>
                    </header>

                    <div className='section__body'>
                        <div className='section-body-main'>
                            <form autoComplete='off'>
                                <div className='form-group'>
                                    <label className='form-label'>Tên đơn vị chủ quản</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.tenDonViChuQuan) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Tên khoa</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(this.state.data.tenKhoa) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Thời gian cập nhật</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(formatDateString(this.state.data.thoiGianCapNhat)) } disabled />
                                </div>
                            </form>
                        </div>
                    </div>

                    <footer className='section__footer'>
                        <div className='section__header-right'>
                            <span className='button' onClick={ () => this.showPopup('confirm') }>
                                Khôi phục giá trị mặc định
                            </span>

                            <span className='button'>
                                <Link to='/cai-dat/cap-nhat'>
                                    Cập nhật lại
                                </Link>
                            </span>
                        </div>
                    </footer>

                    { this.state.showPopup.confirm && <div className='popup-wrapper'>
                        <div className='popup'>
                            <header className='popup__header'>
                                <div className='popup-header__top-section'>
                                    <i className='fas fa-times close-button' onClick={ () => this.closePopup('confirm') }></i>
                                </div>
        
                                <div className='popup-header__main-section'>
                                    <img src='/images/restore.png' className='popup__image' />
                                    <h4 className='popup-header__title'>Khôi phục lại giá trị cài đặt mặc định</h4>
                                </div>
                            </header>

                            <div className='popup__body'>
                                { !this.state.showPopupErrorMessage && <p>Bạn có chắc chắn muốn khôi phục lại giá trị cài đặt mặc định hay không?</p> }
                                { this.state.showPopupErrorMessage && <p>Khôi phục lại giá trị cài đặt mặc định không thành công!</p> }
                            </div>

                            <footer className='popup__footer'>
                                <span className='popup-button-default' onClick={ () => this.closePopup('confirm') }>Hủy bỏ</span>
                                <span className='popup-button-danger' onClick={ () => this.restore() }>Xác nhận</span>
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
  
export default connect(mapStateToProps, null)(CaiDatForView)