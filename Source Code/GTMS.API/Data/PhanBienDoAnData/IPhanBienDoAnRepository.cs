using GTMS.API.Dtos.PhanBienDoAnDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.PhanBienDoAnData
{
    public interface IPhanBienDoAnRepository
    {
        Task<PagedList<PhanBienDoAn>> GetAll(PhanBienDoAnParams userParams);
        Task<PhanBienDoAn> GetById(int maDoAn, string maGiangVien);
        Task<PhanBienDoAn> Create(PhanBienDoAnForCreateDto phanBienDoAn);
        Task<PhanBienDoAn> UpdateById(int maDoAn, string maGiangVien, PhanBienDoAnForUpdateDto phanBienDoAn);
        Task<PhanBienDoAn> DeleteById(int maDoAn, string maGiangVien);
        int Count();
        bool HasNextPage();
    }
}
