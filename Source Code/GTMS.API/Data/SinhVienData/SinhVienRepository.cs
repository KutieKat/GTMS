using GTMS.API.Dtos;
using GTMS.API.Dtos.SinhVienDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.SinhVienData
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;
        public SinhVienRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }       

        public async Task<PagedList<SinhVien>> GetAll(SinhVienParams userParams)
        {
            var result = _context.DanhSachSinhVien.Include(x => x.Lop).Include(x => x.Lop.KhoaDaoTao).Include(x => x.Lop.Khoa).Include(x => x.DoAn).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var maLop = userParams.MaLop;
            var ngaySinhBatDau = userParams.NgaySinhBatDau;
            var ngaySinhKetThuc = userParams.NgaySinhKetThuc;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.HoVaTen.ToLower().Contains(keyword.ToLower()) ||
                                           x.Email.ToLower().Contains(keyword.ToLower()) ||
                                           x.QueQuan.ToLower().Contains(keyword.ToLower()) ||
                                           x.DiaChi.ToLower().Contains(keyword.ToLower()) ||
                                           x.SoDienThoai.ToLower().Contains(keyword.ToLower()) ||
                                           x.GioiTinh.ToLower().Contains(keyword.ToLower())||
                                           x.MaSinhVien.ToLower().Contains(keyword.ToLower()));
            }

            if (maLop > -1)
            {
                result = result.Where(x => x.MaLop == maLop);
            }

            if (ngaySinhBatDau.GetHashCode() != 0 && ngaySinhKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgaySinh >= ngaySinhBatDau && x.NgaySinh <= ngaySinhKetThuc);
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
                    case "MaSinhVien":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaSinhVien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaSinhVien);
                        }
                        break;

                    case "HoVaTen":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.HoVaTen);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.HoVaTen);
                        }
                        break;

                    case "GioiTinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.GioiTinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.GioiTinh);
                        }
                        break;

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

                    case "NgaySinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgaySinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgaySinh);
                        }
                        break;

                    case "Email":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.Email);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.Email);
                        }
                        break;

                    case "QueQuan":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.QueQuan);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.QueQuan);
                        }
                        break;

                    case "DiaChi":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DiaChi);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DiaChi);
                        }
                        break;

                    case "SoDienThoai":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.SoDienThoai);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.SoDienThoai);
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

            return await PagedList<SinhVien>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<SinhVien> GetById(string id)
        {
            var result = await _context.DanhSachSinhVien.Include(x => x.Lop).FirstOrDefaultAsync(x => x.MaSinhVien == id);

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

        private string GenerateId()
        {
            int count = _context.DanhSachSinhVien.Count() + 1;
            string tempId = count.ToString();
            string currentYear = DateTime.Now.ToString("yy");

            while (tempId.Length < 4)
            {
                tempId = "0" + tempId;
            }

            tempId = "SV" + currentYear + tempId;

            return tempId;
        }

        public async Task<SinhVien> Create(SinhVienForCreateDto sinhVien)
        {
            var newSinhVien = new SinhVien
            {
                MaSinhVien = GenerateId(),
                HoVaTen = sinhVien.HoVaTen,
                MaLop = sinhVien.MaLop,
                GioiTinh = sinhVien.GioiTinh,
                NgaySinh = sinhVien.NgaySinh,
                Email = sinhVien.Email,
                QueQuan = sinhVien.QueQuan,
                DiaChi = sinhVien.DiaChi,
                SoDienThoai = sinhVien.SoDienThoai,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachSinhVien.AddAsync(newSinhVien);
            await _context.SaveChangesAsync();

            return newSinhVien;
        }

        public async Task<SinhVien> UpdateById(string id, SinhVienForUpdateDto sinhVien)
        {
            var oldRecord = await _context.DanhSachSinhVien.AsNoTracking().FirstOrDefaultAsync(x => x.MaSinhVien == id);
            var sinhVienToUpdate = new SinhVien
            {
                MaSinhVien = id,
                HoVaTen = sinhVien.HoVaTen,
                MaLop = sinhVien.MaLop,
                GioiTinh = sinhVien.GioiTinh,
                NgaySinh = sinhVien.NgaySinh,
                Email = sinhVien.Email,
                QueQuan = sinhVien.QueQuan,
                DiaChi = sinhVien.DiaChi,
                SoDienThoai = sinhVien.SoDienThoai,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = sinhVien.TrangThai,
                MaDoAn = oldRecord.MaDoAn
            };

            _context.DanhSachSinhVien.Update(sinhVienToUpdate);
            await _context.SaveChangesAsync();
            return sinhVienToUpdate;
        }

        public async Task<SinhVien> PermanentlyDeleteById(string id)
        {
            var sinhVienToDelete = await _context.DanhSachSinhVien.FirstOrDefaultAsync(x => x.MaSinhVien == id);

            _context.DanhSachSinhVien.Remove(sinhVienToDelete);
            await _context.SaveChangesAsync();
            return sinhVienToDelete;
        }

        public async Task<SinhVien> TemporarilyDeleteById(string id)
        {
            var sinhVienToTemporarilyDelete = await _context.DanhSachSinhVien.FirstOrDefaultAsync(x => x.MaSinhVien == id);

            sinhVienToTemporarilyDelete.TrangThai = -1;
            sinhVienToTemporarilyDelete.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachSinhVien.Update(sinhVienToTemporarilyDelete);
            await _context.SaveChangesAsync();

            return sinhVienToTemporarilyDelete;
        }

        public async Task<SinhVien> RestoreById(string id)
        {
            var sinhVienToRestoreById = await _context.DanhSachSinhVien.FirstOrDefaultAsync(x => x.MaSinhVien == id);

            sinhVienToRestoreById.TrangThai = 1;
            sinhVienToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachSinhVien.Update(sinhVienToRestoreById);
            await _context.SaveChangesAsync();

            return sinhVienToRestoreById;
        }
        public Object GetStatusStatistics(SinhVienParams userParams)
        {
            var result = _context.DanhSachSinhVien.Include(x => x.Lop).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var maLop = userParams.MaLop;
            var ngaySinhBatDau = userParams.NgaySinhBatDau;
            var ngaySinhKetThuc = userParams.NgaySinhKetThuc;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.HoVaTen.ToLower().Contains(keyword.ToLower()) ||
                                           x.Email.ToLower().Contains(keyword.ToLower()) ||
                                           x.QueQuan.ToLower().Contains(keyword.ToLower()) ||
                                           x.DiaChi.ToLower().Contains(keyword.ToLower()) ||
                                           x.SoDienThoai.ToLower().Contains(keyword.ToLower()) ||
                                           x.GioiTinh.ToLower().Contains(keyword.ToLower()) ||
                                           x.MaSinhVien.ToLower().Contains(keyword.ToLower()));
            }

            if (maLop > -1)
            {
                result = result.Where(x => x.MaLop == maLop);
            }

            if (ngaySinhBatDau.GetHashCode() != 0 && ngaySinhKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgaySinh >= ngaySinhBatDau && x.NgaySinh <= ngaySinhKetThuc);
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
                    case "MaSinhVien":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaSinhVien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaSinhVien);
                        }
                        break;

                    case "HoVaTen":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.HoVaTen);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.HoVaTen);
                        }
                        break;

                    case "GioiTinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.GioiTinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.GioiTinh);
                        }
                        break;

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

                    case "NgaySinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgaySinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgaySinh);
                        }
                        break;

                    case "Email":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.Email);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.Email);
                        }
                        break;

                    case "QueQuan":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.QueQuan);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.QueQuan);
                        }
                        break;

                    case "DiaChi":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DiaChi);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DiaChi);
                        }
                        break;

                    case "SoDienThoai":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.SoDienThoai);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.SoDienThoai);
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

        public ValidationResultDto ValidateBeforeCreate(SinhVienForCreateDto sinhVien)
        {
            var totalEmail = _context.DanhSachSinhVien.Count(x => x.Email.ToLower() == sinhVien.Email.ToLower());
            var totalSoDienThoai = _context.DanhSachSinhVien.Count(x => x.SoDienThoai.ToLower() == sinhVien.SoDienThoai.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalEmail >= 1 || totalSoDienThoai >= 1)
            {
                if (totalEmail >= 1)
                {
                    Errors.Add("email", new string[] { "email is duplicated!" });
                }

                if (totalSoDienThoai >= 1)
                {
                    Errors.Add("soDienThoai", new string[] { "soDienThoai is duplicated!" });
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

        public ValidationResultDto ValidateBeforeCreateMultiple(ICollection<SinhVienForCreateMultipleDto> danhSachSinhVien)
        {
            var isValid = true;

            for (int i = 0; i < danhSachSinhVien.Count; i++)
            {
                var sinhVien = danhSachSinhVien.ElementAt(i);
                var sinhVienToString = danhSachSinhVien.ElementAt(i).ToString();
                var count = _context.DanhSachSinhVien.Count(x => x.MaSinhVien == sinhVien.MaSinhVien);
                var hasEnoughFields = sinhVienToString.Contains("MaSinhVien") && sinhVienToString.Contains("HoVaTen") && sinhVienToString.Contains("MaLop") && sinhVienToString.Contains("GioiTinh") && sinhVienToString.Contains("NgaySinh") && sinhVienToString.Contains("Email") && sinhVienToString.Contains("QueQuan") && sinhVienToString.Contains("DiaChi") && sinhVienToString.Contains("SoDienThoai") && sinhVienToString.Contains("MaDoAn");
                var isOkayToCreate = true;

                var totalEmail = _context.DanhSachSinhVien.Count(x => x.Email.ToLower() == sinhVien.Email.ToLower());
                var totalSoDienThoai = _context.DanhSachSinhVien.Count(x => x.SoDienThoai.ToLower() == sinhVien.SoDienThoai.ToLower());

                if (totalEmail >= 1 || totalSoDienThoai >= 1)
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

        public ValidationResultDto ValidateBeforeUpdate(string id, SinhVienForUpdateDto sinhVien)
        {
            var totalEmail = _context.DanhSachSinhVien.Count(x => x.MaSinhVien != id && x.Email.ToLower() == sinhVien.Email.ToLower());
            var totalSoDienThoai = _context.DanhSachSinhVien.Count(x => x.MaSinhVien != id && x.SoDienThoai.ToLower() == sinhVien.SoDienThoai.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalEmail > 0 || totalSoDienThoai > 0)
            {
                if (totalEmail > 0)
                {
                    Errors.Add("email", new string[] { "email is duplicated!" });
                }

                if (totalSoDienThoai > 0)
                {
                    Errors.Add("soDienThoai", new string[] { "soDienThoai is duplicated!" });
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

        public async Task<ICollection<SinhVien>> CreateMultiple(ICollection<SinhVienForCreateMultipleDto> danhSachSinhVien)
        {
            ICollection<SinhVien> temp = new List<SinhVien>();

            for (int i = 0; i < danhSachSinhVien.Count; i++)
            {
                var sinhVien = danhSachSinhVien.ElementAt(i);
                var newSinhVien = new SinhVien();

                newSinhVien.MaSinhVien = sinhVien.MaSinhVien;
                newSinhVien.HoVaTen = sinhVien.HoVaTen;
                newSinhVien.MaLop = sinhVien.MaLop;
                newSinhVien.GioiTinh = sinhVien.GioiTinh;
                newSinhVien.NgaySinh = sinhVien.NgaySinh;
                newSinhVien.Email = sinhVien.Email;
                newSinhVien.QueQuan = sinhVien.QueQuan;
                newSinhVien.DiaChi = sinhVien.DiaChi;
                newSinhVien.SoDienThoai = sinhVien.SoDienThoai;
                newSinhVien.ThoiGianTao = sinhVien.ThoiGianTao;
                newSinhVien.ThoiGianCapNhat = sinhVien.ThoiGianCapNhat;
                newSinhVien.TrangThai = sinhVien.TrangThai;

                if (sinhVien.MaDoAn.HasValue)
                {
                    newSinhVien.MaDoAn = sinhVien.MaDoAn;
                }

                temp.Add(newSinhVien);

                await _context.DanhSachSinhVien.AddAsync(newSinhVien);
                await _context.SaveChangesAsync();
            }

            return temp;
        }
    }
}
