using GTMS.API.Dtos.HuongDanDoAnDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.HuongDanDoAnData
{
    public interface IHuongDanDoAnRepository
    {
        Task<PagedList<HuongDanDoAn>> GetAll(HuongDanDoAnParams userParams);
        Task<HuongDanDoAn> Create(HuongDanDoAnForCreateDto huongDanDoAn);
        Task<HuongDanDoAn> GetById(string MaGiangVien, int MaDoAn);
        Task<HuongDanDoAn> UpdateById(string MaGiangVien, int MaDoAn, HuongDanDoAnForUpdateDto huongDanDoAn);
        Task<HuongDanDoAn> DeleteById(string MaGiangVien, int MaDoAn);
        int Count();
        bool HasNextPage();
    }
}
