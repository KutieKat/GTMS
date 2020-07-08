using GTMS.API.Dtos;
using GTMS.API.Dtos.KhoaDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.KhoaData
{
    public interface IKhoaRepository
    {
        Task<PagedList<Khoa>> GetAll(KhoaParams userParams);
        Task<Khoa> GetById(int id);
        Task<Khoa> Create(KhoaForCreateDto khoa);
        Task<Khoa> UpdateById(int id, KhoaForUpdateDto khoa);
        Task<Khoa> TemporarilyDeleteById(int id);
        Task<Khoa> RestoreById(int id);
        Task<Khoa> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(KhoaParams userParams);
        ValidationResultDto ValidateBeforeCreate(KhoaForCreateDto khoa);
        ValidationResultDto ValidateBeforeCreateMultiple(ICollection<KhoaForCreateMultipleDto> khoa);
        ValidationResultDto ValidateBeforeUpdate(int id, KhoaForUpdateDto khoa);
        Task<ICollection<Khoa>> CreateMultiple(ICollection<KhoaForCreateMultipleDto> danhSachKhoa);
    }
}
