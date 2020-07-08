import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import moment from 'moment'
import { Link } from 'react-router-dom'
import axios from 'axios'
import apiRoutes from '../../routes/apis'
import { getValue, formatDateString } from '../../utils'

class HocKyForUpdate extends Component {
    constructor(props) {
        super(props)
        this.state = {
            data: {},
            editingData: {},
            errors: null
        }
    }

    componentDidMount() {
        window.scrollTo(0, 0)
    }

    changeEditingData(e, fieldName) {
        const value = e.target.value
        const editingData = {...this.state.editingData }

        if (typeof value === 'number') {
            value = parseInt(value)
        }

        editingData[fieldName] = value

        this.setState({ editingData })
    }

    submit() {
        const { matKhauCu, matKhauMoi, xacNhanMatKhauMoi } = this.state.editingData

        if (matKhauCu !== '' && matKhauMoi !== '' && xacNhanMatKhauMoi !== '' && matKhauMoi === xacNhanMatKhauMoi) {
            const { id } = 1
            const url = apiRoutes.taiKhoan.changePassword + '/' + id
            const data = this.state.editingData

            return axios.put(url, data)
            .then(response => {
                this.props.history.push('/')
            })
            .catch(error => {
                const { errors } = error.response.data.result
                this.setState({ errors })
            })
        }
        else {
            const errors = {}

            if (matKhauCu === '') {
                errors['matKhauCu'] = ['matKhauCu is required!']
            }

            if (matKhauMoi === '') {
                errors['matKhauMoi'] = ['matKhauMoi is required!']
            }

            if (xacNhanMatKhauMoi === '') {
                errors['xacNhanMatKhauMoi'] = ['xacNhanMatKhauMoi is required!']
            }

            if (matKhauMoi !== xacNhanMatKhauMoi) {
                errors['xacNhanMatKhauMoi'] = ['xacNhanMatKhauMoi is not equal!']
            }

            this.setState({ errors })
        }
    }

    transformErrorMessage(rawMessage) {
        let subject = rawMessage.split(' ')[0].toLowerCase()
        let message = ''

        switch (subject) {
            case 'matkhaucu':
                subject = 'Mật khẩu cũ'
                break
            
            case 'matkhaumoi':
                subject = 'Mật khẩu mới'
                break
            
            case 'xacnhanmatkhaumoi':
                subject = 'Xác nhận mật khẩu mới'
                break
        }

        if (rawMessage.includes('require')) {
            message = subject + ' là bắt buộc và không được bỏ trống!'
        }

        if (rawMessage.includes('duplicate')) {
            message = subject + ' đã bị trùng!'
        }

        if (rawMessage.includes('not equal')) {
            message = subject + ' chưa trùng khớp!'
        }

        return message
    }

    hideAlert() {
        this.setState({ errors: null })
    }

    isValidInputField(inputFieldName) {
        return this.state.errors && Object.keys(this.state.errors).map(key => key.toLowerCase()).indexOf(inputFieldName.toLowerCase()) > -1
    }

    render() {
        return (
            <Fragment>
                <section className='breadcrumbs'>
                    <span className='breadcrumb-home'><Link to='/'>Graduation Thesis Management System (GTMS)</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='/hoc-ky'>Thay đổi mật khẩu</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Thay đổi mật khẩu</h3>
                            <p className='section__subtitle'>Thay đổi mật khẩu của tài khoản</p>
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
                                <div className='form-group'>
                                    <label className='form-label'>Mật khẩu cũ</label>
                                    <input className={ this.isValidInputField('matKhauCu') ? 'form-input-alert' : 'form-input-outline' } type='password' value={this.state.editingData.matKhauCu} onChange={e => this.changeEditingData(e, 'matKhauCu')} onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Mật khẩu mới</label>
                                    <input className={ this.isValidInputField('matKhauMoi') ? 'form-input-alert' : 'form-input-outline' } type='password' value={this.state.editingData.matKhauMoi} onChange={e => this.changeEditingData(e, 'matKhauMoi')} onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Xác nhận mật khẩu mới</label>
                                    <input className={ this.isValidInputField('xacNhanMatKhauMoi') ? 'form-input-alert' : 'form-input-outline' } type='password' value={this.state.editingData.xacNhanMatKhauMoi} onChange={e => this.changeEditingData(e, 'xacNhanMatKhauMoi')} onFocus={ () => this.hideAlert() } />
                                </div>
                            </form>
                        </div>
                    </div>

                    <footer className='section__footer'>
                        <div className='section__header-right'>
                            <span className='button'>
                                <Link to='/'>
                                    <i className='fas fa-arrow-left'></i>&nbsp;&nbsp;Trở về
                                </Link>
                            </span>

                            <span className='button' onClick={() => this.submit()}>
                                <i className='fas fa-save'></i>&nbsp;&nbsp;Lưu lại
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

export default connect(mapStateToProps, null)(HocKyForUpdate)