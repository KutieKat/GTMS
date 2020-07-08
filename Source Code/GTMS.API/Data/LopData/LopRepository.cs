using GTMS.API.Dtos;
using GTMS.API.Dtos.LopDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.LopData
{
    public class LopRepository : ILopRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public LopRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<PagedList<Lop>> GetAll(LopParams userParams)
        {
            var result = _context.DanhSachLop.Include(x => x.Khoa)
                                             .ThenInclude(x => x.GiangVien)
                                             .Include(x => x.KhoaDaoTao).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var maKhoa = userParams.MaKhoa;
            var maKhoaDaoTao = userParams.MaKhoaDaoTao;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenLop.ToLower().Contains(keyword.ToLower()) ||
                x.TenVietTat.ToLower().Contains(keyword.ToLower())||
                x.HeDaoTao.ToLower().Contains(keyword.ToLower()) || 
                x.MaLop.ToString() == keyword);
            }
            
            if (maKhoa > -1)
            {
                result = result.Where(x => x.MaKhoa == maKhoa);
            }

            if (maKhoaDaoTao > -1)
            {
                result = result.Where(x => x.MaKhoaDaoTao == maKhoaDaoTao);
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
                    case "MaLop":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaLop);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaLop);
                        }
                        break;

                    case "TenLop":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenLop);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenLop);
                        }
                        break;

                    case "TenVietTat":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenVietTat);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenVietTat);
                        }
                        break;

                    case "MaKhoa":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaKhoa);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaKhoa);
                        }
                        break;

                    case "HeDaoTao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.HeDaoTao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.HeDaoTao);
                        }
                        break;

                    case "MaKhoaDaoTao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaKhoaDaoTao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaKhoaDaoTao);
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

            return await PagedList<Lop>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Lop> GetById(int id)
        {
            var result = await _context.DanhSachLop.Include(x => x.Khoa).Include(x => x.KhoaDaoTao).FirstOrDefaultAsync(x => x.MaLop == id);

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

        public async Task<Lop> Create(LopForCreateDto lop)
        {
            var danhSachLop = await _context.DanhSachLop.OrderByDescending(x => x.MaLop).FirstOrDefaultAsync();
            var maLop = 0;

            if (danhSachLop == null)
            {
                maLop = 0;
            }
            else
            {
                maLop = danhSachLop.MaLop + 1;
            }
            var newLop = new Lop
            {
                MaLop = maLop,
                TenLop = lop.TenLop,
                TenVietTat = lop.TenVietTat,
                MaKhoa = lop.MaKhoa,
                HeDaoTao = lop.HeDaoTao,
                MaKhoaDaoTao = lop.MaKhoaDaoTao,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachLop.AddAsync(newLop);
            await _context.SaveChangesAsync();

            return newLop;
        }

        public async Task<Lop> UpdateById(int id, LopForUpdateDto lop)
        {
            var oldRecord = await _context.DanhSachLop.AsNoTracking().FirstOrDefaultAsync(x => x.MaLop == id);
            var lopToUpdate = new Lop
            {
                MaLop = id,
                TenLop = lop.TenLop,
                TenVietTat = lop.TenVietTat,
                MaKhoa = lop.MaKhoa,
                HeDaoTao = lop.HeDaoTao,
                MaKhoaDaoTao = lop.MaKhoaDaoTao,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = lop.TrangThai
            };

            _context.DanhSachLop.Update(lopToUpdate);
            await _context.SaveChangesAsync();
            return lopToUpdate;
        }

        public async Task<Lop> PermanentlyDeleteById(int id)
        {
            var lopToDelete = await _context.DanhSachLop.FirstOrDefaultAsync(x => x.MaLop == id);

            _context.DanhSachLop.Remove(lopToDelete);
            await _context.SaveChangesAsync();
            return lopToDelete;
        }

        public async Task<Lop> TemporarilyDeleteById(int id)
        {
            var lopToTemporarilyDelete = await _context.DanhSachLop.FirstOrDefaultAsync(x => x.MaLop == id);

            lopToTemporarilyDelete.TrangThai = -1;
            lopToTemporarilyDelete.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachLop.Update(lopToTemporarilyDelete);
            await _context.SaveChangesAsync();

            return lopToTemporarilyDelete;
        }

        public async Task<Lop> RestoreById(int id)
        {
            var lopToRestoreById = await _context.DanhSachLop.FirstOrDefaultAsync(x => x.MaLop == id);

            lopToRestoreById.TrangThai = 1;
            lopToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachLop.Update(lopToRestoreById);
            await _context.SaveChangesAsync();

            return lopToRestoreById;
        }
        public Object GetStatusStatistics(LopParams userParams)
        {
            var result = _context.DanhSachLop.Include(x => x.Khoa)
                                             .ThenInclude(x => x.GiangVien)
                                             .Include(x => x.KhoaDaoTao).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var maKhoa = userParams.MaKhoa;
            var maKhoaDaoTao = userParams.MaKhoaDaoTao;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenLop.ToLower().Contains(keyword.ToLower()) ||
                x.TenVietTat.ToLower().Contains(keyword.ToLower()) ||
                x.HeDaoTao.ToLower().Contains(keyword.ToLower()) ||
                x.MaLop.ToString() == keyword);
            }

            if (maKhoa > -1)
            {
                result = result.Where(x => x.MaKhoa == maKhoa);
            }

            if (maKhoaDaoTao > -1)
            {
                result = result.Where(x => x.MaKhoaDaoTao == maKhoaDaoTao);
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
                    case "MaLop":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaLop);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaLop);
                        }
                        break;

                    case "TenLop":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenLop);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenLop);
                        }
                        break;

                    case "TenVietTat":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenVietTat);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenVietTat);
                        }
                        break;

                    case "MaKhoa":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaKhoa);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaKhoa);
                        }
                        break;

                    case "HeDaoTao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.HeDaoTao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.HeDaoTao);
                        }
                        break;

                    case "MaKhoaDaoTao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaKhoaDaoTao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaKhoaDaoTao);
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
        public ValidationResultDto ValidateBeforeCreate(LopForCreateDto lop)
        {
            var totalTenLop = _context.DanhSachLop.Count(x => x.TenLop.ToLower().Contains(lop.TenLop.ToLower()));
            var totalTenVietTat = _context.DanhSachLop.Count(x => x.TenVietTat.ToLower().Contains(lop.TenVietTat.ToLower()));
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenLop >= 1 || totalTenVietTat >= 1)
            {
                if (totalTenLop >= 1)
                {
                    Errors.Add("tenLop", new string[] { "tenLop is duplicated!" });
                }

                if (totalTenVietTat >= 1)
                {
                    Errors.Add("tenVietTat", new string[] { "tenVietTat is duplicated!" });
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

        public ValidationResultDto ValidateBeforeCreateMultiple(ICollection<LopForCreateMultipleDto> danhSachLop)
        {
            var isValid = true;

            for (int i = 0; i < danhSachLop.Count; i++)
            {
                var lop = danhSachLop.ElementAt(i);
                var lopToString = danhSachLop.ElementAt(i).ToString();
                var count = _context.DanhSachLop.Count(x => x.MaLop == lop.MaLop);
                var hasEnoughFields = lopToString.Contains("MaLop") && lopToString.Contains("TenLop") && lopToString.Contains("TenVietTat") && lopToString.Contains("MaKhoa") && lopToString.Contains("HeDaoTao") && lopToString.Contains("MaKhoaDaoTao");
                var isOkayToCreate = true;

                var totalTenLop = _context.DanhSachLop.Count(x => x.TenLop.ToLower().Contains(lop.TenLop.ToLower()));
                var totalTenVietTat = _context.DanhSachLop.Count(x => x.TenVietTat.ToLower().Contains(lop.TenVietTat.ToLower()));

                if (totalTenLop >= 1 || totalTenVietTat >= 1)
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

        public ValidationResultDto ValidateBeforeUpdate(int id, LopForUpdateDto lop)
        {
            var totalTenLop = _context.DanhSachLop.Count(x => x.MaLop != id && x.TenLop.ToLower().Contains(lop.TenLop.ToLower()));
            var totalTenVietTat = _context.DanhSachLop.Count(x => x.MaLop != id && x.TenVietTat.ToLower().Contains(lop.TenVietTat.ToLower()));
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenLop > 0 || totalTenVietTat > 0)
            {
                if (totalTenLop > 0)
                {
                    Errors.Add("tenLop", new string[] { "tenLop is duplicated!" });
                }

                if (totalTenVietTat > 0)
                {
                    Errors.Add("tenVietTat", new string[] { "tenVietTat is duplicated!" });
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

        public async Task<ICollection<Lop>> CreateMultiple(ICollection<LopForCreateMultipleDto> danhSachLop)
        {
            ICollection<Lop> temp = new List<Lop>();

            for (int i = 0; i < danhSachLop.Count; i++)
            {
                var lop = danhSachLop.ElementAt(i);

                var newLop = new Lop
                {
                    MaLop = lop.MaLop,
                    TenLop = lop.TenLop,
                    TenVietTat = lop.TenVietTat,
                    MaKhoa = lop.MaKhoa,
                    HeDaoTao = lop.HeDaoTao,
                    MaKhoaDaoTao = lop.MaKhoaDaoTao,
                    ThoiGianTao = lop.ThoiGianTao,
                    ThoiGianCapNhat = lop.ThoiGianCapNhat,
                    TrangThai = lop.TrangThai
                };

                temp.Add(newLop);

                await _context.DanhSachLop.AddAsync(newLop);
                await _context.SaveChangesAsync();
            }

            return temp;
        }

    }
}