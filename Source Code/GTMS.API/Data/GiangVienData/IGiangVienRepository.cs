using GTMS.API.Dtos;
using GTMS.API.Dtos.GiangVienDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.GiangVienData
{
    public interface IGiangVienRepository
    {
        Task<PagedList<GiangVien>> GetAll(GiangVienParams userParams);
        Task<GiangVien> GetById(string id);
        Task<GiangVien> Create(GiangVienForCreateDto giangVien);
        Task<GiangVien> UpdateById(string id, GiangVienForUpdateDto giangVien);
        Task<GiangVien> TemporarilyDeleteById(string id);
        Task<GiangVien> RestoreById(string id);
        Task<GiangVien> PermanentlyDeleteById(string id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(GiangVienParams userParams);
        ValidationResultDto ValidateBeforeCreate(GiangVienForCreateDto giangVien);
        ValidationResultDto ValidateBeforeCreateMultiple(ICollection<GiangVienForCreateMultipleDto> giangVien);
        ValidationResultDto ValidateBeforeUpdate(string id, GiangVienForUpdateDto giangVienVien);
        Task<ICollection<GiangVien>> CreateMultiple(ICollection<GiangVienForCreateMultipleDto> danhSacGiangVien);
    }
}
