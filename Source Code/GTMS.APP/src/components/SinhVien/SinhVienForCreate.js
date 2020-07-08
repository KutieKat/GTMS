import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import axios from 'axios'
import apiRoutes from '../../routes/apis'
import moment from 'moment'
import { isValidEmail, isValidPhoneNumber } from '../../utils'

const GioiTinh = ['Nam', 'Nữ']

class SinhVienForCreate extends Component {
    constructor(props) {
        super(props)
        this.state = {
            editingData: {
                hoVaTen: '',
                maLop: 0,
                gioiTinh: GioiTinh[0],
                ngaySinh: moment().format('YYYY-MM-DD'),
                email: '',
                queQuan: '',
                diaChi: '',
                soDienThoai:'',
                trangThai: 0,
            },
            lop: [],
            errors: null
        }
    }

    componentDidMount() {
        window.scrollTo(0, 0)
        axios.defaults.headers.common['Authorization'] = 'AUTH_TOKEN'
        this.fetchLop()
    }

    async fetchLop() {
        const url = apiRoutes.lop.getAll
        const params = {}
        params['pageSize'] = 1000
        params['trangThai'] = 1

        try {
            const response = await axios.get(url, { params })

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

                this.setState({ lop: data, editingData: { ...this.state.editingData, maLop: data[0].maLop } })
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
        const { maSinhVien, hoVaTen, maLop, gioiTinh, ngaySinh, email, queQuan, diaChi, soDienThoai } = this.state.editingData

        if (maSinhVien !== '' && hoVaTen !== '' && gioiTinh !== 0 && ngaySinh !== 0 && email !== '' && queQuan !== '' 
            && diaChi !== '' && soDienThoai !== '' && isValidEmail(email) && isValidPhoneNumber(soDienThoai)) {
            const url = apiRoutes.sinhVien.create
            const data = this.state.editingData

            return axios.post(url, data)
                .then(response => {
                    this.props.history.push('/sinh-vien')
                })
                .catch(error => {
                    const { errors } = error.response.data.result
                    this.setState({ errors })
                })
        }
        else {
            const errors = {}

            if (hoVaTen === '') {
                errors['hoVaTen'] = ['hoVaTen is required!']
            }

            // if (maLop === 0) {
            //     errors['maLop'] = ['maLop is required!']
            // }

            if (gioiTinh === 0) {
                errors['gioiTinh'] = ['gioiTinh is required!']
            }

            if (ngaySinh === '') {
                errors['ngaySinh'] = ['ngaySinh is required!']
            }

            if (email === '') {
                errors['email'] = ['email is required!']
            }

            if (queQuan === '') {
                errors['queQuan'] = ['queQuan is required!']
            }

            if (diaChi === '') {
                errors['diaChi'] = ['diaChi is required!']
            }

            if (soDienThoai === '') {
                errors['soDienThoai'] = ['soDienThoai is required!']
            }

            if (!isValidEmail(email)) {
                errors['email'] = ['email is not valid!']
            }

            if (!isValidPhoneNumber(soDienThoai)) {
                errors['soDienThoai'] = ['soDienThoai is not valid!']
            }

            this.setState({ errors })
        }
    }

    transformErrorMessage(rawMessage) {
        let subject = rawMessage.split(' ')[0].toLowerCase()
        let message = ''

        switch (subject) {
            case 'hovaten':
                subject = 'Tên của sinh viên'
                break

            case 'malop':
                subject = 'Lớp của sinh viên'
                break

            case 'gioitinh':
                subject = 'Giới tính của sinh viên'
                break

            case 'ngaysinh':
                subject = 'Ngày sinh của sinh viên'
                break

            case 'email':
                subject = 'Địa chỉ email của sinh viên'
                break

            case 'quequan':
                subject = 'Quê quán của sinh viên'
                break

            case 'diachi':
                subject = 'Địa chỉ của sinh viên'
                break

            case 'sodienthoai':
                subject = 'Số điện thoại của sinh viên'
                break
        }

        if (rawMessage.includes('require')) {
            message = subject + ' là bắt buộc và không được bỏ trống!'
        }

        if (rawMessage.includes('duplicate')) {
            message = subject + ' đã bị trùng!'
        }

        if (rawMessage.includes('not valid')) {
            message = subject + ' không hợp lệ!'
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
                    <span className='breadcrumb'><Link to='/sinh-vien'>Quản lý sinh viên</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Thêm sinh viên mới</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Thêm sinh viên mới</h3>
                            <p className='section__subtitle'>Thêm sinh viên mới vào hệ thống</p>
                        </div>
                    </header>

                    {this.state.errors && <div className='section__alert'>
                        <ul>
                            {
                                Object.keys(this.state.errors).map((error, index) => {
                                    return this.state.errors[error].map(message => (
                                        <li key={index}><i className="fas fa-exclamation-triangle"></i>&nbsp;&nbsp;{this.transformErrorMessage(message)}</li>
                                    ))
                                })
                            }
                        </ul>
                        {this.state.message}
                    </div>}

                    <div className='section__body'>
                        <div className='section-body-main'>
                            <form autoComplete='off'>
                                <div className='form-group'>
                                    <label className='form-label'>Họ và tên</label>
                                    <input className={this.isValidInputField('hoVaTen') ? 'form-input-alert' : 'form-input-outline'} type='text' placeholder='Nhập họ và tên của sinh viên' value={this.state.editingData.hoVaTen} onChange={e => this.changeEditingData(e, 'hoVaTen')} onFocus={() => this.hideAlert()} />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Lớp</label>
                                    <select className={this.isValidInputField('maLop') ? 'form-input-alert' : 'form-input-outline'} value={this.state.editingData.maLop} onChange={e => this.changeEditingData(e, 'maLop')} onFocus={() => this.hideAlert()}>
                                        {
                                            this.state.lop.length > 0 && this.state.lop.map((record, index) => (
                                                <option key={ index } value={ record.maLop }>{ record.tenLop }</option>
                                            ))
                                        }
                                    </select>
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Giới tính</label>
                                    <select className={this.isValidInputField('gioiTinh') ? 'form-input-alert' : 'form-input-outline'} value={this.state.editingData.gioiTinh} onChange={e => this.changeEditingData(e, 'gioiTinh')} onFocus={() => this.hideAlert()}>
                                        {
                                            GioiTinh.map((record, index) => (
                                                <option key={ index } value={ record }>{ record }</option>
                                            ))
                                        }
                                    </select>
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Ngày sinh</label>
                                    <input className={this.isValidInputField('ngaySinh') ? 'form-input-alert' : 'form-input-outline'} type='date' placeholder='Nhập ngày sinh của sinh viên' value={moment(this.state.editingData.ngaySinh).format('YYYY-MM-DD')} onChange={e => this.changeEditingData(e, 'ngaySinh')} onFocus={() => this.hideAlert()} />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Email</label>
                                    <input className={this.isValidInputField('email') ? 'form-input-alert' : 'form-input-outline'} type='text' placeholder='Nhập địa chỉ email của sinh viên' value={this.state.editingData.email} onChange={e => this.changeEditingData(e, 'email')} onFocus={() => this.hideAlert()} />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Quê quán</label>
                                    <input className={this.isValidInputField('queQuan') ? 'form-input-alert' : 'form-input-outline'} type='text' placeholder='Nhập quê quán của sinh viên' value={this.state.editingData.queQuan} onChange={e => this.changeEditingData(e, 'queQuan')} onFocus={() => this.hideAlert()} />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Địa chỉ</label>
                                    <input className={this.isValidInputField('diaChi') ? 'form-input-alert' : 'form-input-outline'} type='text' placeholder='Nhập địa chỉ của sinh viên' value={this.state.editingData.diaChi} onChange={e => this.changeEditingData(e, 'diaChi')} onFocus={() => this.hideAlert()} />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số điện thoại</label>
                                    <input className={this.isValidInputField('soDienThoai') ? 'form-input-alert' : 'form-input-outline'} type='text' placeholder='Nhập số điện thoại của sinh viên' value={this.state.editingData.soDienThoai} onChange={e => this.changeEditingData(e, 'soDienThoai')} onFocus={() => this.hideAlert()} />
                                </div>


                            </form>
                        </div>
                    </div>

                    <footer className='section__footer'>
                        <div className='section__header-right'>
                            <span className='button'>
                                <Link to='/sinh-vien'>
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

export default connect(mapStateToProps, null)(SinhVienForCreate)