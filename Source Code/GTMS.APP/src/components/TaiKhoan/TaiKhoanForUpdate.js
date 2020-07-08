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
        this.fetchData()
    }

    async fetchData() {
        const { id } = this.props.match.params
        const url = apiRoutes.hocKy.getById + '/' + id

        try {
            const response = await axios.get(url)

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

                data['thoiGianBatDau'] = moment(data['thoiGianBatDau']).format('YYYY-MM-DD')
                data['thoiGianKetThuc'] = moment(data['thoiGianKetThuc']).format('YYYY-MM-DD')

                this.setState({ data, editingData: data })
            } else {
                throw new Error(response.errors)
            }
        } catch (error) {
            console.error(error)
        }
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
        const { tenHocKy, namHoc, thoiGianBatDau, thoiGianKetThuc } = this.state.editingData

        if (tenHocKy !== '' && namHoc !== '' && thoiGianBatDau !== '' && thoiGianKetThuc !== '' && new Date(thoiGianKetThuc) >= new Date(thoiGianBatDau)) {
            const { id } = this.props.match.params
            const url = apiRoutes.hocKy.updateById + '/' + id
            const data = this.state.editingData

            return axios.put(url, data)
            .then(response => {
                this.props.history.push('/hoc-ky')
            })
            .catch(error => {
                const { errors } = error.response.data.result
                this.setState({ errors })
            })
        }
        else {
            const errors = {}

            if (tenHocKy === '') {
                errors['tenHocKy'] = ['tenHocKy is required!']
            }

            if (namHoc === '') {
                errors['namHoc'] = ['namHoc is required!']
            }

            if (thoiGianBatDau === '') {
                errors['thoiGianBatDau'] = ['thoiGianBatDau is required!']
            }

            if (thoiGianKetThuc === '') {
                errors['thoiGianKetThuc'] = ['thoiGianKetThuc is required!']
            }

            if (new Date(thoiGianKetThuc) < new Date(thoiGianBatDau)) {
                errors['thoiGianKetThuc'] = ['thoiGianKetThuc must come before thoiGianBatDau!']
            }

            this.setState({ errors })
        }
    }

    transformErrorMessage(rawMessage) {
        let subject = rawMessage.split(' ')[0].toLowerCase()
        let message = ''

        switch (subject) {
            case 'tenhocky':
                subject = 'Tên học kỳ'
                break
            
            case 'namhoc':
                subject = 'Năm học'
                break
            
            case 'thoigianbatdau':
                subject = 'Thời gian bắt đầu'
                break
            
            case 'thoigianketthuc':
                subject = 'Thời gian kết thúc'
                break
        }

        if (rawMessage.includes('require')) {
            message = subject + ' là bắt buộc và không được bỏ trống!'
        }

        if (rawMessage.includes('duplicate')) {
            message = subject + ' đã bị trùng!'
        }

        if (rawMessage.includes('come before')) {
            message = subject + ' phải lớn hơn hoặc bằng thời gian bắt đầu!'
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
                    <span className='breadcrumb'><Link to='/hoc-ky'>Quản lý học kỳ</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Cập nhật thông tin học kỳ</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Cập nhật thông tin học kỳ</h3>
                            <p className='section__subtitle'>Cập nhật thông tin chi tiết của học kỳ</p>
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
                                    <label className='form-label'>Mã học kỳ</label>
                                    <input type='text' className='form-input-disabled' value={getValue(this.state.data.maHocKy)} disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Tên học kỳ</label>
                                    <input className={ this.isValidInputField('tenKhoaDaoTao') ? 'form-input-alert' : 'form-input-outline' } type='text' value={this.state.editingData.tenHocKy} onChange={e => this.changeEditingData(e, 'tenHocKy')} onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Năm của học kỳ</label>
                                    <input className={ this.isValidInputField('tenKhoaDaoTao') ? 'form-input-alert' : 'form-input-outline' } type='text' value={this.state.editingData.namHoc} onChange={e => this.changeEditingData(e, 'namHoc')} onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Thời gian bắt đầu</label>
                                    <input className={ this.isValidInputField('thoiGianBatDau') ? 'form-input-alert' : 'form-input-outline' } type='date' placeholder='Nhập thời gian bắt đầu của học kỳ' value={this.state.editingData.thoiGianBatDau} onChange={e => this.changeEditingData(e, 'thoiGianBatDau')} onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Thời gian kết thúc</label>
                                    <input className={ this.isValidInputField('thoiGianKetThuc') ? 'form-input-alert' : 'form-input-outline' } type='date' placeholder='Nhập thời gian kết thúc của học kỳ' value={this.state.editingData.thoiGianKetThuc} onChange={e => this.changeEditingData(e, 'thoiGianKetThuc')} onFocus={ () => this.hideAlert() } />
                                </div>
                            
                                <div className='form-group'>
                                    <label className='form-label'>Ngày tạo</label>
                                    <input className='form-input-disabled' type='text' value={getValue(formatDateString(this.state.data.thoiGianTao))} disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Ngày cập nhật</label>
                                    <input className='form-input-disabled' type='text' value={getValue(formatDateString(this.state.data.thoiGianCapNhat))} disabled />
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