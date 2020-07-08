using GTMS.API.Dtos;
using GTMS.API.Dtos.KhoaDaoTaoDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.KhoaDaoTaoData
{
    public interface IKhoaDaoTaoRepository
    {
        Task<PagedList<KhoaDaoTao>> GetAll(KhoaDaoTaoParams userParams);
        Task<KhoaDaoTao> GetById(int id);
        Task<KhoaDaoTao> Create(KhoaDaoTaoForCreateDto khoaDaoTao);
        Task<KhoaDaoTao> UpdateById(int id,KhoaDaoTaoForUpdateDto khoaDaoTao);
        Task<KhoaDaoTao> TemporarilyDeleteById(int id);
        Task<KhoaDaoTao> RestoreById(int id);
        Task<KhoaDaoTao> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(KhoaDaoTaoParams userParams);
        ValidationResultDto ValidateBeforeCreate(KhoaDaoTaoForCreateDto khoaDaoTao);
        ValidationResultDto ValidateBeforeCreateMultiple(ICollection<KhoaDaoTaoForCreateMultipleDto> khoaDaoTao);
        ValidationResultDto ValidateBeforeUpdate(int id, KhoaDaoTaoForUpdateDto khoaDaoTao);
        Task<ICollection<KhoaDaoTao>> CreateMultiple(ICollection<KhoaDaoTaoForCreateMultipleDto> danhSachKhoaDaoTao);
    }
}
