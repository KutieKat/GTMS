import React, { Component } from 'react'
import { connect } from 'react-redux'
import * as actions from './redux/actions'
import { isEmpty } from './utils'
import App from './App'
import Login from './components/Login'
import { BrowserRouter as Router, Route, Switch, NavLink, withRouter } from 'react-router-dom'

class Main extends Component {
  componentDidMount () {
    const user = JSON.parse(localStorage.getItem('GTMS_USER'))
    if (!isEmpty(user)) this.props.logIn(user)
  }

  render () {
    // if (!isEmpty(this.props.user)) {
    //   return <App />
    // } else {
    //   return <Login />
    // }

    return (
      <Router>
        <App />
        {/* <Login /> */}
      </Router>
    )
  }
}

const mapStateToProps = state => ({
  user: state.user
})

export default connect(mapStateToProps, actions)(Main)
