using GTMS.API.Dtos;
using GTMS.API.Dtos.SinhVienDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.SinhVienData
{
    public interface ISinhVienRepository
    {
        Task<PagedList<SinhVien>> GetAll(SinhVienParams userParams);
        Task<SinhVien> GetById(string id);
        Task<SinhVien> Create(SinhVienForCreateDto sinhVien);
        Task<SinhVien> UpdateById(string id, SinhVienForUpdateDto sinhVien);
        Task<SinhVien> TemporarilyDeleteById(string id);
        Task<SinhVien> RestoreById(string id);
        Task<SinhVien> PermanentlyDeleteById(string id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(SinhVienParams userParams);
        ValidationResultDto ValidateBeforeCreate(SinhVienForCreateDto sinhVien);
        ValidationResultDto ValidateBeforeCreateMultiple(ICollection<SinhVienForCreateMultipleDto> sinhVien);
        ValidationResultDto ValidateBeforeUpdate(string id, SinhVienForUpdateDto sinhVien);
        Task<ICollection<SinhVien>> CreateMultiple(ICollection<SinhVienForCreateMultipleDto> danhSachSinhVien);
    }
}
