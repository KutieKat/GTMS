import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import axios from 'axios'
import apiRoutes from '../../routes/apis'
import moment from 'moment'
import { isValidEmail, isValidPhoneNumber } from '../../utils'

const GioiTinh = ['Nam', 'Nữ']
const HocHam = [
    {
        text: 'Chưa có học hàm',
        value: ''
    },
    {
        text: 'Phó giáo sư',
        value: 'Phó giáo sư'
    },
    {
        text: 'Giáo sư',
        value: 'Giáo sư'
    }
]
const HocVi = [
    {
        text: 'Chưa có học vị',
        value: ''
    },
    {
        text: 'Thạc sĩ',
        value: 'Thạc sĩ'
    },
    {
        text: 'Tiến sĩ',
        value: 'Tiến sĩ'
    }
]

class GiangVienForCreate extends Component {
    constructor (props) {
      super(props)
      this.state = {
          editingData: {
              hoVaTen: '',
              gioiTinh: GioiTinh[0],
              ngaySinh: moment().format('YYYY-MM-DD'),
              email: '',
              soDienThoai: '',
              queQuan: '',
              diaChi: '',
              donViCongTac: '',
              hocVi: '',
              hocHam: '',
              maKhoa: ''
          },
          khoa: [],
          errors: null
      }
    }

    componentDidMount() {
        window.scrollTo(0, 0)
        axios.defaults.headers.common['Authorization'] = 'AUTH_TOKEN'
        this.fetchKhoa()
    }

    async fetchKhoa() {
        const url = apiRoutes.khoa.getAll
        const params = {}
        params['pageSize'] = 1000
        params['trangThai'] = 1

        try {
            const response = await axios.get(url, { params })

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

                this.setState({ khoa: data, editingData: { ...this.state.editingData, maKhoa: data[0].maKhoa || '' } })
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
        const { hoVaTen, gioiTinh,ngaySinh, email, soDienThoai,queQuan,diaChi,donViCongTac,hocVi,hocHam } = this.state.editingData

        if (hoVaTen !== '' && gioiTinh !== '' && ngaySinh !== '' && email !=='' && soDienThoai !=='' && queQuan !=='' && diaChi !=='' && donViCongTac !=='' && isValidEmail(email) && isValidPhoneNumber(soDienThoai)) {
            const url = apiRoutes.giangVien.create
            const data = this.state.editingData

            return axios.post(url, data)
            .then(response => {
                this.props.history.push('/giang-vien')
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

            if (gioiTinh === '') {
                errors['gioiTinh'] = ['gioiTinh is required!']
            }

            if (ngaySinh === '') {
                errors['ngaySinh'] = ['ngaySinh is required!']
            }

            if (email === '') {
                errors['email'] = ['email is required!']
            }

            if (soDienThoai === '') {
                errors['soDienThoai'] = ['soDienThoai is required!']
            }
            
            if (queQuan === '') {
                errors['queQuan'] = ['queQuan is required!']
            }

            if (diaChi === '') {
                errors['diaChi'] = ['diaChi is required!']
            }

            if (donViCongTac === '') {
                errors['donViCongTac'] = ['donViCongTac is required!']
            }

            if (!isValidEmail(email)) {
                errors['email'] = ['email is not valid!']
            }

            if (!isValidPhoneNumber(soDienThoai)) {
                errors['soDienThoai'] = ['soDienThoai is not valid!']
            }

            // if (hocHam === '') {
            //     errors['hocHam'] = ['hocHam is required!']
            // }

            // if (hocVi === '') {
            //     errors['hocVi'] = ['hocVi is required!']
            // }

            this.setState({ errors })
        }
    }

    transformErrorMessage(rawMessage) {
        let subject = rawMessage.split(' ')[0].toLowerCase()
        let message = ''

        switch (subject) {
            case 'hovaten':
                subject = 'Họ và tên'
                break
            
            case 'gioitinh':
                subject = 'Giới tính'
                break

            case 'ngaysinh':
                subject = 'Ngày sinh'
                break    

           case 'email':
                subject = 'Địa chỉ email'
                break

            case 'sodienthoai':
                subject = 'Số điện thoại'
                break  
                 
            case 'quequan':
                subject = 'Quê quán'
                break                

            case 'diachi':
                subject = 'Địa chỉ'
                break     
                
            case 'donvicongtac':
                subject = 'Đơn vị công tác'
                break     
                
            case 'makhoa':
                subject = 'Khoa'
                break

            case 'hocvi':
                subject = 'Học Vị'
                break                     

            case 'hocham':
                subject = 'Học hàm'
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
                    <span className='breadcrumb'><Link to='/giang-vien'>Quản lý giảng viên</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Thêm giảng viên mới</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Thêm giảng viên mới</h3>
                            <p className='section__subtitle'>Thêm giảng viên mới vào hệ thống</p>
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
                                    <label className='form-label'>Họ và tên</label>
                                    <input type='text' className={ this.isValidInputField('hoVaTen') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập họ và tên của giảng viên' value={ this.state.editingData.hoVaTen } onChange={ e => this.changeEditingData(e, 'hoVaTen') } onFocus={ () => this.hideAlert() } />
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
                                    <input type='date' className={ this.isValidInputField('ngaySinh') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập ngày sinh của giảng viên' value={ moment(this.state.editingData.ngaySinh).format('YYYY-MM-DD') } onChange={ e => this.changeEditingData(e, 'ngaySinh') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Email</label>
                                    <input type='text' className={ this.isValidInputField('email') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập địa chỉ email của giảng viên' value={ this.state.editingData.email } onChange={ e => this.changeEditingData(e, 'email') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số điện thoại</label>
                                    <input type='text' className={ this.isValidInputField('soDienThoai') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số điện thoại của giảng viên' value={ this.state.editingData.soDienThoai } onChange={ e => this.changeEditingData(e, 'soDienThoai') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Quê quán</label>
                                    <input type='text' className={ this.isValidInputField('queQuan') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập quê quán của giảng viên' value={ this.state.editingData.queQuan } onChange={ e => this.changeEditingData(e, 'queQuan') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Địa chỉ</label>
                                    <input type='text' className={ this.isValidInputField('diaChi') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập địa chỉ của giảng viên' value={ this.state.editingData.diaChi } onChange={ e => this.changeEditingData(e, 'diaChi') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Đơn vị công tác</label>
                                    <input type='text' className={ this.isValidInputField('donViCongTac') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập đơn vị công tác của giảng viên' value={ this.state.editingData.donViCongTac } onChange={ e => this.changeEditingData(e, 'donViCongTac') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Khoa</label>
                                    <select className={this.isValidInputField('maKhoa') ? 'form-input-alert' : 'form-input-outline'} value={this.state.editingData.maKhoa} onChange={e => this.changeEditingData(e, 'maKhoa')} onFocus={() => this.hideAlert()}>
                                        {
                                            this.state.khoa.map((record, index) => (
                                                <option key={ index } value={ record.maKhoa }>{ record.tenKhoa }</option>
                                            ))
                                        }
                                        <option value=''>Khác</option>
                                    </select>
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Học hàm</label>
                                    <select className={this.isValidInputField('hocHam') ? 'form-input-alert' : 'form-input-outline'} value={this.state.editingData.hocHam} onChange={e => this.changeEditingData(e, 'hocHam')} onFocus={() => this.hideAlert()}>
                                        {
                                            HocHam.map((record, index) => (
                                                <option key={ index } value={ record.value }>{ record.text }</option>
                                            ))
                                        }
                                    </select>
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Học vị</label>
                                    <select className={this.isValidInputField('hocVi') ? 'form-input-alert' : 'form-input-outline'} value={this.state.editingData.hocVi} onChange={e => this.changeEditingData(e, 'hocVi')} onFocus={() => this.hideAlert()}>
                                        {
                                            HocVi.map((record, index) => (
                                                <option key={ index } value={ record.value }>{ record.text }</option>
                                            ))
                                        }
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
  
export default connect(mapStateToProps, null)(GiangVienForCreate)