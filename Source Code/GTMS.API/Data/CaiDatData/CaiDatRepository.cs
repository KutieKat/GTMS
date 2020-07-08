using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTMS.API.Dtos;
using GTMS.API.Dtos.CaiDatDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GTMS.API.Data.CaiDatData
{
    public class CaiDatRepository : ICaiDatRepository
    {
        private readonly DataContext _context;

        public CaiDatRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<CaiDat> UpdateById(int id, CaiDatForUpdateDto caiDat)
        {
            var oldRecord = await _context.DanhSachCaiDat.AsNoTracking().FirstOrDefaultAsync();

            var caiDatToUpdate = new CaiDat
            {
                MaCaiDat = id,
                TenDonViChuQuan = caiDat.TenDonViChuQuan,
                TenKhoa = caiDat.TenKhoa,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = caiDat.TrangThai
            };

            _context.DanhSachCaiDat.Update(caiDatToUpdate);
            await _context.SaveChangesAsync();

            return caiDatToUpdate;
        }

        public async Task<CaiDat> Restore()
        {
            var caiDatToRestore = await _context.DanhSachCaiDat.FirstOrDefaultAsync();

            caiDatToRestore.TenDonViChuQuan = "Trường đại học Công nghệ thông tin";
            caiDatToRestore.TenKhoa = "Khoa Công nghệ phần mềm";
            caiDatToRestore.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachCaiDat.Update(caiDatToRestore);
            await _context.SaveChangesAsync();

            return caiDatToRestore;
        }

        public async Task<CaiDat> GetAll()
        {
            var result = await _context.DanhSachCaiDat.FirstOrDefaultAsync();

            return result;
        }
    }
}
