const baseApiUrl = 'http://localhost:5000/api'
const baseKhoaApiUrl = baseApiUrl + '/Khoa'
const baseKhoaDaoTaoApiUrl=baseApiUrl+'/KhoaDaoTao'
const baseHuongNghienCuuApiUrl=baseApiUrl+'/HuongNghienCuu'
const baseHocKyApiUrl = baseApiUrl + '/HocKy'
const baseTaiKhoanApiUrl = baseApiUrl + '/TaiKhoan'
const baseQuyDinhApiUrl = baseApiUrl + '/QuyDinh'
const baseLopApiUrl = baseApiUrl + '/Lop'
const baseGiangVienApiUrl = baseApiUrl + '/GiangVien'
const baseSinhVienApiUrl = baseApiUrl + '/SinhVien'
const baseCaiDatApiUrl = baseApiUrl + '/CaiDat'
const baseDoAnApiUrl = baseApiUrl + '/DoAn'

export default {
    khoa: {
        getAll: baseKhoaApiUrl + '/GetAll',
        getById: baseKhoaApiUrl + '/GetById',
        create: baseKhoaApiUrl + '/Create',
        createMultiple: baseKhoaApiUrl + '/CreateMultiple',
        updateById: baseKhoaApiUrl + '/UpdateById',
        temporarilyDeleteById: baseKhoaApiUrl + '/TemporarilyDeleteById',
        permanentlyDeleteById: baseKhoaApiUrl + '/PermanentlyDeleteById',
        restoreById: baseKhoaApiUrl + '/RestoreById'
    },
    khoaDaoTao: {
        getAll: baseKhoaDaoTaoApiUrl + '/GetAll',
        getById: baseKhoaDaoTaoApiUrl + '/GetById',
        create: baseKhoaDaoTaoApiUrl + '/Create',
        createMultiple: baseKhoaDaoTaoApiUrl + '/CreateMultiple',
        updateById: baseKhoaDaoTaoApiUrl + '/UpdateById',
        temporarilyDeleteById: baseKhoaDaoTaoApiUrl + '/TemporarilyDeleteById',
        permanentlyDeleteById: baseKhoaDaoTaoApiUrl + '/PermanentlyDeleteById',
        restoreById: baseKhoaDaoTaoApiUrl + '/RestoreById'
    },
    huongNghienCuu: {
        getAll: baseHuongNghienCuuApiUrl + '/GetAll',
        getById: baseHuongNghienCuuApiUrl + '/GetById',
        create: baseHuongNghienCuuApiUrl + '/Create',
        createMultiple: baseHuongNghienCuuApiUrl + '/CreateMultiple',
        updateById: baseHuongNghienCuuApiUrl + '/UpdateById',
        temporarilyDeleteById: baseHuongNghienCuuApiUrl + '/TemporarilyDeleteById',
        permanentlyDeleteById: baseHuongNghienCuuApiUrl + '/PermanentlyDeleteById',
        restoreById: baseHuongNghienCuuApiUrl + '/RestoreById'
    },
    hocKy: {
        getAll: baseHocKyApiUrl + '/GetAll',
        getById: baseHocKyApiUrl + '/GetById',
        create: baseHocKyApiUrl + '/Create',
        createMultiple: baseHocKyApiUrl + '/CreateMultiple',
        updateById: baseHocKyApiUrl + '/UpdateById',
        temporarilyDeleteById: baseHocKyApiUrl + '/TemporarilyDeleteById',
        permanentlyDeleteById: baseHocKyApiUrl + '/PermanentlyDeleteById',
        restoreById: baseHocKyApiUrl + '/RestoreById'
    },
    taiKhoan: {
        getAll: baseTaiKhoanApiUrl + '/GetAll',
        getById: baseTaiKhoanApiUrl + '/GetById',
        create: baseTaiKhoanApiUrl + '/Create',
        createMultiple: baseTaiKhoanApiUrl + '/CreateMultiple',
        updateById: baseTaiKhoanApiUrl + '/UpdateById',
        temporarilyDeleteById: baseTaiKhoanApiUrl + '/TemporarilyDeleteById',
        permanentlyDeleteById: baseTaiKhoanApiUrl + '/PermanentlyDeleteById',
        restoreById: baseTaiKhoanApiUrl + '/RestoreById',
        changePassword: baseTaiKhoanApiUrl + '/ChangePassword'
    },
    quyDinh: {
        getAll: baseQuyDinhApiUrl + '/GetAll',
        getById: baseQuyDinhApiUrl + '/GetById',
        create: baseQuyDinhApiUrl + '/Create',
        createMultiple: baseQuyDinhApiUrl + '/CreateMultiple',
        updateById: baseQuyDinhApiUrl + '/UpdateById',
        temporarilyDeleteById: baseQuyDinhApiUrl + '/TemporarilyDeleteById',
        permanentlyDeleteById: baseQuyDinhApiUrl + '/PermanentlyDeleteById',
        restoreById: baseQuyDinhApiUrl + '/RestoreById'
    },
    lop: {
        getAll: baseLopApiUrl + '/GetAll',
        getById: baseLopApiUrl + '/GetById',
        create: baseLopApiUrl + '/Create',
        createMultiple: baseLopApiUrl + '/CreateMultiple',
        updateById: baseLopApiUrl + '/UpdateById',
        temporarilyDeleteById: baseLopApiUrl + '/TemporarilyDeleteById',
        permanentlyDeleteById: baseLopApiUrl + '/PermanentlyDeleteById',
        restoreById: baseLopApiUrl + '/RestoreById'
    },
    giangVien: {
        getAll: baseGiangVienApiUrl + '/GetAll',
        getById: baseGiangVienApiUrl + '/GetById',
        create: baseGiangVienApiUrl + '/Create',
        createMultiple: baseGiangVienApiUrl + '/CreateMultiple',
        updateById: baseGiangVienApiUrl + '/UpdateById',
        temporarilyDeleteById: baseGiangVienApiUrl + '/TemporarilyDeleteById',
        permanentlyDeleteById: baseGiangVienApiUrl + '/PermanentlyDeleteById',
        restoreById: baseGiangVienApiUrl + '/RestoreById'
    },
    sinhVien: {
        getAll: baseSinhVienApiUrl + '/GetAll',
        getById: baseSinhVienApiUrl + '/GetById',
        create: baseSinhVienApiUrl + '/Create',
        createMultiple: baseSinhVienApiUrl + '/CreateMultiple',
        updateById: baseSinhVienApiUrl + '/UpdateById',
        temporarilyDeleteById: baseSinhVienApiUrl + '/TemporarilyDeleteById',
        permanentlyDeleteById: baseSinhVienApiUrl + '/PermanentlyDeleteById',
        restoreById: baseSinhVienApiUrl + '/RestoreById'
    },
    caiDat: {
        getAll: baseCaiDatApiUrl + '/GetAll',
        updateById: baseCaiDatApiUrl + '/UpdateById',
        restore: baseCaiDatApiUrl + '/Restore'
    },
    taiKhoan: {
        getAll: baseTaiKhoanApiUrl + '/GetAll',
        getById: baseTaiKhoanApiUrl + '/GetById',
        create: baseTaiKhoanApiUrl + '/Create',
        createMultiple: baseTaiKhoanApiUrl + '/CreateMultiple',
        updateById: baseTaiKhoanApiUrl + '/UpdateById',
        temporarilyDeleteById: baseTaiKhoanApiUrl + '/TemporarilyDeleteById',
        permanentlyDeleteById: baseTaiKhoanApiUrl + '/PermanentlyDeleteById',
        restoreById: baseTaiKhoanApiUrl + '/RestoreById',
        login: baseTaiKhoanApiUrl + '/Login'
    },
    doAn: {
        getAll: baseDoAnApiUrl + '/GetAll',
        getById: baseDoAnApiUrl + '/GetById',
        create: baseDoAnApiUrl + '/Create',
        createMultiple: baseDoAnApiUrl + '/CreateMultiple',
        updateById: baseDoAnApiUrl + '/UpdateById',
        temporarilyDeleteById: baseDoAnApiUrl + '/TemporarilyDeleteById',
        permanentlyDeleteById: baseDoAnApiUrl + '/PermanentlyDeleteById',
        restoreById: baseDoAnApiUrl + '/RestoreById'
    }
}