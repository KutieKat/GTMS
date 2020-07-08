import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import axios from 'axios'
import apiRoutes from '../../routes/apis'
import { formatDateString, getValue } from '../../utils'

class HuongNghienCuuForUpdate extends Component {
    constructor (props) {
      super(props)
      this.state = {
          data: {},
          editingData: {},
          errors: null
      }
    }

    componentDidMount() {
        window.scrollTo(0, 0)
        this.fetchData()
    }

    async fetchData() {
        const { id } = this.props.match.params
        const url = apiRoutes.huongNghienCuu.getById + '/' + id

        try {
            const response = await axios.get(url)

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result
                this.setState({ data, editingData: data })
            }
            else {
                throw new Error(response.errors)
            }
        }
        catch (error) {
            console.error(error)
        }
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
        const { tenHuongNghienCuu } = this.state.editingData

        if (tenHuongNghienCuu !== '') {
            const { id } = this.props.match.params
            const url = apiRoutes.huongNghienCuu.updateById + '/' + id
            const data = this.state.editingData

            return axios.put(url, data)
            .then(response => {
                this.props.history.push('/huong-nghien-cuu')
            })
            .catch(error => {
                const { errors } = error.response.data.result
                this.setState({ errors })
            })
        }
        else {
            const errors = {}

            if (tenHuongNghienCuu === '') {
                errors['tenHuongNghienCuu'] = ['tenHuongNghienCuu is required!']
            }

            this.setState({ errors })
        }
    }

    transformErrorMessage(rawMessage) {
        let subject = rawMessage.split(' ')[0].toLowerCase()
        let message = ''

        switch (subject) {
            case 'tenhuongnghiencuu':
                subject = 'Tên hướng nghiên cứu'
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
                    <span className='breadcrumb'><Link to='/huong-nghien-cuu'>Quản lý hướng nghiên cứu</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Cập nhật thông tin hướng nghiên cứu</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Cập nhật thông tin hướng nghiên cứu</h3>
                            <p className='section__subtitle'>Cập nhật thông tin chi tiết của hướng nghiên cứu</p>
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
                                    <label className='form-label'>Mã hướng nghiên cứu</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.maHuongNghienCuu) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Tên hướng nghiên cứu</label>
                                    <input className={ this.isValidInputField('tenHuongNghienCuu') ? 'form-input-alert' : 'form-input-outline' } type='text' value={ this.state.editingData.tenHuongNghienCuu } onChange={ e => this.changeEditingData(e, 'tenHuongNghienCuu') } onFocus={ () => this.hideAlert() } />
                                </div>                                

                                <div className='form-group'>
                                    <label className='form-label'>Ngày tạo</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(formatDateString(this.state.data.thoiGianTao)) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Ngày cập nhật</label>
                                    <input className='form-input-disabled' type='text' value={ getValue(formatDateString(this.state.data.thoiGianCapNhat)) } disabled />
                                </div>
                            </form>
                        </div>
                    </div>

                    <footer className='section__footer'>
                        <div className='section__header-right'>
                        <span className='button'>
                                <Link to='/huong-nghien-cuu'>
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
  
export default connect(mapStateToProps, null)(HuongNghienCuuForUpdate)