using AutoMapper;
using GTMS.API.Dtos.HuongNghienCuuDto;
using GTMS.API.Dtos.DoAnDto;
using GTMS.API.Dtos.GiangVienDto;
using GTMS.API.Dtos.HocKyDto;
using GTMS.API.Dtos.HuongDanDoAnDto;
using GTMS.API.Dtos.KhoaDaoTaoDto;
using GTMS.API.Dtos.KhoaDto;
using GTMS.API.Dtos.LopDto;
using GTMS.API.Dtos.PhanBienDoAnDto;
using GTMS.API.Dtos.QuyDinhDto;
using GTMS.API.Dtos.SinhVienDto;
using GTMS.API.Dtos.TaiKhoanDto;
using GTMS.API.Dtos.ThanhVienHDBV;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTMS.API.Dtos.CaiDatDto;

namespace GTMS.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMapForChuDe();
            CreateMapForDoAn();
            CreateMapForGiangVien();
            CreateMapForHocKy();
            CreateMapForHuongDanDoAn();
            CreateMapForKhoa();
            CreateMapForKhoaDaoTao();
            CreateMapForLop();
            CreateMapForPhanBienDoAn();
            CreateMapForQuyDinh();
            CreateMapForSinhVien();
            CreateMapForTaiKhoan();
            CreateMapForThanhVienHDBV();
            CreateMapForCaiDat();
            CreateMapForQuyDinh();
        }

        private void CreateMapForChuDe()
        {
            CreateMap<HuongNghienCuu, HuongNghienCuuForListDto>();
            CreateMap<HuongNghienCuu, HuongNghienCuuForViewDto>();
            CreateMap<HuongNghienCuu, HuongNghienCuuForCreateDto >();
            CreateMap<HuongNghienCuu, HuongNghienCuuForUpdateDto>();
            CreateMap<HuongNghienCuuForCreateDto, HuongNghienCuu>();
            CreateMap<HuongNghienCuuForUpdateDto, HuongNghienCuu>();
        }

        private void CreateMapForDoAn()
        {
            CreateMap<DoAn, DoAnForListDto>();
            CreateMap<DoAn, DoAnForViewDto>();
            CreateMap<DoAn, DoAnForCreateDto>();
            CreateMap<DoAn, DoAnForUpdateDto>();
            CreateMap<DoAnForCreateDto, DoAn>();
            CreateMap<DoAnForUpdateDto, DoAn>();
        }

        private void CreateMapForGiangVien()
        {
            CreateMap<GiangVien, GiangVienForListDto>();
            CreateMap<GiangVien, GiangVienForViewDto>();
            CreateMap<GiangVien, GiangVienForCreateDto>();
            CreateMap<GiangVien, GiangVienForUpdateDto>();
            CreateMap<GiangVienForCreateDto, GiangVien>();
            CreateMap<GiangVienForUpdateDto, GiangVien>();
        }
        
        private void CreateMapForHocKy()
        {
            CreateMap<HocKy, HocKyForListDto>();
            CreateMap<HocKy, HocKyForViewDto>();
            CreateMap<HocKy, HocKyForCreateDto>();
            CreateMap<HocKy, HocKyForUpdateDto>();
            CreateMap<HocKyForCreateDto, HocKy>();
            CreateMap<HocKyForUpdateDto, HocKy>();
        }
        
        private void CreateMapForHuongDanDoAn()
        {
            CreateMap<HuongDanDoAn, HuongDanDoAnForListDto>();
            CreateMap<HuongDanDoAn, HuongDanDoAnForViewDto>();
            CreateMap<HuongDanDoAn, HuongDanDoAnForCreateDto>();
            CreateMap<HuongDanDoAn, HuongDanDoAnForUpdateDto>();
            CreateMap<HuongDanDoAnForCreateDto, HuongDanDoAn>();
            CreateMap<HuongDanDoAnForUpdateDto, HuongDanDoAn>();
        }

        private void CreateMapForKhoaDaoTao()
        {
            CreateMap<KhoaDaoTao, KhoaDaoTaoForListDto>();
            CreateMap<KhoaDaoTao, KhoaDaoTaoForViewDto>();
            CreateMap<KhoaDaoTao, KhoaDaoTaoForCreateDto>();
            CreateMap<KhoaDaoTao, KhoaDaoTaoForUpdateDto>();
            CreateMap<KhoaDaoTaoForCreateDto, KhoaDaoTao>();
            CreateMap<KhoaDaoTaoForUpdateDto, KhoaDaoTao>();
        }

        private void CreateMapForKhoa()
        {
            CreateMap<Khoa, KhoaForListDto>();
            CreateMap<Khoa, KhoaForViewDto>();
            CreateMap<Khoa, KhoaForCreateDto>();
            CreateMap<Khoa, KhoaForUpdateDto>();
            CreateMap<KhoaForCreateDto, Khoa>();
            CreateMap<KhoaForUpdateDto, Khoa>();
        }

        private void CreateMapForLop()
        {
            CreateMap<Lop, LopForListDto>();
            CreateMap<Lop, LopForViewDto>();
            CreateMap<Lop, LopForCreateDto>();
            CreateMap<Lop, LopForUpdateDto>();
            CreateMap<LopForCreateDto, Lop>();
            CreateMap<LopForUpdateDto, Lop>();
        }

        private void CreateMapForPhanBienDoAn()
        {
            CreateMap<PhanBienDoAn, PhanBienDoAnForListDto>();
            CreateMap<PhanBienDoAn, PhanBienDoAnForViewDto>();
            CreateMap<PhanBienDoAn, PhanBienDoAnForCreateDto>();
            CreateMap<PhanBienDoAn, PhanBienDoAnForUpdateDto>();
            CreateMap<PhanBienDoAnForCreateDto, PhanBienDoAn>();
            CreateMap<PhanBienDoAnForUpdateDto, PhanBienDoAn>();

        }

        private void CreateMapForSinhVien()
        {
            CreateMap<SinhVien, SinhVienForListDto>();
            CreateMap<SinhVien, SinhVienForViewDto>();
            CreateMap<SinhVien, SinhVienForCreateDto>();
            CreateMap<SinhVien, SinhVienForUpdateDto>();
            CreateMap<SinhVienForCreateDto, SinhVien>();
            CreateMap<SinhVienForUpdateDto, SinhVien>();
        }

        private void CreateMapForTaiKhoan()
        {
            CreateMap<TaiKhoan, TaiKhoanForListDto>();
            CreateMap<TaiKhoan, TaiKhoanForViewDto>();
            CreateMap<TaiKhoan, TaiKhoanForCreateDto>();
            CreateMap<TaiKhoan, TaiKhoanForUpdateDto>();
            CreateMap<TaiKhoan, TaiKhoanForLoginDto>();
            CreateMap<TaiKhoanForCreateDto, TaiKhoan>();
            CreateMap<TaiKhoanForUpdateDto, TaiKhoan>();
        }

        private void CreateMapForThanhVienHDBV()
        {
            CreateMap<ThanhVienHDBV, ThanhVienHDBVForListDto>();
            CreateMap<ThanhVienHDBV, ThanhVienHDBVForViewDto>();
            CreateMap<ThanhVienHDBV, ThanhVienHDBVForCreateDto>();
            CreateMap<ThanhVienHDBV, ThanhVienHDBVForUpdateDto>();
            CreateMap<ThanhVienHDBVForCreateDto, ThanhVienHDBV>();
            CreateMap<ThanhVienHDBVForUpdateDto, ThanhVienHDBV>();
        }
     
        private void CreateMapForCaiDat()
        {
            CreateMap<CaiDat, CaiDatForViewDto>();
            CreateMap<CaiDat, CaiDatForUpdateDto>();
            CreateMap<CaiDatForUpdateDto, CaiDat>();
        }

        private void CreateMapForQuyDinh()
        {
            CreateMap<QuyDinh, QuyDinhForViewDto>();
            CreateMap<QuyDinh, QuyDinhForViewDto>();
            CreateMap<QuyDinh, QuyDinhForUpdateDto>();
            CreateMap<QuyDinh, QuyDinhForCreateDto>();
            CreateMap<QuyDinhForCreateDto, QuyDinh>();
            CreateMap<QuyDinhForUpdateDto, QuyDinh>();
        }
    }
}
