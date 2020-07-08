const baseApiAddress = 'http://localhost:5000/api'

export const apiAddress = {
  base: baseApiAddress,
  baseNews: baseApiAddress + '/news',
  baseFeedbacks: baseApiAddress + '/feedbacks',
  baseComments: baseApiAddress + '/comments',
  baseReactions: baseApiAddress + '/reactions',
  baseBookmarks: baseApiAddress + '/bookmarks',
  baseUsers: baseApiAddress + '/users',
  baseHiddenNews: baseApiAddress + '/hidden-news',
  baseFaqs: baseApiAddress + '/faqs.html',
  baseNewsPages: baseApiAddress + '/news-pages',
  baseNewsCategories: baseApiAddress + '/news-categories',
  baseNewsPageSubscriptions: baseApiAddress + '/news-page-subscriptions',
  baseViews: baseApiAddress + '/views',
  baseShares: baseApiAddress + '/shares',
  TaiKhoan: {
    GetAll: baseApiAddress + '/TaiKhoan/GetAll',
    GetById: baseApiAddress + '/TaiKhoan/GetById',
    Register: baseApiAddress + '/TaiKhoan/Register',
    Login: baseApiAddress + '/TaiKhoan/Login',
    UpdateById: baseApiAddress + '/TaiKhoan/UpdateById',
    DeleteById: baseApiAddress + '/TaiKhoan/DeleteById'
  },
  HocKy: {
    GetAll: baseApiAddress + '/HocKy/GetAll'
  }
}
