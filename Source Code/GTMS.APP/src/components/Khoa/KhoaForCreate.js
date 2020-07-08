import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import axios from 'axios'
import apiRoutes from '../../routes/apis'

class KhoaForCreate extends Component {
    constructor (props) {
      super(props)
      this.state = {
          editingData: {
              tenKhoa: '',
              tenVietTat: ''
          },
          errors: null
      }
    }

    componentDidMount() {
        window.scrollTo(0, 0)
        axios.defaults.headers.common['Authorization'] = 'AUTH_TOKEN'
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
        const { tenKhoa, tenVietTat } = this.state.editingData

        if (tenKhoa !== '' && tenVietTat !== '') {
            const url = apiRoutes.khoa.create
            const data = this.state.editingData

            return axios.post(url, data)
            .then(response => {
                this.props.history.push('/khoa')
            })
            .catch(error => {
                const { errors } = error.response.data.result
                this.setState({ errors })
            })
        }
        else {
            const errors = {}

            if (tenKhoa === '') {
                errors['tenKhoa'] = ['tenKhoa is required!']
            }

            if (tenVietTat === '') {
                errors['tenVietTat'] = ['tenVietTat is required!']
            }

            this.setState({ errors })
        }
    }

    transformErrorMessage(rawMessage) {
        let subject = rawMessage.split(' ')[0].toLowerCase()
        let message = ''

        switch (subject) {
            case 'tenkhoa':
                subject = 'Tên khoa'
                break
            
            case 'tenviettat':
                subject = 'Tên viết tắt của khoa'
                break
        }

        if (rawMessage.includes('require')) {
            message = subject + ' là bắt buộc và không được bỏ trống!'
        }

        if (rawMessage.includes('duplicate')) {
            message = subject + ' đã bị trùng!'
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
                    <span className='breadcrumb'><Link to='/khoa'>Quản lý khoa</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Thêm khoa mới</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Thêm khoa mới</h3>
                            <p className='section__subtitle'>Thêm khoa mới vào hệ thống</p>
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
                                    <label className='form-label'>Tên khoa</label>
                                    <input type='text' className={ this.isValidInputField('tenKhoa') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập tên khoa' value={ this.state.editingData.tenKhoa } onChange={ e => this.changeEditingData(e, 'tenKhoa') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Tên viết tắt của khoa</label>
                                    <input type='text' className={ this.isValidInputField('tenVietTat') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập tên viết tắt của khoa' value={ this.state.editingData.tenVietTat } onChange={ e => this.changeEditingData(e, 'tenVietTat') } onFocus={ () => this.hideAlert() } />
                                </div>
                            </form>
                        </div>
                    </div>

                    <footer className='section__footer'>
                        <div className='section__header-right'>
                            <span className='button'>
                                <Link to='/khoa'>
                                    <i className='fas fa-arrow-left'></i>&nbsp;&nbsp;Trở về
                                </Link>
                            </span>

                            <span className='button' onClick={ () => this.submit() }>
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
  
export default connect(mapStateToProps, null)(KhoaForCreate)