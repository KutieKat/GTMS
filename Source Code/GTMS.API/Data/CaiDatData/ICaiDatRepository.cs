using GTMS.API.Dtos;
using GTMS.API.Dtos.CaiDatDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.CaiDatData
{
    public interface ICaiDatRepository
    {
        Task<CaiDat> GetAll();
        Task<CaiDat> UpdateById(int id, CaiDatForUpdateDto caiDat);
        Task<CaiDat> Restore();
    }
}
