using GTMS.API.Dtos;
using GTMS.API.Dtos.HocKyDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.HocKyData
{
    public class HocKyRepository : IHocKyRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public HocKyRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<PagedList<HocKy>> GetAll(HocKyParams userParams)
        {
            var result = _context.DanhSachHocKy.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var thoiGianBatDau = userParams.ThoiGianBatDau;
            var thoiGianKetThuc = userParams.ThoiGianKetThuc;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenHocKy.ToLower().Contains(keyword.ToLower()) || x.NamHoc.ToLower().Contains(keyword.ToLower()) || x.MaHocKy.ToString() == keyword);
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

            if (thoiGianBatDau.GetHashCode() != 0 && thoiGianKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianBatDau >= thoiGianBatDau && x.ThoiGianKetThuc <= thoiGianKetThuc);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaHocKy":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaHocKy);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaHocKy);
                        }
                        break;

                    case "TenHocKy":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenHocKy);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenHocKy);
                        }
                        break;
                    case "NamHoc":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NamHoc);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NamHoc);
                        }
                        break;

                    case "ThoiGianBatDau":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianBatDau);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianBatDau);
                        }
                        break;

                    case "ThoiGianKetThuc":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianKetThuc);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianKetThuc);
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

            return await PagedList<HocKy>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }


        public async Task<HocKy> GetById(int id)
        {
            var result = await _context.DanhSachHocKy.FirstOrDefaultAsync(x => x.MaHocKy == id);

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

        public async Task<HocKy> Create(HocKyForCreateDto hocKy)
        {
            var danhSachHocKy = await _context.DanhSachHocKy.OrderByDescending(x => x.MaHocKy).FirstOrDefaultAsync();
            var maHocKy = 0;

            if (danhSachHocKy == null)
            {
                maHocKy = 0;
            }
            else
            {
                maHocKy = danhSachHocKy.MaHocKy + 1;
            }
            var newHocKy = new HocKy
            {
                MaHocKy = maHocKy,
                TenHocKy = hocKy.TenHocKy,
                NamHoc = hocKy.NamHoc,
                ThoiGianBatDau = hocKy.ThoiGianBatDau,
                ThoiGianKetThuc = hocKy.ThoiGianKetThuc,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachHocKy.AddAsync(newHocKy);
            await _context.SaveChangesAsync();

            return newHocKy;
        }

        public async Task<HocKy> UpdateById(int id, HocKyForUpdateDto hocKy)
        {
            var oldRecord = await _context.DanhSachHocKy.AsNoTracking().FirstOrDefaultAsync(x => x.MaHocKy == id);
            var hocKyToUpdate = new HocKy
            {
                MaHocKy = id,
                TenHocKy = hocKy.TenHocKy,
                NamHoc = hocKy.NamHoc,
                ThoiGianBatDau = hocKy.ThoiGianBatDau,
                ThoiGianKetThuc = hocKy.ThoiGianKetThuc,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = hocKy.TrangThai
            };

            _context.DanhSachHocKy.Update(hocKyToUpdate);
            await _context.SaveChangesAsync();

            return hocKyToUpdate;
        }

        public async Task<HocKy> PermanentlyDeleteById(int id)
        {
            var hocKyToDelete = await _context.DanhSachHocKy.FirstOrDefaultAsync(x => x.MaHocKy == id);

            _context.DanhSachHocKy.Remove(hocKyToDelete);
            await _context.SaveChangesAsync();

            return hocKyToDelete;
        }

        public async Task<HocKy> TemporarilyDeleteById(int id)
        {
            var hocKyToTemporarilyDeleteById = await _context.DanhSachHocKy.FirstOrDefaultAsync(x => x.MaHocKy == id);

            hocKyToTemporarilyDeleteById.TrangThai = -1;
            hocKyToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachHocKy.Update(hocKyToTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return hocKyToTemporarilyDeleteById;
        }

        public async Task<HocKy> RestoreById(int id)
        {
            var hocKyToRestoreById = await _context.DanhSachHocKy.FirstOrDefaultAsync(x => x.MaHocKy == id);

            hocKyToRestoreById.TrangThai = 1;
            hocKyToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachHocKy.Update(hocKyToRestoreById);
            await _context.SaveChangesAsync();

            return hocKyToRestoreById;
        }

        public Object GetStatusStatistics(HocKyParams userParams)
        {
            var result = _context.DanhSachHocKy.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var thoiGianBatDau = userParams.ThoiGianBatDau;
            var thoiGianKetThuc = userParams.ThoiGianKetThuc;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenHocKy.ToLower().Contains(keyword.ToLower()) || x.NamHoc.ToLower().Contains(keyword.ToLower()) || x.MaHocKy.ToString() == keyword);
            }

            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
            }

            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
            }

            if (thoiGianBatDau.GetHashCode() != 0 && thoiGianKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianBatDau >= thoiGianBatDau && x.ThoiGianKetThuc <= thoiGianKetThuc);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaHocKy":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaHocKy);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaHocKy);
                        }
                        break;

                    case "TenHocKy":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenHocKy);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenHocKy);
                        }
                        break;
                    case "NamHoc":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NamHoc);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NamHoc);
                        }
                        break;

                    case "ThoiGianBatDau":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianBatDau);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianBatDau);
                        }
                        break;

                    case "ThoiGianKetThuc":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianKetThuc);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianKetThuc);
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

        public ValidationResultDto ValidateBeforeCreate(HocKyForCreateDto hocKy)
        {
            var totalTenHocKy = _context.DanhSachHocKy.Count(x => x.TenHocKy.ToLower().Contains(hocKy.TenHocKy.ToLower()));
            var totalThoiGianBatDau = _context.DanhSachHocKy.Count(x => x.ThoiGianBatDau == hocKy.ThoiGianBatDau);
            var totalThoiGianKetThuc = _context.DanhSachHocKy.Count(x => x.ThoiGianKetThuc == hocKy.ThoiGianKetThuc);
            var isThoiGianValid = hocKy.ThoiGianKetThuc >= hocKy.ThoiGianBatDau;
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenHocKy >= 1 || totalThoiGianBatDau >= 1 || totalThoiGianKetThuc >= 1 || isThoiGianValid == false)
            {
                if (totalTenHocKy >= 1)
                {
                    Errors.Add("tenHocKy", new string[] { "tenHocKy is duplicated!" });
                }

                if (totalThoiGianBatDau >= 1)
                {
                    Errors.Add("thoiGianBatDau", new string[] { "thoiGianBatDau is duplicated!" });
                }

                if (totalThoiGianKetThuc >= 1)
                {
                    Errors.Add("thoiGianKetThuc", new string[] { "thoiGianKetThuc is duplicated!" });
                }

                if (isThoiGianValid == false)
                {
                    Errors.Add("thoiGianKetThuc", new string[] { "thoiGianKetThuc must come before thoiGianBatDau!" });
                }

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

        public ValidationResultDto ValidateBeforeCreateMultiple(ICollection<HocKyForCreateMultipleDto> danhSachHocKy)
        {
            var isValid = true;

            for (int i = 0; i < danhSachHocKy.Count; i++)
            {
                var hocKy = danhSachHocKy.ElementAt(i);
                var hocKyToString = danhSachHocKy.ElementAt(i).ToString();
                var count = _context.DanhSachHocKy.Count(x => x.MaHocKy == hocKy.MaHocKy);
                var hasEnoughFields = hocKyToString.Contains("MaHocKy") && hocKyToString.Contains("TenHocKy") && hocKyToString.Contains("NamHoc") && hocKyToString.Contains("ThoiGianBatDau") && hocKyToString.Contains("ThoiGianKetThuc");
                var isOkayToCreate = true;

                var totalTenHocKy = _context.DanhSachHocKy.Count(x => x.TenHocKy.ToLower().Contains(hocKy.TenHocKy.ToLower()));
                var totalThoiGianBatDau = _context.DanhSachHocKy.Count(x => x.ThoiGianBatDau == hocKy.ThoiGianBatDau);
                var totalThoiGianKetThuc = _context.DanhSachHocKy.Count(x => x.ThoiGianKetThuc == hocKy.ThoiGianKetThuc);
                var isThoiGianValid = hocKy.ThoiGianKetThuc >= hocKy.ThoiGianBatDau;

                if (totalTenHocKy >= 1 || totalThoiGianBatDau >= 1 || totalThoiGianKetThuc >= 1 || isThoiGianValid == false)
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

        public ValidationResultDto ValidateBeforeUpdate(int id, HocKyForUpdateDto hocKy)
        {
            var totalTenHocKy = _context.DanhSachHocKy.Count(x => x.MaHocKy != id && x.TenHocKy.ToLower().Contains(hocKy.TenHocKy.ToLower()));
            var totalThoiGianBatDau = _context.DanhSachHocKy.Count(x => x.MaHocKy != id && x.ThoiGianBatDau == hocKy.ThoiGianBatDau);
            var totalThoiGianKetThuc = _context.DanhSachHocKy.Count(x => x.MaHocKy != id && x.ThoiGianKetThuc == hocKy.ThoiGianKetThuc);
            var isThoiGianValid = hocKy.ThoiGianKetThuc >= hocKy.ThoiGianBatDau;
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenHocKy > 0 || totalThoiGianBatDau > 0 || totalThoiGianKetThuc > 0 || isThoiGianValid == false)
            {
                if (totalTenHocKy > 0)
                {
                    Errors.Add("tenHocKy", new string[] { "tenHocKy is duplicated!" });
                }

                if (totalThoiGianBatDau > 0)
                {
                    Errors.Add("thoiGianBatDau", new string[] { "thoiGianBatDau is duplicated!" });
                }

                if (totalThoiGianKetThuc > 0)
                {
                    Errors.Add("thoiGianKetThuc", new string[] { "thoiGianKetThuc is duplicated!" });
                }

                if (isThoiGianValid == false)
                {
                    Errors.Add("thoiGianKetThuc", new string[] { "thoiGianKetThuc must come before thoiGianBatDau!" });
                }

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

        public async Task<ICollection<HocKy>> CreateMultiple(ICollection<HocKyForCreateMultipleDto> danhSachHocKy)
        {
            ICollection<HocKy> temp = new List<HocKy>();

            for (int i = 0; i < danhSachHocKy.Count; i++)
            {
                var hocKy = danhSachHocKy.ElementAt(i);

                var newHocKy = new HocKy
                {
                    MaHocKy = hocKy.MaHocKy,
                    TenHocKy = hocKy.TenHocKy,
                    NamHoc = hocKy.NamHoc,
                    ThoiGianBatDau = hocKy.ThoiGianBatDau,
                    ThoiGianKetThuc = hocKy.ThoiGianKetThuc,
                    ThoiGianTao = hocKy.ThoiGianTao,
                    ThoiGianCapNhat = hocKy.ThoiGianCapNhat,
                    TrangThai = hocKy.TrangThai
                };

                temp.Add(newHocKy);

                await _context.DanhSachHocKy.AddAsync(newHocKy);
                await _context.SaveChangesAsync();
            }

            return temp;
        }
    }
}
