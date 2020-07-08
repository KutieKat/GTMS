export default {
    statistics: [
        {
            title: 'Thống kê tổng quan',
            path: '/',
            exact: true,
            icon: 'fas fa-chart-pie'
        }
    ],
    lookup: [
        {
            title: 'Tra cứu đồ án',
            path: '/',
            exact: true,
            icon: 'fas fa-book'
        }
    ],
    business: [
        {
            title: 'Quản lý khoa',
            path: '/khoa',
            exact: false,
            icon: 'fas fa-school'
        },
        {
            title: 'Quản lý khóa đào tạo',
            path: '/khoa-dao-tao',
            exact: false,
            icon: 'far fa-calendar-alt'
        },
        {
            title: 'Quản lý học kỳ',
            path: '/hoc-ky',
            exact: false,
            icon: 'far fa-calendar-alt'
        },
        {
            title: 'Quản lý lớp',
            path: '/lop',
            exact: false,
            icon: 'fas fa-school'
        },
        {
            title: 'Quản lý hướng nghiên cứu',
            path: '/huong-nghien-cuu',
            exact: false,
            icon: 'fas fa-list'
        },
        {
            title: 'Quản lý sinh viên',
            path: '/sinh-vien',
            exact: false,
            icon: 'fas fa-user-graduate'
        },
        {
            title: 'Quản lý giảng viên',
            path: '/giang-vien',
            exact: false,
            icon: 'fas fa-user-tie'
        },
        {
            title: 'Quản lý đồ án',
            path: '/do-an',
            exact: false,
            icon: 'fas fa-book'
        }
    ],
    system: [
        {
            title: 'Quản lý cài đặt',
            path: '/cai-dat',
            exact: false,
            icon: 'fas fa-tools'
        },
        {
            title: 'Quản lý quy định',
            path: '/quy-dinh',
            exact: false,
            icon: 'fas fa-sliders-h'
        },
        {
            title: 'Quản lý tài khoản',
            path: '/tai-khoan',
            exact: false,
            icon: 'fas fa-users'
        }
    ]
}