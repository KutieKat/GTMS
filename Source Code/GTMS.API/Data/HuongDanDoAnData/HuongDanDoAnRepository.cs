using GTMS.API.Dtos.HuongDanDoAnDto;
using GTMS.API.Dtos.KhoaDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.HuongDanDoAnData
{
    public class HuongDanDoAnRepository : IHuongDanDoAnRepository
    {
        private readonly DataContext _context;
        private int _total;
        private bool _hasNextPage;
        public HuongDanDoAnRepository(DataContext context)
        {
            _context = context;
            _total = 0;
            _hasNextPage = false;
        }
        public int Count()
        {
            return _total;
        }
        public async Task<HuongDanDoAn> Create(HuongDanDoAnForCreateDto huongDanDoAn)
        {
            var newHuongDanDoAn = new HuongDanDoAn
            {
                MaDoAn = huongDanDoAn.MaDoAn,
                MaGiangVien = huongDanDoAn.MaGiangVien,
                //Diem = huongDanDoAn.Diem,
                NhanXet = huongDanDoAn.NhanXet,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachHuongDanDoAn.AddAsync(newHuongDanDoAn);
            await _context.SaveChangesAsync();
            return newHuongDanDoAn;
        }

        public async Task<HuongDanDoAn> DeleteById(string maGiangVien, int maDoAn)
        {
            var huongDanDoAnToDelete = await _context.DanhSachHuongDanDoAn.FirstOrDefaultAsync(x => x.MaGiangVien == maGiangVien && x.MaDoAn == maDoAn);

            _context.DanhSachHuongDanDoAn.Remove(huongDanDoAnToDelete);
            await _context.SaveChangesAsync();
            return huongDanDoAnToDelete;
        }

        public async Task<PagedList<HuongDanDoAn>> GetAll(HuongDanDoAnParams userParams)
        {
            var result = _context.DanhSachHuongDanDoAn.Include(x => x.GiangVien).Include(x => x.DoAn).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var maGiangVien = userParams.MaGiangVien;
            var maDoAn = userParams.MaDoAn;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.NhanXet.ToLower().Contains(keyword.ToLower()));
            }

            if (!string.IsNullOrEmpty(maGiangVien))
            {
                result = result.Where(x => x.MaGiangVien.ToLower().Contains(keyword.ToLower()));
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

                   /* //case "Diem":
                     //   if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.Diem);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.Diem);
                        }
                        break;*/

                    case "NhanXet":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NhanXet);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NhanXet);
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
            return await PagedList<HuongDanDoAn>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<HuongDanDoAn> GetById(string maGiangVien, int maDoAn)
        {
            var result = await _context.DanhSachHuongDanDoAn.Include(x => x.GiangVien).Include(x => x.DoAn).FirstOrDefaultAsync(x => x.MaGiangVien == maGiangVien && x.MaDoAn == maDoAn);
            return result;
        }
        public bool HasNextPage()
        {
            return _hasNextPage;
        }
        public async Task<HuongDanDoAn> UpdateById(string maGiangVien, int maDoAn, HuongDanDoAnForUpdateDto huongDanDoAn)
        {
            var huongDanDoAnToUpdate = new HuongDanDoAn
            {
                MaGiangVien = maGiangVien,
                MaDoAn = maDoAn,
                //Diem = huongDanDoAn.Diem,
                NhanXet = huongDanDoAn.NhanXet,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = huongDanDoAn.TrangThai
            };

            _context.DanhSachHuongDanDoAn.Update(huongDanDoAnToUpdate);
            await _context.SaveChangesAsync();
            return huongDanDoAnToUpdate;
        }
 
    }
}