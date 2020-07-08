using GTMS.API.Dtos;
using GTMS.API.Dtos.HuongNghienCuuDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.HuongNghienCuuData
{
    public interface IHuongNghienCuuRepository
    {
        Task<PagedList<HuongNghienCuu>> GetAll(HuongNghienCuuParams userParams);
        Task<HuongNghienCuu> GetById(int id);
        Task<HuongNghienCuu> Create(HuongNghienCuuForCreateDto huongNghienCuu);
        Task<HuongNghienCuu> UpdateById(int id,HuongNghienCuuForUpdateDto huongNghienCuu);
        Task<HuongNghienCuu> TemporarilyDeleteById(int id);
        Task<HuongNghienCuu> RestoreById(int id);
        Task<HuongNghienCuu> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(HuongNghienCuuParams userParams);
        ValidationResultDto ValidateBeforeCreate(HuongNghienCuuForCreateDto huongNghienCuu);
        ValidationResultDto ValidateBeforeCreateMultiple(ICollection<HuongNghienCuuForCreateMultipleDto> huongNghienCuu);
        ValidationResultDto ValidateBeforeUpdate(int id, HuongNghienCuuForUpdateDto huongNghienCuu);
        Task<ICollection<HuongNghienCuu>> CreateMultiple(ICollection<HuongNghienCuuForCreateMultipleDto> danhSachHuongNghienCuu);
    }
}
