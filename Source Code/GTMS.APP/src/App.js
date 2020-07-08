import React, { Component, Fragment } from 'react'
import { BrowserRouter as Router, Route, Switch, NavLink, withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import * as actions from './redux/actions'
import menuRoutes from './routes/menus'
import pageRoutes from './routes/pages'
import { isEmpty } from './utils'

import Login from './components/Login'
import NotFound from './components/NotFound'

import './styles.css'

const role = 'CBQL'

class App extends Component {
  constructor(props) {
    super(props)
    this.state = {
      pathTitle: ''
    }
  }

  componentDidMount() {
    let pathTitle = ''

    switch (role.toUpperCase()) {
      case 'CBQL':
        pathTitle = 'Thống kê tổng quan'
        break

      case 'GV':
      case 'SV':
        pathTitle = 'Tra cứu'
        break
    }

    this.setState({ pathTitle })
  }

  logOut () {
    if (window.confirm('Bạn có chắc chắn muốn đăng xuất khỏi GTMS hay không?')) {
      this.props.logOut()
      localStorage.removeItem('GTMS_USER')
    }
  }

  renderMenuSection(title, menuItems) {
    return (
      <Fragment>
        <div className="main-nav__title">{ title }</div>
        <ul className="main-nav__list">
          { 
            menuItems.map((menu, index) => (
              menu.path !== '/dang-xuat' ?
              <li className="main-nav__list-item" key={ index }>
                <NavLink to={ menu.path } onClick={ () => this.setState({ pathTitle: menu.title }) } activeClassName="main-nav__list-item--active" exact={ menu.exact }><i className={ menu.icon }></i>&nbsp;&nbsp;{ menu.title }</NavLink>
              </li> :
              <li className="main-nav__list-item" key={ index }>
                <NavLink to="#" onClick={ () => this.logOut() }><i className={ menu.icon }></i>&nbsp;&nbsp;{ menu.title }</NavLink>
              </li>
            ))
          }
        </ul>
      </Fragment>
    )
  }

  renderMenuByRole(role) {
    switch (role.toUpperCase()) {
      case 'CBQL':
        return (
          <Fragment>
            {/* { this.renderMenuSection('Tổng quan', menuRoutes.statistics) } */}
            { this.renderMenuSection('Nghiệp vụ', menuRoutes.business) }
            { this.renderMenuSection('Hệ thống', menuRoutes.system) }
            {/* { this.renderMenuSection('Tài khoản', menuRoutes.account) } */}
          </Fragment>
        )

      case 'GV':
      case 'SV':
        return (
          <Fragment>
            { this.renderMenuSection('Tra cứu', menuRoutes.lookup) }
          </Fragment>
        )
    }
  }

  getRoleName(role) {
    switch (role.toUpperCase()) {
      case 'CBQL':
        return 'Cán bộ quản lý'

      case 'GV':
        return 'Giảng viên'

      case 'SV':
        return 'Sinh viên'
    }
  }

  renderRoutes() {
    return (
      pageRoutes.map((pageRoute, index) => (
        <Route key={ index } path={ pageRoute.path } component={ pageRoute.component } exact={ pageRoute.exact } />
      ))
    )
  }

  render () {
    return (
      <Fragment>
        <header className="main-header">
          <div className="main-header__left">
            <h1 className="main-header__title"><i className="fas fa-graduation-cap"></i> GTMS</h1>
            <p className="main-header__subtitle">Graduation Thesis Management System</p>
          </div>

          <div className="main-header__center">
            { this.state.pathTitle }
          </div>

          <div className="main-header__right">
            <span className="main-header__profile-avatar">TD</span>
            <div>
              <div className="main-header__profile-name">Nguyễn Tiến Dũng</div>
              <div className="main-header__profile-role"><i className="fas fa-user-tie"></i>&nbsp;&nbsp;{ this.getRoleName(role) }</div>
            </div>

            <div className='main-header__dropdown-menu'>
              <ul className="main-nav__list">
                <li className="main-nav__list-item">
                  <NavLink to="#"><i className='fas fa-address-card'></i>&nbsp;&nbsp;Cập nhật thông tin</NavLink>
                </li>
                <li className="main-nav__list-item">
                  <NavLink to="/ho-so/thay-doi-mat-khau"><i className='fas fa-lock'></i>&nbsp;&nbsp;Thay đổi mật khẩu</NavLink>
                </li>
                <li className="main-nav__list-item">
                  <NavLink to="#"><i className='fas fa-sign-out-alt'></i>&nbsp;&nbsp;Đăng xuất</NavLink>
                </li>
              </ul>
            </div>
          </div>
        </header>

        <main className="main">
          <div className="main-left">
            { this.renderMenuByRole(role) }
          </div>

          <div className="main-right">
            <Switch>
              { this.renderRoutes() }
              <Route component={ NotFound } />
            </Switch>
          </div>
        </main>
      </Fragment>
    )
  }
}

const mapStateToProps = state => ({
  user: state.user
})

export default withRouter(connect(mapStateToProps, actions)(App))
