using GTMS.API.Dtos.ThanhVienHDBV;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.ThanhVienHoiDongBaoVeData
{
    public interface IThanhVienHDBVRepository
    {
        Task<PagedList<ThanhVienHDBV>> GetAll(ThanhVienHDBVParams userParams);
        Task<ThanhVienHDBV> Create(ThanhVienHDBVForCreateDto huongDanDoAn);
        Task<ThanhVienHDBV> GetById(int id);
        Task<ThanhVienHDBV> UpdateById(int id, ThanhVienHDBVForUpdateDto thanhVienHDBV);
        Task<ThanhVienHDBV> DeleteById(int id);
        int Count();
        bool HasNextPage();
    }
}
