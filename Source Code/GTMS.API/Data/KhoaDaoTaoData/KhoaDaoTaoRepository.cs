using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTMS.API.Dtos;
using GTMS.API.Dtos.KhoaDaoTaoDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GTMS.API.Data.KhoaDaoTaoData
{
    public class KhoaDaoTaoRepository : IKhoaDaoTaoRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;
        public KhoaDaoTaoRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<KhoaDaoTao> Create(KhoaDaoTaoForCreateDto khoaDaoTao)
        {
            var danhSachKhoaDaoTao = await _context.DanhSachKhoaDaoTao.OrderByDescending(x => x.MaKhoaDaoTao).FirstOrDefaultAsync();
            var maKhoaDaoTao = 0;

            if (danhSachKhoaDaoTao == null)
            {
                maKhoaDaoTao = 0;
            }
            else
            {
                maKhoaDaoTao = danhSachKhoaDaoTao.MaKhoaDaoTao + 1;
            }
            var newKhoaDaoTao = new KhoaDaoTao
            {
                MaKhoaDaoTao = maKhoaDaoTao,
                TenKhoaDaoTao = khoaDaoTao.TenKhoaDaoTao,
                TenVietTat = khoaDaoTao.TenVietTat,
                ThoiGianBatDau = khoaDaoTao.ThoiGianBatDau,
                ThoiGianKetThuc = khoaDaoTao.ThoiGianKetThuc,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachKhoaDaoTao.AddAsync(newKhoaDaoTao);
            await _context.SaveChangesAsync();

            return newKhoaDaoTao;
        }

        public async Task<KhoaDaoTao> PermanentlyDeleteById(int id)
        {
            var khoaDaoTaoToDelete = await _context.DanhSachKhoaDaoTao.FirstOrDefaultAsync(x => x.MaKhoaDaoTao == id);

            _context.DanhSachKhoaDaoTao.Remove(khoaDaoTaoToDelete);
            await _context.SaveChangesAsync();

            return khoaDaoTaoToDelete;
        }

        public async Task<PagedList<KhoaDaoTao>> GetAll(KhoaDaoTaoParams userParams)
        {
            var result = _context.DanhSachKhoaDaoTao.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianBatDau = userParams.ThoiGianBatDau;
            var thoiGianKetThuc = userParams.ThoiGianKetThuc;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenKhoaDaoTao.ToLower().Contains(keyword.ToLower()) || x.TenVietTat.ToLower().Contains(keyword.ToLower()) || x.MaKhoaDaoTao.ToString() == keyword);
            }

            if (thoiGianBatDau.GetHashCode() != 0 && thoiGianKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianBatDau >= thoiGianBatDau && x.ThoiGianKetThuc <= thoiGianKetThuc);
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

                    case "TenKhoaDaoTao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenKhoaDaoTao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenKhoaDaoTao);
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

            return await PagedList<KhoaDaoTao>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<KhoaDaoTao> GetById(int id)
        {
            var result = await _context.DanhSachKhoaDaoTao.FirstOrDefaultAsync(x => x.MaKhoaDaoTao == id);

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

        public async Task<KhoaDaoTao> UpdateById(int id, KhoaDaoTaoForUpdateDto khoaDaoTao)
        {
            var oldRecord = await _context.DanhSachKhoaDaoTao.AsNoTracking().FirstOrDefaultAsync(x => x.MaKhoaDaoTao == id);
            var khoaDaoTaoToUpDate = new KhoaDaoTao
            {
                MaKhoaDaoTao = id,
                TenKhoaDaoTao = khoaDaoTao.TenKhoaDaoTao,
                TenVietTat = khoaDaoTao.TenVietTat,
                ThoiGianBatDau = khoaDaoTao.ThoiGianBatDau,
                ThoiGianKetThuc = khoaDaoTao.ThoiGianKetThuc,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = khoaDaoTao.TrangThai
            };

            _context.DanhSachKhoaDaoTao.Update(khoaDaoTaoToUpDate);
            await _context.SaveChangesAsync();

            return khoaDaoTaoToUpDate;
        }

        public async Task<KhoaDaoTao> TemporarilyDeleteById(int id)
        {
            var khoaDaoTaoToTemporarilyDeleteById = await _context.DanhSachKhoaDaoTao.FirstOrDefaultAsync(x => x.MaKhoaDaoTao == id);

            khoaDaoTaoToTemporarilyDeleteById.TrangThai = -1;
            khoaDaoTaoToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachKhoaDaoTao.Update(khoaDaoTaoToTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return khoaDaoTaoToTemporarilyDeleteById;
        }

        public async Task<KhoaDaoTao> RestoreById(int id)
        {
            var khoaDaoTaoToRestoreById = await _context.DanhSachKhoaDaoTao.FirstOrDefaultAsync(x => x.MaKhoaDaoTao == id);

            khoaDaoTaoToRestoreById.TrangThai = 1;
            khoaDaoTaoToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachKhoaDaoTao.Update(khoaDaoTaoToRestoreById);
            await _context.SaveChangesAsync();

            return khoaDaoTaoToRestoreById;
        }

        public Object GetStatusStatistics(KhoaDaoTaoParams userParams)
        {
            var result = _context.DanhSachKhoaDaoTao.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianBatDau = userParams.ThoiGianBatDau;
            var thoiGianKetThuc = userParams.ThoiGianKetThuc;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenKhoaDaoTao.ToLower().Contains(keyword.ToLower()) || x.TenVietTat.ToLower().Contains(keyword.ToLower()) || x.MaKhoaDaoTao.ToString() == keyword);
            }

            if (thoiGianBatDau.GetHashCode() != 0 && thoiGianKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianBatDau >= thoiGianBatDau && x.ThoiGianKetThuc <= thoiGianKetThuc);
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

                    case "TenKhoaDaoTao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenKhoaDaoTao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenKhoaDaoTao);
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
        public ValidationResultDto ValidateBeforeCreate(KhoaDaoTaoForCreateDto khoaDaoTao)
        {
            var totalTenKhoaDaoTao = _context.DanhSachKhoaDaoTao.Count(x => x.TenKhoaDaoTao.ToLower().Contains(khoaDaoTao.TenKhoaDaoTao.ToLower()));
            var totalTenVietTat = _context.DanhSachKhoaDaoTao.Count(x => x.TenVietTat.ToLower().Contains(khoaDaoTao.TenVietTat.ToLower()));
            var totalThoiGianBatDau = _context.DanhSachKhoaDaoTao.Count(x => x.ThoiGianBatDau == khoaDaoTao.ThoiGianBatDau);
            var totalThoiGianKetThuc = _context.DanhSachKhoaDaoTao.Count(x => x.ThoiGianKetThuc == khoaDaoTao.ThoiGianKetThuc);
            var isThoiGianValid = khoaDaoTao.ThoiGianKetThuc >= khoaDaoTao.ThoiGianBatDau;
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenKhoaDaoTao >= 1 || totalTenVietTat >= 1 || totalThoiGianBatDau >= 1 || totalThoiGianKetThuc >= 1 || isThoiGianValid == false)
            {
                if (totalTenKhoaDaoTao >= 1)
                {
                    Errors.Add("tenKhoaDaoTao", new string[] { "tenKhoaDaoTao is duplicated!" });
                }

                if (totalTenVietTat >= 1)
                {
                    Errors.Add("tenVietTat", new string[] { "tenVietTat is duplicated!" });
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

        public ValidationResultDto ValidateBeforeCreateMultiple(ICollection<KhoaDaoTaoForCreateMultipleDto> danhSachKhoaDaoTao)
        {
            var isValid = true;

            for (int i = 0; i < danhSachKhoaDaoTao.Count; i++)
            {
                var khoaDaoTao = danhSachKhoaDaoTao.ElementAt(i);
                var khoaDaoTaoToString = danhSachKhoaDaoTao.ElementAt(i).ToString();
                var count = _context.DanhSachKhoaDaoTao.Count(x => x.MaKhoaDaoTao == khoaDaoTao.MaKhoaDaoTao);
                var hasEnoughFields = khoaDaoTaoToString.Contains("MaKhoaDaoTao") && khoaDaoTaoToString.Contains("TenKhoaDaoTao") && khoaDaoTaoToString.Contains("TenVietTat") && khoaDaoTaoToString.Contains("ThoiGianBatDau") && khoaDaoTaoToString.Contains("ThoiGianKetThuc");
                var isOkayToCreate = true;

                var totalTenKhoaDaoTao= _context.DanhSachKhoaDaoTao.Count(x => x.TenKhoaDaoTao.ToLower().Contains(khoaDaoTao.TenKhoaDaoTao.ToLower()));
                var totalTenVietTat = _context.DanhSachKhoaDaoTao.Count(x => x.TenVietTat.ToLower().Contains(khoaDaoTao.TenVietTat.ToLower()));
                var totalThoiGianBatDau = _context.DanhSachKhoaDaoTao.Count(x => x.ThoiGianBatDau == khoaDaoTao.ThoiGianBatDau);
                var totalThoiGianKetThuc = _context.DanhSachKhoaDaoTao.Count(x => x.ThoiGianKetThuc == khoaDaoTao.ThoiGianKetThuc);
                var isThoiGianValid = khoaDaoTao.ThoiGianKetThuc >= khoaDaoTao.ThoiGianBatDau;

                if (totalTenKhoaDaoTao >= 1 || totalTenVietTat >= 1 || totalThoiGianBatDau >= 1 || totalThoiGianKetThuc >= 1 || isThoiGianValid == false)
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

        public ValidationResultDto ValidateBeforeUpdate(int id, KhoaDaoTaoForUpdateDto khoaDaoTao)
        {
            var totalTenKhoaDaoTao = _context.DanhSachKhoaDaoTao.Count(x => x.MaKhoaDaoTao != id && x.TenKhoaDaoTao.ToLower().Contains(khoaDaoTao.TenKhoaDaoTao.ToLower()));
            var totalTenVietTat = _context.DanhSachKhoaDaoTao.Count(x => x.MaKhoaDaoTao != id && x.TenVietTat.ToLower().Contains(khoaDaoTao.TenVietTat.ToLower()));
            var totalThoiGianBatDau = _context.DanhSachKhoaDaoTao.Count(x => x.MaKhoaDaoTao != id && x.ThoiGianBatDau == khoaDaoTao.ThoiGianBatDau);
            var totalThoiGianKetThuc = _context.DanhSachKhoaDaoTao.Count(x => x.MaKhoaDaoTao != id && x.ThoiGianKetThuc == khoaDaoTao.ThoiGianKetThuc);
            var isThoiGianValid = khoaDaoTao.ThoiGianKetThuc >= khoaDaoTao.ThoiGianBatDau;
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenKhoaDaoTao > 0 || totalTenVietTat > 0 || totalThoiGianBatDau > 0 || totalThoiGianKetThuc > 0 || isThoiGianValid == false)
            {
                if (totalTenKhoaDaoTao > 0)
                {
                    Errors.Add("tenKhoaDaoTao", new string[] { "tenKhoaDaoTao is duplicated!" });
                }

                if (totalTenVietTat > 0)
                {
                    Errors.Add("tenVietTat", new string[] { "tenVietTat is duplicated!" });
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

        public async Task<ICollection<KhoaDaoTao>> CreateMultiple(ICollection<KhoaDaoTaoForCreateMultipleDto> danhSachKhoaDaoTao)
        {
            ICollection<KhoaDaoTao> temp = new List<KhoaDaoTao>();

            for (int i = 0; i < danhSachKhoaDaoTao.Count; i++)
            {
                var khoaDaoTao = danhSachKhoaDaoTao.ElementAt(i);

                var newKhoaDaoTao = new KhoaDaoTao
                {
                    MaKhoaDaoTao = khoaDaoTao.MaKhoaDaoTao,
                    TenKhoaDaoTao = khoaDaoTao.TenKhoaDaoTao,
                    TenVietTat = khoaDaoTao.TenVietTat,
                    ThoiGianBatDau = khoaDaoTao.ThoiGianBatDau,
                    ThoiGianKetThuc = khoaDaoTao.ThoiGianKetThuc,
                    ThoiGianTao = khoaDaoTao.ThoiGianTao,
                    ThoiGianCapNhat = khoaDaoTao.ThoiGianCapNhat,
                    TrangThai = khoaDaoTao.TrangThai
                };

                temp.Add(newKhoaDaoTao);

                await _context.DanhSachKhoaDaoTao.AddAsync(newKhoaDaoTao);
                await _context.SaveChangesAsync();
            }

            return temp;
        }
    }
}
