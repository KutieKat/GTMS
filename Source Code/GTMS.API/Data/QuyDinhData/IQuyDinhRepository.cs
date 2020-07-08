using GTMS.API.Dtos;
using GTMS.API.Dtos.QuyDinhDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.QuyDinhData
{
    public interface IQuyDinhRepository
    {
        Task<PagedList<QuyDinh>> GetAll(QuyDinhParams userParams);
        Task<QuyDinh> GetById(int id);
        Task<QuyDinh> GetByDate(DateTime date);
        Task<QuyDinh> Create(QuyDinhForCreateDto quyDinh);
        Task<QuyDinh> UpdateById(int id, QuyDinhForUpdateDto quyDinh);
        Task<QuyDinh> TemporarilyDeleteById(int id);
        Task<QuyDinh> RestoreById(int id);
        Task<QuyDinh> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(QuyDinhParams userParams);
        ValidationResultDto ValidateBeforeCreate(QuyDinhForCreateDto quyDinh);
        ValidationResultDto ValidateBeforeCreateMultiple(ICollection<QuyDinhForCreateMultipleDto> quyDinh);
        ValidationResultDto ValidateBeforeUpdate(int id, QuyDinhForUpdateDto quyDinh);
        Task<ICollection<QuyDinh>> CreateMultiple(ICollection<QuyDinhForCreateMultipleDto> danhSachQuyDinh);
    }
}
