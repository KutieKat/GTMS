using GTMS.API.Dtos.DoAnDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.DoAnData
{
    public interface IDoAnRepository
    {
        Task<PagedList<DoAn>> GetAll(DoAnParams userParams);
        Task<DoAn> GetById(int id);
        Task<DoAn> Create(DoAnForCreateDto doAn);
        Task<DoAn> UpdateById(int id, DoAnForUpdateDto doAn);
        Task<DoAn> TemporarilyDeleteById(int id);
        Task<DoAn> RestoreById(int id);
        Task<DoAn> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(DoAnParams userParams);
    }
}
