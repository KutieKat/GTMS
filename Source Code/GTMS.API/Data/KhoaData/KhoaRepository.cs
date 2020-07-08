using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTMS.API.Dtos;
using GTMS.API.Dtos.KhoaDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GTMS.API.Data.KhoaData
{
    public class KhoaRepository : IKhoaRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public KhoaRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<Khoa> Create(KhoaForCreateDto khoa)
        {
            var danhSachKhoa = await _context.DanhSachKhoa.OrderByDescending(x => x.MaKhoa).FirstOrDefaultAsync();
            var maKhoa = 0;

            if (danhSachKhoa == null)
            {
                maKhoa = 0;
            }
            else
            {
                maKhoa = danhSachKhoa.MaKhoa + 1;
            }

            var newKhoa = new Khoa
            {
                MaKhoa = maKhoa,
                TenKhoa = khoa.TenKhoa,
                TenVietTat = khoa.TenVietTat,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachKhoa.AddAsync(newKhoa);
            await _context.SaveChangesAsync();

            return newKhoa;
        }

        public async Task<Khoa> PermanentlyDeleteById(int id)
        {
            var khoaToDelete = await _context.DanhSachKhoa.FirstOrDefaultAsync(x => x.MaKhoa == id);

            _context.DanhSachKhoa.Remove(khoaToDelete);
            await _context.SaveChangesAsync();

            return khoaToDelete;
        }

        public async Task<PagedList<Khoa>> GetAll(KhoaParams userParams)
        {
            var result = _context.DanhSachKhoa.AsQueryable();
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
                result = result.Where(x => x.TenKhoa.ToLower().Contains(keyword.ToLower()) || x.TenVietTat.ToLower().Contains(keyword.ToLower()) || x.MaKhoa.ToString() == keyword);
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

                    case "TenKhoa":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenKhoa);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenKhoa);
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
            _totalPages = (int) Math.Ceiling((double) _totalItems / (double) userParams.PageSize);

            return await PagedList<Khoa>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Khoa> GetById(int id)
        {
            var result = await _context.DanhSachKhoa.FirstOrDefaultAsync(x => x.MaKhoa == id);

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

        public async Task<Khoa> UpdateById(int id, KhoaForUpdateDto khoa)
        {
            var oldRecord = await _context.DanhSachKhoa.AsNoTracking().FirstOrDefaultAsync(x => x.MaKhoa == id);

            var khoaToUpdate = new Khoa
            {
                MaKhoa = id,
                TenKhoa = khoa.TenKhoa,
                TenVietTat = khoa.TenVietTat,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = khoa.TrangThai
            };

            _context.DanhSachKhoa.Update(khoaToUpdate);
            await _context.SaveChangesAsync();

            return khoaToUpdate;
        }

        public async Task<Khoa> TemporarilyDeleteById(int id)
        {
            var khoaToTemporarilyDeleteById = await _context.DanhSachKhoa.FirstOrDefaultAsync(x => x.MaKhoa == id);

            khoaToTemporarilyDeleteById.TrangThai = -1;
            khoaToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachKhoa.Update(khoaToTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return khoaToTemporarilyDeleteById;
        }

        public async Task<Khoa> RestoreById(int id)
        {
            var khoaToRestoreById = await _context.DanhSachKhoa.FirstOrDefaultAsync(x => x.MaKhoa == id);

            khoaToRestoreById.TrangThai = 1;
            khoaToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachKhoa.Update(khoaToRestoreById);
            await _context.SaveChangesAsync();

            return khoaToRestoreById;
        }

        public Object GetStatusStatistics (KhoaParams userParams)
        {
            var result = _context.DanhSachKhoa.AsQueryable();
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
                result = result.Where(x => x.TenKhoa.ToLower().Contains(keyword.ToLower()) || x.TenVietTat.ToLower().Contains(keyword.ToLower()) || x.MaKhoa.ToString() == keyword);
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

                    case "TenKhoa":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenKhoa);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenKhoa);
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

        public ValidationResultDto ValidateBeforeCreate(KhoaForCreateDto khoa)
        {
            var totalTenKhoa = _context.DanhSachKhoa.Count(x => x.TenKhoa.ToLower() == khoa.TenKhoa.ToLower());
            var totalTenVietTat = _context.DanhSachKhoa.Count(x => x.TenVietTat.ToLower() == khoa.TenVietTat.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenKhoa >= 1 || totalTenVietTat >= 1)
            {
                if (totalTenKhoa >= 1)
                {
                    Errors.Add("tenKhoa", new string[] { "tenKhoa is duplicated!" });
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

        public ValidationResultDto ValidateBeforeCreateMultiple(ICollection<KhoaForCreateMultipleDto> danhSachKhoa)
        {
            var isValid = true;

            for (int i = 0; i < danhSachKhoa.Count; i++)
            {
                var khoa = danhSachKhoa.ElementAt(i);
                var khoaToString = danhSachKhoa.ElementAt(i).ToString();
                var count = _context.DanhSachKhoa.Count(x => x.MaKhoa == khoa.MaKhoa);
                var hasEnoughFields = khoaToString.Contains("MaKhoa") && khoaToString.Contains("TenKhoa") && khoaToString.Contains("TenVietTat");
                var isOkayToCreate = true;

                var totalTenKhoa = _context.DanhSachKhoa.Count(x => x.TenKhoa.ToLower() == khoa.TenKhoa.ToLower());
                var totalTenVietTat = _context.DanhSachKhoa.Count(x => x.TenVietTat.ToLower() == khoa.TenVietTat.ToLower());

                if (totalTenKhoa >= 1 || totalTenVietTat >= 1)
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

        public ValidationResultDto ValidateBeforeUpdate(int id, KhoaForUpdateDto khoa)
        {
            var totalTenKhoa = _context.DanhSachKhoa.Count(x => x.MaKhoa != id && x.TenKhoa.ToLower() == khoa.TenKhoa.ToLower());
            var totalTenVietTat = _context.DanhSachKhoa.Count(x => x.MaKhoa != id && x.TenVietTat.ToLower() == khoa.TenVietTat.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenKhoa > 0 || totalTenVietTat > 0)
            {
                if (totalTenKhoa > 0)
                {
                    Errors.Add("tenKhoa", new string[] { "tenKhoa is duplicated!" });
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

        public async Task<ICollection<Khoa>> CreateMultiple(ICollection<KhoaForCreateMultipleDto> danhSachKhoa)
        {
            ICollection<Khoa> temp = new List<Khoa>();

            for (int i = 0; i < danhSachKhoa.Count; i++)
            {
                var khoa = danhSachKhoa.ElementAt(i);

                var newKhoa = new Khoa
                {
                    MaKhoa = khoa.MaKhoa,
                    TenKhoa = khoa.TenKhoa,
                    TenVietTat = khoa.TenVietTat,
                    ThoiGianTao = khoa.ThoiGianTao,
                    ThoiGianCapNhat = khoa.ThoiGianCapNhat,
                    TrangThai = khoa.TrangThai
                };

                temp.Add(newKhoa);

                await _context.DanhSachKhoa.AddAsync(newKhoa);
                await _context.SaveChangesAsync();
            }

            return temp;
        }
    }
}
