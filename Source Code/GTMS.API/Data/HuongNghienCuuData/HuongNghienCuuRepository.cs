using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTMS.API.Dtos;
using GTMS.API.Dtos.HuongNghienCuuDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GTMS.API.Data.HuongNghienCuuData
{
    public class HuongNghienCuuRepository : IHuongNghienCuuRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public HuongNghienCuuRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<HuongNghienCuu> Create(HuongNghienCuuForCreateDto huongNghienCuu)
        {
            var danhSachHuongNghienCuu = await _context.DanhSachHuongNghienCuu.OrderByDescending(x => x.MaHuongNghienCuu).FirstOrDefaultAsync();
            var maHuongNghienCuu = 0;

            if (danhSachHuongNghienCuu == null)
            {
                maHuongNghienCuu = 0;
            }
            else
            {
                maHuongNghienCuu = danhSachHuongNghienCuu.MaHuongNghienCuu + 1;
            }
            var newHuongNghienCuu = new HuongNghienCuu
            {
                MaHuongNghienCuu = maHuongNghienCuu,
                TenHuongNghienCuu = huongNghienCuu.TenHuongNghienCuu,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachHuongNghienCuu.AddAsync(newHuongNghienCuu);
            await _context.SaveChangesAsync();

            return newHuongNghienCuu;
        }

        public async Task<HuongNghienCuu> PermanentlyDeleteById(int id)
        {
            var huongNghienCuuToDelete = await _context.DanhSachHuongNghienCuu.FirstOrDefaultAsync(x => x.MaHuongNghienCuu == id);

            _context.DanhSachHuongNghienCuu.Remove(huongNghienCuuToDelete);
            await _context.SaveChangesAsync();

            return huongNghienCuuToDelete;
        }

        public async Task<PagedList<HuongNghienCuu>> GetAll(HuongNghienCuuParams userParams)
        {
            var result = _context.DanhSachHuongNghienCuu.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenHuongNghienCuu.ToLower().Contains(keyword.ToLower()) || x.MaHuongNghienCuu.ToString() == keyword);
            }

            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
            }

            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
            }

            if (trangThai == -1 || trangThai == 1)
            {
                result = result.Where(x => x.TrangThai == trangThai);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaHuongNghienCuu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaHuongNghienCuu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaHuongNghienCuu);
                        }
                        break;

                    case "TenHuongNghienCuu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenHuongNghienCuu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenHuongNghienCuu);
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

            _totalItems = result.Count();
            _totalPages = (int)Math.Ceiling((double)_totalItems / (double)userParams.PageSize);

            return await PagedList<HuongNghienCuu>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<HuongNghienCuu> GetById(int id)
        {
            var result = await _context.DanhSachHuongNghienCuu.FirstOrDefaultAsync(x => x.MaHuongNghienCuu == id);
            return result;
        }

        public int GetTotalItems()
        {
            return _totalItems;
        }

        public int GetTotalPages()
        {
            return _totalPages;
        }

        public async Task<HuongNghienCuu> UpdateById(int id, HuongNghienCuuForUpdateDto huongNghienCuu)
        {
            var oldRecord = await _context.DanhSachKhoa.AsNoTracking().FirstOrDefaultAsync(x => x.MaKhoa == id);
            var huongNghienCuuToUpdate = new HuongNghienCuu
            {
                MaHuongNghienCuu = id,
                TenHuongNghienCuu = huongNghienCuu.TenHuongNghienCuu,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = huongNghienCuu.TrangThai
            };

            _context.DanhSachHuongNghienCuu.Update(huongNghienCuuToUpdate);
            await _context.SaveChangesAsync();

            return huongNghienCuuToUpdate;
        }

        public async Task<HuongNghienCuu> TemporarilyDeleteById(int id)
        {
            var chuDeToTemporarilyDelete = await _context.DanhSachHuongNghienCuu.FirstOrDefaultAsync(x => x.MaHuongNghienCuu == id);

            chuDeToTemporarilyDelete.TrangThai = -1;
            chuDeToTemporarilyDelete.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachHuongNghienCuu.Update(chuDeToTemporarilyDelete);
            await _context.SaveChangesAsync();

            return chuDeToTemporarilyDelete;
        }

        public async Task<HuongNghienCuu> RestoreById(int id)
        {
            var chuDeToRestoreById = await _context.DanhSachHuongNghienCuu.FirstOrDefaultAsync(x => x.MaHuongNghienCuu == id);

            chuDeToRestoreById.TrangThai = 1;
            chuDeToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachHuongNghienCuu.Update(chuDeToRestoreById);
            await _context.SaveChangesAsync();

            return chuDeToRestoreById;
        }

        public Object GetStatusStatistics(HuongNghienCuuParams userParams)
        {
            var result = _context.DanhSachHuongNghienCuu.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenHuongNghienCuu.ToLower().Contains(keyword.ToLower()) || x.MaHuongNghienCuu.ToString() == keyword);
            }

            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
            }

            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaHuongNghienCuu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaHuongNghienCuu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaHuongNghienCuu);
                        }
                        break;

                    case "TenHuongNghienCuu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenHuongNghienCuu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenHuongNghienCuu);
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
            var all = result.Count();
            var active = result.Count(x => x.TrangThai == 1);
            var inactive = result.Count(x => x.TrangThai == -1);

            return new
            {
                All = all,
                Active = active,
                Inactive = inactive
            };
        }
        public ValidationResultDto ValidateBeforeCreate(HuongNghienCuuForCreateDto huongNghienCuu)
        {
            var totalTenHuongNghienCuu = _context.DanhSachHuongNghienCuu.Count(x => x.TenHuongNghienCuu.ToLower().Contains(huongNghienCuu.TenHuongNghienCuu.ToLower()));
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenHuongNghienCuu >= 1)
            {
                Errors.Add("tenHuongNghienCuu", new string[] { "tenHuongNghienCuu is duplicated!" });
                

                return new ValidationResultDto
                {
                    IsValid = false,
                    Errors = Errors
                };
            }
            else
            {
                return new ValidationResultDto
                {
                    IsValid = true
                };
            }
        }

        public ValidationResultDto ValidateBeforeCreateMultiple(ICollection<HuongNghienCuuForCreateMultipleDto> danhSachHuongNghienCuu)
        {
            var isValid = true;

            for (int i = 0; i < danhSachHuongNghienCuu.Count; i++)
            {
                var huongNghienCuu = danhSachHuongNghienCuu.ElementAt(i);
                var huongNghienCuuToString = danhSachHuongNghienCuu.ElementAt(i).ToString();
                var count = _context.DanhSachHuongNghienCuu.Count(x => x.MaHuongNghienCuu == huongNghienCuu.MaHuongNghienCuu);
                var hasEnoughFields = huongNghienCuuToString.Contains("MaHuongNghienCuu") && huongNghienCuuToString.Contains("TenHuongNghienCuu");
                var isOkayToCreate = true;

                var totalTenHuongNghienCuu = _context.DanhSachHuongNghienCuu.Count(x=>x.TenHuongNghienCuu.ToLower().Contains(huongNghienCuu.TenHuongNghienCuu.ToLower()));
                

                if (totalTenHuongNghienCuu >= 1)
                {
                    isOkayToCreate = false;
                }

                if (count >= 1 || !hasEnoughFields || !isOkayToCreate)
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                return new ValidationResultDto
                {
                    IsValid = true
                };
            }
            else
            {
                IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();
                Errors.Add("createMultiple", new string[] { "createMultiple failed!" });

                return new ValidationResultDto
                {
                    IsValid = false,
                    Errors = Errors
                };
            }
        }



        public ValidationResultDto ValidateBeforeUpdate(int id, HuongNghienCuuForUpdateDto huongNghienCuu)
        {
            var totalTenHuongNghienCuu = _context.DanhSachHuongNghienCuu.Count(x => x.MaHuongNghienCuu != id && x.TenHuongNghienCuu.ToLower().Contains(huongNghienCuu.TenHuongNghienCuu.ToLower()));
            
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenHuongNghienCuu > 0)
            {
                    Errors.Add("tenHuongNghienCuu", new string[] { "tenHuongNghienCuu is duplicated!" });
                return new ValidationResultDto
                {
                    IsValid = false,
                    Errors = Errors
                };
            }
            else
            {
                return new ValidationResultDto
                {
                    IsValid = true
                };
            }
        }

        public async Task<ICollection<HuongNghienCuu>> CreateMultiple(ICollection<HuongNghienCuuForCreateMultipleDto> danhSachHuongNghienCuu)
        {
            ICollection<HuongNghienCuu> temp = new List<HuongNghienCuu>();

            for (int i = 0; i < danhSachHuongNghienCuu.Count; i++)
            {
                var huongNghienCuu = danhSachHuongNghienCuu.ElementAt(i);

                var newHuongNghienCuu = new HuongNghienCuu
                {
                    MaHuongNghienCuu = huongNghienCuu.MaHuongNghienCuu,
                    TenHuongNghienCuu = huongNghienCuu.TenHuongNghienCuu,
                    ThoiGianTao = huongNghienCuu.ThoiGianTao,
                    ThoiGianCapNhat = huongNghienCuu.ThoiGianCapNhat,
                    TrangThai = huongNghienCuu.TrangThai
                };

                temp.Add(newHuongNghienCuu);

                await _context.DanhSachHuongNghienCuu.AddAsync(newHuongNghienCuu);
                await _context.SaveChangesAsync();
            }

            return temp;
        }
    }
}
