import {
  USER_LOG_IN,
  USER_LOG_OUT
} from '../actions/types'

let initialState = {}
// let initialState = { '_id': '5c30b3640556a50017147115', 'full_name': '', 'date_of_birth': 0, 'phone': '', 'bio': '', 'avatar': 'https://blog.stylingandroid.com/wp-content/themes/lontano-pro/images/no-image-slide.png', 'token': 'AzvpOEpl173cBJEk2UZzH6juxFjD8Ec70XLnRnQuwkG', 'reset_password_token': '', 'reset_password_expires': null, 'is_admin': true, 'is_blocked': false, 'created_at': 1546695524, 'updated_at': 1546695524, 'username': 'nguoidung1', 'password': '$2a$10$0P3AOsXyxvq2nIdMg.319.0Ec9hFAtnbjdV77kOfQ8WNuvfSlDfI.', 'email': 'nguoidung1@gmail.com', '__v': 0 }

export default function (state = initialState, action) {
  switch (action.type) {
    case USER_LOG_IN: {
      const newState = action.user
      return newState
    }

    case USER_LOG_OUT: {
      const newState = {}
      return newState
    }

    default: {
      return state
    }
  }
};
