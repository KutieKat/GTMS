// Khoa
import KhoaForList from '../components/Khoa/KhoaForList'
import KhoaForCreate from '../components/Khoa/KhoaForCreate'
import KhoaForView from '../components/Khoa/KhoaForView'
import KhoaForUpdate from '../components/Khoa/KhoaForUpdate'

// KhoaDaoTao
import KhoaDaoTaoForList from '../components/KhoaDaoTao/KhoaDaoTaoForList'
import KhoaDaoTaoForCreate from '../components/KhoaDaoTao/KhoaDaoTaoForCreate'
import KhoaDaoTaoForView from '../components/KhoaDaoTao/KhoaDaoTaoForView'
import KhoaDaoTaoForUpdate from '../components/KhoaDaoTao/KhoaDaoTaoForUpdate'

// HuongNghienCuu
import HuongNghienCuuForList from '../components/HuongNghienCuu/HuongNghienCuuForList'
import HuongNghienCuuForCreate from '../components/HuongNghienCuu/HuongNghienCuuForCreate'
import HuongNghienCuuForView from '../components/HuongNghienCuu/HuongNghienCuuForView'
import HuongNghienCuuForUpdate from '../components/HuongNghienCuu/HuongNghienCuuForUpdate'

// HocKy
import HocKyForList from '../components/HocKy/HocKyForList'
import HocKyForCreate from '../components/HocKy/HocKyForCreate'
import HocKyForView from '../components/HocKy/HocKyForView'
import HocKyForUpdate from '../components/HocKy/HockyForUpdate'

// HocKy
import TaiKhoanForList from '../components/TaiKhoan/TaiKhoanForList'
import TaiKhoanForCreate from '../components/TaiKhoan/TaiKhoanForCreate'
import TaiKhoanForView from '../components/TaiKhoan/TaiKhoanForView'
import TaiKhoanForUpdate from '../components/TaiKhoan/TaiKhoanForUpdate'

// Lop
import LopForList from '../components/Lop/LopForList'
import LopForCreate from '../components/Lop/LopForCreate'
import LopForView from '../components/Lop/LopForView'
import LopForUpdate from '../components/Lop/LopForUpdate'

// QuyDinh
import QuyDinhForList from '../components/QuyDinh/QuyDinhForList'
import QuyDinhForCreate from '../components/QuyDinh/QuyDinhForCreate'
import QuyDinhForView from '../components/QuyDinh/QuyDinhForView'
import QuyDinhForUpdate from '../components/QuyDinh/QuyDinhForUpdate'

// GiangVien
import GiangVienForList from '../components/GiangVien/GiangVienForList'
import GiangVienForCreate from '../components/GiangVien/GiangVienForCreate'
import GiangVienForView from '../components/GiangVien/GiangVienForView'
import GiangVienForUpdate from '../components/GiangVien/GiangVienForUpdate'

// SinhVien
import SinhVienForList from '../components/SinhVien/SinhVienForList'
import SinhVienForCreate from '../components/SinhVien/SinhVienForCreate'
import SinhVienForView from '../components/SinhVien/SinhVienForView'
import SinhVienForUpdate from '../components/SinhVien/SinhVienForUpdate'

// CaiDat
import CaiDatForView from '../components/CaiDat/CaiDatForView'
import CaiDatForUpdate from '../components/CaiDat/CaiDatForUpdate'

// DoAn
import DoAnForCreate from '../components/DoAn/DoAnForCreate'
import DoAnForView from '../components/DoAn/DoAnForView'

// HoSo
import ThayDoiMatKhau from '../components/HoSo/ThayDoiMatKhau'
import DoAnForList from '../components/DoAn/DoAnForList';

export default [
    {
        path: '/khoa',
        component: KhoaForList,
        exact: true
    },
    {
        path: '/khoa/them-moi',
        component: KhoaForCreate,
        exact: false
    },
    {
        path: '/khoa/xem-thong-tin/:id',
        component: KhoaForView,
        exact: false
    },
    {
        path: '/khoa/cap-nhat/:id',
        component: KhoaForUpdate,
        exact: false
    },
    
    {
        path: '/khoa-dao-tao',
        component: KhoaDaoTaoForList,
        exact: true
    },
    {
        path: '/khoa-dao-tao/them-moi',
        component: KhoaDaoTaoForCreate,
        exact: false
    },
    {
        path: '/khoa-dao-tao/xem-thong-tin/:id',
        component: KhoaDaoTaoForView,
        exact: false
    },
    {
        path: '/khoa-dao-tao/cap-nhat/:id',
        component: KhoaDaoTaoForUpdate,
        exact: false
    },

    {
        path: '/huong-nghien-cuu',
        component: HuongNghienCuuForList,
        exact: true
    },
    {
        path: '/huong-nghien-cuu/them-moi',
        component: HuongNghienCuuForCreate,
        exact: false
    },
    {
        path: '/huong-nghien-cuu/xem-thong-tin/:id',
        component: HuongNghienCuuForView,
        exact: false
    },
    {
        path: '/huong-nghien-cuu/cap-nhat/:id',
        component: HuongNghienCuuForUpdate,
        exact: false
    },

    {
        path: '/hoc-ky',
        component: HocKyForList,
        exact: true
    }, 
    {
        path: '/hoc-ky/them-moi',
        component: HocKyForCreate,
        exact: false
    }, 
    {
        path: '/hoc-ky/xem-thong-tin/:id',
        component: HocKyForView,
        exact: false
    }, 
    {
        path: '/hoc-ky/cap-nhat/:id',
        component: HocKyForUpdate,
        exact: false
    },

    {
        path: '/tai-khoan',
        component: TaiKhoanForList,
        exact: true
    }, 
    {
        path: '/tai-khoan/them-moi',
        component: TaiKhoanForCreate,
        exact: false
    }, 
    {
        path: '/tai-khoan/xem-thong-tin/:id',
        component: TaiKhoanForView,
        exact: false
    }, 
    {
        path: '/tai-khoan/cap-nhat/:id',
        component: TaiKhoanForUpdate,
        exact: false
    },

    {
        path: '/lop',
        component: LopForList,
        exact: true

    },
    {
        path: '/lop/them-moi',
        component: LopForCreate,
        exact: false
    },
    {
        path: '/lop/xem-thong-tin/:id',
        component: LopForView,
        exact: false
    },
    {
        path: '/lop/cap-nhat/:id',
        component: LopForUpdate,
        exact: false
    },

    {
        path: '/giang-vien',
        component: GiangVienForList,
        exact: true
    }, 
    {
        path: '/giang-vien/them-moi',
        component: GiangVienForCreate,
        exact: false
    }, 
    {
        path: '/giang-vien/xem-thong-tin/:id',
        component: GiangVienForView,
        exact: false
    }, 
    {
        path: '/giang-vien/cap-nhat/:id',
        component: GiangVienForUpdate,
        exact: false
    },

    {
        path: '/sinh-vien',
        component: SinhVienForList,
        exact: true

    },
    {
        path: '/sinh-vien/them-moi',
        component: SinhVienForCreate,
        exact: false
    },
    {
        path: '/sinh-vien/xem-thong-tin/:id',
        component: SinhVienForView,
        exact: false
    },
    {
        path: '/sinh-vien/cap-nhat/:id',
        component: SinhVienForUpdate,
        exact: false
    },

    {
        path: '/quy-dinh',
        component: QuyDinhForList,
        exact: true
    }, 
    {
        path: '/quy-dinh/them-moi',
        component: QuyDinhForCreate,
        exact: false
    }, 
    {
        path: '/quy-dinh/xem-thong-tin/:id',
        component: QuyDinhForView,
        exact: false
    }, 
    {
        path: '/quy-dinh/cap-nhat/:id',
        component: QuyDinhForUpdate,
        exact: false
    },
    
    {
        path: '/cai-dat',
        component: CaiDatForView,
        exact: true
    },
    {
        path: '/cai-dat/cap-nhat',
        component: CaiDatForUpdate,
        exact: false
    },

    {
        path: '/do-an',
        component: DoAnForList,
        exact: true
    },
    {
        path: '/do-an/xem-thong-tin/:id',
        component: DoAnForView,
        exact: false
    }, 
    {
        path: '/do-an/them-moi',
        component: DoAnForCreate,
        exact: false
    },
    {
        path: '/ho-so/thay-doi-mat-khau',
        component: ThayDoiMatKhau,
        exact: false
    }
]