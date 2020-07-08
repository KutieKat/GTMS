import React, { Component, Fragment } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import axios from 'axios'
import apiRoutes from '../../routes/apis'
import { formatDateString, getValue } from '../../utils'
import moment from 'moment'

class QuyDinhForUpdate extends Component {
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
        const url = apiRoutes.quyDinh.getById + '/' + id

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
        const { tenQuyDinh, thoiGianBatDauHieuLuc } = this.state.editingData

        if (tenQuyDinh !== '' && thoiGianBatDauHieuLuc !== '') {
            const { id } = this.props.match.params
            const url = apiRoutes.quyDinh.updateById + '/' + id
            const data = this.state.editingData

            return axios.put(url, data)
            .then(response => {
                this.props.history.push('/quy-dinh')
            })
            .catch(error => {
                const { errors } = error.response.data.result
                this.setState({ errors })
            })
        }
        else {
            const errors = {}

            if (tenQuyDinh === '') {
                errors['tenQuyDinh'] = ['tenQuyDinh is required!']
            }

            if (thoiGianBatDauHieuLuc === '') {
                errors['thoiGianBatDauHieuLuc'] = ['thoiGianBatDauHieuLuc is required!']
            }

            this.setState({ errors })
        }
    }

    transformErrorMessage(rawMessage) {
        let subject = rawMessage.split(' ')[0].toLowerCase()
        let message = ''

        switch (subject) {
            case 'tenquydinh':
                subject = 'Tên quy định'
                break
            
            case 'thoigianbatdauhieuluc':
                subject = 'Thời gian bắt đầu hiệu lực của quy định'
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
                    <span className='breadcrumb'><Link to='/quy-dinh'>Quản lý quy định</Link></span>
                    <span className='breadcrumb-separator'><i className='fas fa-chevron-right'></i></span>
                    <span className='breadcrumb-active'><Link to='#'>Cập nhật thông tin quy định</Link></span>
                </section>

                <section className='section'>
                    <header className='section__header'>
                        <div className='section__header-left'>
                            <h3 className='section__title'>Cập nhật thông tin quy định</h3>
                            <p className='section__subtitle'>Cập nhật thông tin chi tiết của quy định</p>
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
                                    <label className='form-label'>Mã quy định</label>
                                    <input type='text' className='form-input-disabled' value={ getValue(this.state.data.maQuyDinh) } disabled />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Tên quy định</label>
                                    <input type='text' className={ this.isValidInputField('tenQuyDinh') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập tên quy định' value={ this.state.editingData.tenQuyDinh } onChange={ e => this.changeEditingData(e, 'tenQuyDinh') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Thời gian bắt đầu hiệu lực</label>
                                    <input className={ this.isValidInputField('thoiGianBatDauHieuLuc') ? 'form-input-alert' : 'form-input-outline' } type='date' placeholder='Nhập thời gian bắt đầu hiệu lực của quy định' value={moment(this.state.editingData.thoiGianBatDauHieuLuc).format('YYYY-MM-DD')} onChange={e => this.changeEditingData(e, 'thoiGianBatDauHieuLuc')} onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số sinh viên thực hiện đồ án tối thiểu</label>
                                    <input type='number' className={ this.isValidInputField('soSVTHToiThieu') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng sinh viên thực hiện đồ án tối thiểu' value={ this.state.editingData.soSVTHToiThieu } onChange={ e => this.changeEditingData(e, 'soSVTHToiThieu') } onFocus={ () => this.hideAlert() } />
                                </div>
                                
                                <div className='form-group'>
                                    <label className='form-label'>Số sinh viên thực hiện đồ án tối đa</label>
                                    <input type='number' className={ this.isValidInputField('soSVTHToiDa') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng sinh viên thực hiện đồ án tối đa' value={ this.state.editingData.soSVTHToiDa } onChange={ e => this.changeEditingData(e, 'soSVTHToiDa') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số giảng viên hướng dẫn đồ án tối thiểu</label>
                                    <input type='number' className={ this.isValidInputField('soGVHDToiThieu') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng giảng viên hướng dẫn đồ án tối thiểu' value={ this.state.editingData.soGVHDToiThieu } onChange={ e => this.changeEditingData(e, 'soGVHDToiThieu') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số giảng viên hướng dẫn đồ án tối đa</label>
                                    <input type='number' className={ this.isValidInputField('soGVHDToiDa') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng giảng viên hướng dẫn đồ án tối đa' value={ this.state.editingData.soGVHDToiDa } onChange={ e => this.changeEditingData(e, 'soGVHDToiDa') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số giảng viên phản biện đồ án tối thiểu</label>
                                    <input type='number' className={ this.isValidInputField('soGVHDToiThieu') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng giảng viên phản biện đồ án tối thiểu' value={ this.state.editingData.soGVPBToiThieu } onChange={ e => this.changeEditingData(e, 'soGVPBToiThieu') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số giảng viên phản biện đồ án tối đa</label>
                                    <input type='number' className={ this.isValidInputField('soGVHDToiDa') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng giảng viên phản biện đồ án tối đa' value={ this.state.editingData.soGVHDToiDa } onChange={ e => this.changeEditingData(e, 'soGVPBToiDa') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số thành viên hội đồng bảo vệ đồ án tối thiểu</label>
                                    <input type='number' className={ this.isValidInputField('soTVHDToiThieu') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng thành viên hội đồng bảo vệ đồ án tối thiểu' value={ this.state.editingData.soTVHDToiThieu } onChange={ e => this.changeEditingData(e, 'soTVHDToiThieu') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số thành viên hội đồng bảo vệ đồ án tối đa</label>
                                    <input type='number' className={ this.isValidInputField('soTVHDToiDa') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng thành viên hội đồng bảo vệ đồ án tối đa' value={ this.state.editingData.soTVHDToiDa } onChange={ e => this.changeEditingData(e, 'soTVHDToiDa') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số chủ tịch hội đồng bảo vệ đồ án tối thiểu</label>
                                    <input type='number' className={ this.isValidInputField('soCTHDToiThieu') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng chủ tịch hội đồng bảo vệ đồ án tối thiểu' value={ this.state.editingData.soCTHDToiThieu } onChange={ e => this.changeEditingData(e, 'soCTHDToiThieu') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số chủ tịch hội đồng bảo vệ đồ án tối đa</label>
                                    <input type='number' className={ this.isValidInputField('soCTHDToiDa') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng chủ tịch hội đồng bảo vệ đồ án tối đa' value={ this.state.editingData.soCTHDToiDa } onChange={ e => this.changeEditingData(e, 'soCTHDToiDa') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số thư ký hội đồng bảo vệ đồ án tối thiểu</label>
                                    <input type='number' className={ this.isValidInputField('soTKHDToiThieu') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng thư ký hội đồng bảo vệ đồ án tối thiểu' value={ this.state.editingData.soTKHDToiThieu } onChange={ e => this.changeEditingData(e, 'soTKHDToiThieu') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số thư ký hội đồng bảo vệ đồ án tối đa</label>
                                    <input type='number' className={ this.isValidInputField('soTKHDToiDa') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng thư ký hội đồng bảo vệ đồ án tối đa' value={ this.state.editingData.soTKHDToiDa } onChange={ e => this.changeEditingData(e, 'soTKHDToiDa') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số ủy viên hội đồng bảo vệ đồ án tối thiểu</label>
                                    <input type='number' className={ this.isValidInputField('soUVHDToiThieu') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng ủy viên hội đồng bảo vệ đồ án tối thiểu' value={ this.state.editingData.soUVHDToiThieu } onChange={ e => this.changeEditingData(e, 'soUVHDToiThieu') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số ủy viên hội đồng bảo vệ đồ án tối đa</label>
                                    <input type='number' className={ this.isValidInputField('soUVHDToiDa') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng ủy viên hội đồng bảo vệ đồ án tối đa' value={ this.state.editingData.soUVHDToiDa } onChange={ e => this.changeEditingData(e, 'soUVHDToiDa') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Số chữ số thập phân</label>
                                    <input type='number' className={ this.isValidInputField('soChuSoThapPhan') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập số lượng chữ số thập phân' value={ this.state.editingData.soChuSoThapPhan } onChange={ e => this.changeEditingData(e, 'soChuSoThapPhan') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Điểm số tối thiểu</label>
                                    <input type='number' className={ this.isValidInputField('diemSoToiThieu') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập điểm số tối thiểu' value={ this.state.editingData.diemSoToiThieu } onChange={ e => this.changeEditingData(e, 'diemSoToiThieu') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Điểm số tối đa</label>
                                    <input type='number' className={ this.isValidInputField('diemSoToiDa') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập điểm số tối đa' value={ this.state.editingData.diemSoToiDa } onChange={ e => this.changeEditingData(e, 'diemSoToiDa') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Điểm số tối đa</label>
                                    <input type='number' className={ this.isValidInputField('diemSoToiDa') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập điểm số tối đa' value={ this.state.editingData.diemSoToiDa } onChange={ e => this.changeEditingData(e, 'diemSoToiDa') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Hệ số điểm của giảng viên hướng dẫn đồ án</label>
                                    <input type='number' className={ this.isValidInputField('heSoGVHD') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập hệ số điểm của giảng viên hướng dẫn đồ án' value={ this.state.editingData.heSoGVHD } onChange={ e => this.changeEditingData(e, 'heSoGVHD') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Hệ số điểm của giảng viên phản biện đồ án</label>
                                    <input type='number' className={ this.isValidInputField('heSoGVPB') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập hệ số điểm của giảng viên phản biện đồ án' value={ this.state.editingData.heSoGVPB } onChange={ e => this.changeEditingData(e, 'heSoGVPB') } onFocus={ () => this.hideAlert() } />
                                </div>

                                <div className='form-group'>
                                    <label className='form-label'>Hệ số điểm của thành viên hội đồng bảo vệ đồ án</label>
                                    <input type='number' className={ this.isValidInputField('heSoTVHD') ? 'form-input-alert' : 'form-input-outline' } placeholder='Nhập hệ số điểm của thành viên hội đồng bảo vệ đồ án' value={ this.state.editingData.heSoTVHD } onChange={ e => this.changeEditingData(e, 'heSoTVHD') } onFocus={ () => this.hideAlert() } />
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
                                <Link to='/quy-dinh'>
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
  
export default connect(mapStateToProps, null)(QuyDinhForUpdate)