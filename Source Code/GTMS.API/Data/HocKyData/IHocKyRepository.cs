using GTMS.API.Dtos;
using GTMS.API.Dtos.HocKyDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GTMS.API.Data.HocKyData
{
    public interface IHocKyRepository
    {
        Task<PagedList<HocKy>> GetAll(HocKyParams userParams);
        Task<HocKy> GetById(int id);
        Task<HocKy> Create(HocKyForCreateDto hocKy);
        Task<HocKy> UpdateById(int id, HocKyForUpdateDto hocKy);
        Task<HocKy> TemporarilyDeleteById(int id);
        Task<HocKy> RestoreById(int id);
        Task<HocKy> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(HocKyParams userParams);
        ValidationResultDto ValidateBeforeCreate(HocKyForCreateDto hocKy);
        ValidationResultDto ValidateBeforeCreateMultiple(ICollection<HocKyForCreateMultipleDto> hocKy);
        ValidationResultDto ValidateBeforeUpdate(int id, HocKyForUpdateDto hocKy);
        Task<ICollection<HocKy>> CreateMultiple(ICollection<HocKyForCreateMultipleDto> danhSachHocKy);
    }
}
