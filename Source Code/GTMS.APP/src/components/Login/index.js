import React, { Component } from 'react'
import axios from 'axios'
import { connect } from 'react-redux'
import * as actions from '../../redux/actions'
import { apiAddress } from '../../config/request'

const API_ADDRESS = apiAddress.baseUsers
// const API_ADDRESS = apiAddress.baseUsers

class Login extends Component {
  constructor (props) {
    super(props)
    this.state = {
      userData: {
        tenDangNhap: '',
        matKhau: ''
      }
    }
  }

  changeUserData (fieldName, value) {
    const userData = { ...this.state.userData }
    userData[fieldName] = value
    this.setState({ userData })
  }

  async login (e) {
    e.preventDefault()

    try {
      const requestUrl = apiAddress.TaiKhoan.Login
      const requestBody = this.state.userData
      const response = await axios.post(requestUrl, requestBody)

      // if (response && response.data.status === 'SUCCESS' && response.data.data.user.is_admin === true) {
      //   this.props.logIn(response.data.data.user)
      //   localStorage.setItem('GTMS_USER', JSON.stringify({ id: response.data.data.user._id, token: response.data.data.user.token }))
      // } else {
      //   alert('Đăng nhập vào tài khoản thất bại! Vui lòng kiểm tra lại!')
      //   this.reset()
      // }

      if (response && response.status == 200) {
        this.props.logIn({ _id: 123, username: 'Xin chào' });
        // localStorage.setItem('GTMS_USER', )

      }
      else {
        alert('Đăng nhập vào tài khoản thất bại! Vui lòng kiểm tra lại!')
        this.reset()
      }
    } catch (error) {
      alert('Đăng nhập vào tài khoản thất bại! Vui lòng kiểm tra lại!')
      this.reset()
    }
  }

  reset() {
    this.setState({
      userData: {
        tenDangNhap: '',
        matKhau: ''
      }
    });
  }

  render () {
    return (
      <div className='container'>
        <div className='col-md-6 offset-md-3 loginBox'>
          <form onSubmit={e => this.login(e)} className='login-form'>
            <div className='login-form__header'>
              <img src='/images/logo.png' className='login-form__icon' />
              {/* <h1 className='display-4 text-center loginBox__title'>Đăng nhập GTMS</h1> */}
              <p className='text-center login-form__description'>Graduation Thesis Management System - Hệ thống quản lý Đồ án tốt nghiệp</p>
            </div>

            <hr />

            <div className='form-group'>
              <label>Tên đăng nhập</label>
              <input type='text' placeholder='Tên đăng nhập của bạn' className='form-input-outline' value={this.state.userData.tenDangNhap} onChange={e => this.changeUserData('tenDangNhap', e.target.value)} autoComplete='off' />
            </div>

            <div className='form-group'>
              <label>Mật khẩu</label>
              <input type='password' placeholder='Mật khẩu của bạn' className='form-input-outline' value={this.state.userData.matKhau} onChange={e => this.changeUserData('matKhau', e.target.value)} autoComplete='off' />
            </div>

            <button type='submit' className='login-button'>Đăng nhập</button>

            <hr />

            <p className='login-form__footer'>Đồ án môn học Phân tích thiết kế hệ thống thông tin <strong>(IS201.J21.PMCL)</strong> - Nhóm 1</p>
          </form>

          {/* <p className='lead text-center mt-3 loginBox__footer'>Copyright &copy; GTMS, 2019</p> */}
        </div>
      </div>
    )
  }
}

export default connect(null, actions)(Login)
