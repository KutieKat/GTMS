using GTMS.API.Dtos.TaiKhoanDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data
{
    public interface ITaiKhoanRepository
    {
        Task<PagedList<TaiKhoan>> GetAll(TaiKhoanParams userParams);
        Task<TaiKhoan> GetById(int id);
        Task<TaiKhoan> TaoTaiKhoan(TaiKhoan taiKhoan, string matKhau);
        Task<TaiKhoan> DangNhap(string tenDangNhap, string matKhau);
        Task<bool> TaiKhoanTonTai(string tenDangNhap);
        Task<TaiKhoan> UpdateById(int id, TaiKhoanForUpdateDto taiKhoan);
        Task<TaiKhoan> DeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(TaiKhoanParams taiKhoanParams);
        Task<TaiKhoan> TemporarilyDeleteById(int id);
        Task<TaiKhoan> RestoreById(int id);
        Task<TaiKhoan> PermanentlyDeleteById(int id);
        Task<TaiKhoan> ChangePassword(int id, TaiKhoanForChangePasswordDto taiKhoan);
    }
}
