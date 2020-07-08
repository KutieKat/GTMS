//using GTMS.API.Dtos.ThanhVienHDBV;
//using GTMS.API.Helpers;
//using GTMS.API.Helpers.Params;
//using GTMS.API.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GTMS.API.Data.ThanhVienHoiDongBaoVeData
//{
//    public class ThanhVienHDBVRepository : IThanhVienHDBVRepository
//    {
//        private readonly DataContext _context;
//        private int _total;
//        private bool _hasNextPage;
//        public ThanhVienHDBVRepository(DataContext context)
//        {
//            _context = context;
//            _total = 0;
//            _hasNextPage = false;
//        }
//        public int Count()
//        {
//            return _total;
//        }
//        public async Task<ThanhVienHDBV> Create(ThanhVienHDBVForCreateDto thanhVienHDBV)
//        {
//            var newThanhVienHDBV = new ThanhVienHDBV
//            { 
//                MaGiangVien = thanhVienHDBV.MaGiangVien,
//                MaChucVuHDBV = thanhVienHDBV.MaChucVuHDBV,
//                ThoiGianTao = DateTime.Now,
//                ThoiGianCapNhat = DateTime.Now,
//                TrangThai = 1
//            };

//            await _context.DanhSachThanhVienHDBV.AddAsync(newThanhVienHDBV);
//            await _context.SaveChangesAsync();
//            return newThanhVienHDBV;
//        }

//        public async Task<ThanhVienHDBV> DeleteById(int id)
//        {
//            var thanhVienHDBVToDelete = await _context.DanhSachThanhVienHDBV.FirstOrDefaultAsync(x => x.MaThanhVienHDBV == id);

//            _context.DanhSachThanhVienHDBV.Remove(thanhVienHDBVToDelete);
//            await _context.SaveChangesAsync();
//            return thanhVienHDBVToDelete;
//        }

//        public async Task<PagedList<ThanhVienHDBV>> GetAll(ThanhVienHDBVParams userParams)
//        {
//            var result = _context.DanhSachThanhVienHDBV.Include(x => x.GiangVien).Include(x =>x.ChucVuHDBV).AsQueryable();
//            var sortField = userParams.SortField;
//            var sortOrder = userParams.SortOrder;
//            var maGiangVien = userParams.MaGiangVien;
//            var maChucVuHDBV = userParams.MaChucVuHDBV;
//            var keyword = userParams.Keyword;   
//            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
//            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
//            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
//            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
//            var trangThai = userParams.TrangThai;

//            if (!string.IsNullOrEmpty(keyword))
//            {
//                result = result.Where(x => x.NhanXet.ToLower().Contains(keyword.ToLower()) || x.MaThanhVienHDBV.ToString() == keyword);
//            }

//            if (!string.IsNullOrEmpty(maGiangVien))
//            {
//                result = result.Where(x => x.MaGiangVien.ToLower().Contains(maGiangVien.ToLower()));
//            }

//            if (maChucVuHDBV > -1)
//            {
//                result = result.Where(x => x.MaChucVuHDBV == maChucVuHDBV);
//            }

//            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
//            {
//                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
//            }

//            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
//            {
//                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
//            }

//            if (trangThai > -1)
//            {
//                result = result.Where(x => x.TrangThai == trangThai);
//            }

//            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
//            {
//                switch (sortField)
//                {
//                    case "MaThanhVienHDBV":
//                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
//                        {
//                            result = result.OrderBy(x => x.MaThanhVienHDBV);
//                        }
//                        else
//                        {
//                            result = result.OrderByDescending(x => x.MaThanhVienHDBV);
//                        }
//                        break;

//                    case "MaGiangVien":
//                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
//                        {
//                            result = result.OrderBy(x => x.MaGiangVien);
//                        }
//                        else
//                        {
//                            result = result.OrderByDescending(x => x.MaGiangVien);
//                        }
//                        break;

//                    case "MaChucVuHDBV":
//                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
//                        {
//                            result = result.OrderBy(x => x.MaChucVuHDBV);
//                        }
//                        else
//                        {
//                            result = result.OrderByDescending(x => x.MaChucVuHDBV);
//                        }
//                        break;

//                    case "Diem":
//                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
//                        {
//                            result = result.OrderBy(x => x.Diem);
//                        }
//                        else
//                        {
//                            result = result.OrderByDescending(x => x.Diem);
//                        }
//                        break;

//                    case "NhanXet":
//                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
//                        {
//                            result = result.OrderBy(x => x.NhanXet);
//                        }
//                        else
//                        {
//                            result = result.OrderByDescending(x => x.NhanXet);
//                        }
//                        break;

//                    case "ThoiGianTao":
//                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
//                        {
//                            result = result.OrderBy(x => x.ThoiGianTao);
//                        }
//                        else
//                        {
//                            result = result.OrderByDescending(x => x.ThoiGianTao);
//                        }
//                        break;

//                    case "ThoiGianCapNhat":
//                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
//                        {
//                            result = result.OrderBy(x => x.ThoiGianCapNhat);
//                        }
//                        else
//                        {
//                            result = result.OrderByDescending(x => x.ThoiGianCapNhat);
//                        }
//                        break;

//                    case "TrangThai":
//                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
//                        {
//                            result = result.OrderBy(x => x.TrangThai);
//                        }
//                        else
//                        {
//                            result = result.OrderByDescending(x => x.TrangThai);
//                        }
//                        break;

//                    default:
//                        result = result.OrderByDescending(x => x.ThoiGianTao);
//                        break;
//                }
//            }
//            _total = result.Count();

//            var totalPages = (_total / userParams.PageSize) + 1;
//            var hasNextPage = userParams.PageNumber < totalPages ? true : false;

//            _hasNextPage = hasNextPage;

//            return await PagedList<ThanhVienHDBV>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
//        }

//        public async Task<ThanhVienHDBV> GetById(int id)
//        {
//            var result = await _context.DanhSachThanhVienHDBV.Include(x => x.GiangVien).Include(x => x.ChucVuHDBV).FirstOrDefaultAsync(x => x.MaThanhVienHDBV == id);

//            return result;
//        }
//        public bool HasNextPage()
//        {
//            return _hasNextPage;
//        }
//        public async Task<ThanhVienHDBV> UpdateById(int id, ThanhVienHDBVForUpdateDto thanhVienHDBV)
//        {
//            var thanhVienHDBVToUpdate = new ThanhVienHDBV
//            {
//                MaThanhVienHDBV = id,
//                MaGiangVien = thanhVienHDBV.MaGiangVien,
//                MaChucVuHDBV = thanhVienHDBV.MaChucVuHDBV,
//                Diem = thanhVienHDBV.Diem,
//                NhanXet = thanhVienHDBV.NhanXet,
//                ThoiGianCapNhat = DateTime.Now,
//                TrangThai = thanhVienHDBV.TrangThai
//            };

//            _context.DanhSachThanhVienHDBV.Update(thanhVienHDBVToUpdate);
//            await _context.SaveChangesAsync();
//            return thanhVienHDBVToUpdate;
//        }
//    }
//}
