using GTMS.API.Dtos.PhanBienDoAnDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.PhanBienDoAnData
{
    public class PhanBienDoAnRepository : IPhanBienDoAnRepository
    {
        private readonly DataContext _context;
        private int _total;
        private bool _hasNextPage;
        public PhanBienDoAnRepository(DataContext context)
        {
            _context = context;
            _total = 0;
            _hasNextPage = false;
        }
        public int Count()
        {
            return _total;
        }

        public async Task<PhanBienDoAn> Create(PhanBienDoAnForCreateDto phanBienDoAn)
        {
            var newPhanBienDoAn = new PhanBienDoAn
            {
                MaDoAn = phanBienDoAn.MaDoAn,
                MaGiangVien = phanBienDoAn.MaGiangVien,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachPhanBienDoAn.AddAsync(newPhanBienDoAn);
            await _context.SaveChangesAsync();
            return newPhanBienDoAn;
        }

        public async Task<PhanBienDoAn> DeleteById(int maDoAn, string maGiangVien)
        {
            var phanBienDoAnToDelete = await _context.DanhSachPhanBienDoAn.FirstOrDefaultAsync(x => x.MaDoAn == maDoAn && x.MaGiangVien == maGiangVien);

            _context.DanhSachPhanBienDoAn.Remove(phanBienDoAnToDelete);
            await _context.SaveChangesAsync();
            return phanBienDoAnToDelete;
        }

        public async Task<PagedList<PhanBienDoAn>> GetAll(PhanBienDoAnParams userParams)
        {
            var result = _context.DanhSachPhanBienDoAn.Include(x => x.DoAn).Include(x=> x.GiangVien).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var maDoAn = userParams.MaDoAn;
            var maGiangVien = userParams.MaGiangVien;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(maGiangVien))
            {
                result = result.Where(x => x.MaGiangVien.ToLower().Contains(maGiangVien.ToLower()));
            }

            if (maDoAn > -1)
            {
                result = result.Where(x => x.MaDoAn == maDoAn);
            }

            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
            }

            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
            }

            if (trangThai > -1)
            {
                result = result.Where(x => x.TrangThai == trangThai);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaDoAn":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaDoAn);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaDoAn);
                        }
                        break;

                    case "MaGiangVien":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaGiangVien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaGiangVien);
                        }
                        break;

                    case "ThoiGianTao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianTao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianTao);
                        }
                        break;

                    case "ThoiGianCapNhat":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianCapNhat);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianCapNhat);
                        }
                        break;

                    case "TrangThai":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TrangThai);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TrangThai);
                        }
                        break;

                    default:
                        result = result.OrderByDescending(x => x.ThoiGianTao);
                        break;
                }
            }
            _total = result.Count();

            var totalPages = (_total / userParams.PageSize) + 1;
            var hasNextPage = userParams.PageNumber < totalPages ? true : false;

            _hasNextPage = hasNextPage;
            return await PagedList<PhanBienDoAn>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PhanBienDoAn> GetById(int maDoAn, string maGiangVien)
        {
            var result = await _context.DanhSachPhanBienDoAn.Include(x => x.DoAn).Include(x => x.GiangVien).FirstOrDefaultAsync(x => x.MaDoAn == maDoAn && x.MaGiangVien == maGiangVien);

            return result;
        }

        public bool HasNextPage()
        {
            return _hasNextPage;
        }
        public async Task<PhanBienDoAn> UpdateById(int maDoAn, string maGiangVien, PhanBienDoAnForUpdateDto phanBienDoAn)
        {
            var phanBienDoAnToUpdate = new PhanBienDoAn
            {
                MaDoAn = phanBienDoAn.MaDoAn,
                MaGiangVien = phanBienDoAn.MaGiangVien,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = phanBienDoAn.TrangThai
            };

            _context.DanhSachPhanBienDoAn.Update(phanBienDoAnToUpdate);
            await _context.SaveChangesAsync();
            return phanBienDoAnToUpdate;
        }
    }
}