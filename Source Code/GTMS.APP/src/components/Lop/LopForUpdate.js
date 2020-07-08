import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import moment from 'moment'
import { Link } from 'react-router-dom'
import axios from 'axios'
import apiRoutes from '../../routes/apis'
import { getValue, formatDateString } from '../../utils'

const HeDaoTao = ['Chính quy', 'Từ xa qua mạng', 'Văn bằng 2', 'Liên thông']

class LopForUpdate extends Component {
    constructor(props) {
        super(props)
        this.state = {
            data: {},
            editingData: {},
            khoa: [],
            khoaDaoTao: [],
            errors: null
        }
    }

    componentDidMount() {
        window.scrollTo(0, 0)
        this.fetchData()
        this.fetchKhoa()
        this.fetchKhoaDaoTao()
    }

    async fetchData() {
        const { id } = this.props.match.params
        const url = apiRoutes.lop.getById + '/' + id

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

    async fetchKhoa() {
        const url = apiRoutes.khoa.getAll
        const params = {}
        params['pageSize'] = 1000
        params['trangThai'] = 1

        try {
            const response = await axios.get(url, { params })

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

                this.setState({ khoa: data, editingData: { ...this.state.editingData, maKhoa: data[0].maKhoa } })
            }
            else {
                throw new Error(response.errors)
            }
        }
        catch (error) {
            console.error(error)
        }
    }

    async fetchKhoaDaoTao() {
        const url = apiRoutes.khoaDaoTao.getAll
        const params = {}
        params['pageSize'] = 1000
        params['trangThai'] = 1

        try {
            const response = await axios.get(url, { params })

            if (response && response.data.status === 'SUCCESS') {
                const { data } = response.data.result

                this.setState({ khoaDaoTao: data, editingData: { ...this.state.editingData, maKhoaDaoTao: data[0].maKhoaDaoTao } })
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
        const { tenLop, tenVietTat, maKhoa, maKhoaDaoTao, heDaoTao } = this.state.editingData

        if (tenLop !== '' && tenVietTat !== '') {
            const { id } = this.props.match.params
            const url = apiRoutes.lop.updateById + '/' + id
            const data = this.state.editingData

            return axios.put(url, data)
                .then(response => {
                    this.props.history.push('/lop')
                })
                .catch(error => {
                    const { errors } = error.response.data.result
                    this.setState({ errors })
                })
        }
        else {
            const errors = {}

            if (tenLop=== '') {
                errors['tenLop'] = ['tenLop is required!']
            }

            if (tenVietTat === '') {
                errors['tenVietTat'] = ['tenVietTat is required!']
            }

            // if (maKhoa === '') {
            //     errors['maKhoa'] = ['maKhoa is required!']
            // }

            // if (maKhoaDaoTao === '') {
            //     errors['maKhoaDaoTao'] = ['maKhoaDaoTao is required!']
            // }

            // if (heDaoTao === '') {
            //     errors['heDaoTao'] = ['heDaoTao is required!']
            // }

            this.setState({ errors })
        }
    }

    transformErrorMessage(rawMessage) {
        let subject = rawMessage.split(' ')[0].toLowerCase()
        let message = ''

        switch (subject) {
            case 'tenLop':
                subject = 'Tên lớp'
                break

            case 'tenviettat':
                subject = 'Tên viết tắt của lớp'
                break

            case 'maKhoa':
                subject = 'Khoa'
                break

            case 'maHeDaoTao':
                subject = 'Hệ đào tạo'
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
                    <span className='breadcrumb'><Link to='/lop'>Quản lý lớp</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Cập nhật lớp</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Cập nhật thông tin lớp</h3>
                            <p className='section__subtitle'>Cập nhật thông tin chi tiết của lớp</p>
                        </div>
                    </header>

                    <div className='section__body'>
                        <div className='section-body-main'>
                            <form autoComplete='off'>
                                <div className='form-group'>
                                    <label className='form-label'>Mã lớp</label>
                                    <input type='text' className='form-input-disabled' value={getValue(this.state.data.maLop)} disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Tên lớp</label>
                                    <input className={this.isValidInputField('tenLop') ? 'form-input-alert' : 'form-input-outline'} type='text' value={this.state.editingData.tenLop} onChange={e => this.changeEditingData(e, 'tenLop')} />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Tên viết tắt của lớp</label>
                                    <input className={this.isValidInputField('tenVietTat') ? 'form-input-alert' : 'form-input-outline'} type='text' value={this.state.editingData.tenVietTat} onChange={e => this.changeEditingData(e, 'tenVietTat')} />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Khoa</label>
                                    <select className={this.isValidInputField('maKhoa') ? 'form-input-alert' : 'form-input-outline'} value={this.state.editingData.maKhoa} onChange={e => this.changeEditingData(e, 'maKhoa')} onFocus={() => this.hideAlert()}>
                                        {
                                            this.state.khoa.length > 0 && this.state.khoa.map((record, index) => (
                                                <option key={ index } value={ record.maKhoa }>{ record.tenKhoa }</option>
                                            ))
                                        }
                                    </select>
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Khóa đào tạo</label>
                                    <select className={this.isValidInputField('maKhoaDaoTao') ? 'form-input-alert' : 'form-input-outline'} value={this.state.editingData.khoaDaoTao} onChange={e => this.changeEditingData(e, 'maKhoaDaoTao')}>
                                        {
                                            this.state.khoaDaoTao.length > 0 && this.state.khoaDaoTao.map((record, index) => (
                                                <option key={ index } value={ record.maKhoaDaoTao }>{ record.tenKhoaDaoTao }</option>
                                            ))
                                        }
                                    </select>
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Hệ đào tạo</label>
                                    <select className={this.isValidInputField('heDaoTao') ? 'form-input-alert' : 'form-input-outline'} value={this.state.editingData.heDaoTao} onChange={e => this.changeEditingData(e, 'heDaoTao')} onFocus={() => this.hideAlert()}>
                                        {
                                            HeDaoTao.map((record, index) => (
                                                <option key={ index } value={ record }>{ record }</option>
                                            ))
                                        }
                                    </select>
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
                                <Link to='/lop'>
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

export default connect(mapStateToProps, null)(LopForUpdate)