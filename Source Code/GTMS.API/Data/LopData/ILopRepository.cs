using GTMS.API.Dtos;
using GTMS.API.Dtos.LopDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.LopData
{
    public interface ILopRepository
    {
        Task<PagedList<Lop>> GetAll(LopParams userParams);
        Task<Lop> GetById(int id);
        Task<Lop> Create(LopForCreateDto lop);
        Task<Lop> UpdateById(int id, LopForUpdateDto lop);
        Task<Lop> TemporarilyDeleteById(int id);
        Task<Lop> RestoreById(int id);
        Task<Lop> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(LopParams userParams);
        ValidationResultDto ValidateBeforeCreate(LopForCreateDto lop);
        ValidationResultDto ValidateBeforeCreateMultiple(ICollection<LopForCreateMultipleDto> lop);
        ValidationResultDto ValidateBeforeUpdate(int id, LopForUpdateDto lop);
        Task<ICollection<Lop>> CreateMultiple(ICollection<LopForCreateMultipleDto> danhSachLop);
    }
}
