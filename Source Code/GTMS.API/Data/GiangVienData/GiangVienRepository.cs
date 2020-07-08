using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTMS.API.Dtos;
using GTMS.API.Dtos.GiangVienDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GTMS.API.Data.GiangVienData
{
    public class GiangVienRepository : IGiangVienRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public GiangVienRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<PagedList<GiangVien>> GetAll(GiangVienParams userParams)
        {
            var result = _context.DanhSachGiangVien.Include(x => x.Khoa).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var ngaySinhBatDau = userParams.NgaySinhBatDau;
            var ngaySinhKetThuc = userParams.NgaySinhKetThuc;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.HoVaTen.ToLower().Contains(keyword.ToLower()) ||
                                           x.MaGiangVien.ToLower().Contains(keyword.ToLower()) ||
                                           x.QueQuan.ToLower().Contains(keyword.ToLower()) ||
                                           x.Email.ToLower().Contains(keyword.ToLower()) ||
                                           x.SoDienThoai.ToLower().Contains(keyword.ToLower()) ||
                                           x.DiaChi.ToLower().Contains(keyword.ToLower())||
                                           x.DonViCongTac.ToLower().Contains(keyword.ToLower())||
                                           x.HocHam.ToLower().Contains(keyword.ToLower())||
                                           x.HocVi.ToLower().Contains(keyword.ToLower())||
                                           x.GioiTinh.ToLower().Contains(keyword.ToLower()));
            }

            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
            }

            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
            }

            if (trangThai == -1 || trangThai ==1)
            {
                result = result.Where(x => x.TrangThai == trangThai);
            }

            if (ngaySinhBatDau.GetHashCode() != 0 && ngaySinhKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgaySinh >= ngaySinhBatDau && x.NgaySinh <= ngaySinhKetThuc);
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

                    case "HocVi":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.HocVi);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.HocVi);
                        }
                        break;

                    case "HocHam":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.HocHam);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.HocHam);
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
                    case "DonViCongTac":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DonViCongTac);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DonViCongTac);
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

            return await PagedList<GiangVien>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<GiangVien> GetById(string id)
        {
            var result = await _context.DanhSachGiangVien.Include(x => x.Khoa).FirstOrDefaultAsync(x => x.MaGiangVien == id);

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
            int count = _context.DanhSachGiangVien.Count() + 1;
            string tempId = count.ToString();
            string currentYear = DateTime.Now.ToString("yy");

            while (tempId.Length < 4)
            {
                tempId = "0" + tempId;
            }

            tempId = "GV" + currentYear + tempId;

            return tempId;
        }

        public async Task<GiangVien> Create(GiangVienForCreateDto giangVien)
        {
            var newGiangVien = new GiangVien();

            newGiangVien.MaGiangVien = GenerateId();
            newGiangVien.HoVaTen = giangVien.HoVaTen;
            newGiangVien.GioiTinh = giangVien.GioiTinh;
            newGiangVien.NgaySinh = giangVien.NgaySinh;
            newGiangVien.Email = giangVien.Email;
            newGiangVien.QueQuan = giangVien.QueQuan;
            newGiangVien.SoDienThoai = giangVien.SoDienThoai;
            newGiangVien.DiaChi = giangVien.DiaChi;
            newGiangVien.DonViCongTac = giangVien.DonViCongTac;
            newGiangVien.HocVi = giangVien.HocVi;
            newGiangVien.HocHam = giangVien.HocHam;
            newGiangVien.ThoiGianTao = DateTime.Now;
            newGiangVien.ThoiGianCapNhat = DateTime.Now;
            newGiangVien.TrangThai = 1;

            if (giangVien.MaKhoa.HasValue)
            {
                newGiangVien.MaKhoa = giangVien.MaKhoa;
            }

            await _context.DanhSachGiangVien.AddAsync(newGiangVien);
            await _context.SaveChangesAsync();

            return newGiangVien;
        }

        public async Task<GiangVien> UpdateById(string id, GiangVienForUpdateDto giangVien)
        {
            var oldRecord = await _context.DanhSachGiangVien.AsNoTracking().FirstOrDefaultAsync(x => x.MaGiangVien == id);
            var newGiangVien = new GiangVien();

            newGiangVien.MaGiangVien = oldRecord.MaGiangVien;
            newGiangVien.HoVaTen = giangVien.HoVaTen;
            newGiangVien.GioiTinh = giangVien.GioiTinh;
            newGiangVien.NgaySinh = giangVien.NgaySinh;
            newGiangVien.Email = giangVien.Email;
            newGiangVien.QueQuan = giangVien.QueQuan;
            newGiangVien.SoDienThoai = giangVien.SoDienThoai;
            newGiangVien.DiaChi = giangVien.DiaChi;
            newGiangVien.DonViCongTac = giangVien.DonViCongTac;
            newGiangVien.HocVi = giangVien.HocVi;
            newGiangVien.HocHam = giangVien.HocHam;
            newGiangVien.ThoiGianTao = oldRecord.ThoiGianTao;
            newGiangVien.ThoiGianCapNhat = DateTime.Now;
            newGiangVien.TrangThai = giangVien.TrangThai;

            if (giangVien.MaKhoa.HasValue)
            {
                newGiangVien.MaKhoa = giangVien.MaKhoa;
            }

            _context.DanhSachGiangVien.Update(newGiangVien);
            await _context.SaveChangesAsync();
            return newGiangVien;
        }

        public async Task<GiangVien> PermanentlyDeleteById(string id)
        {
            var giangVienToDelete = await _context.DanhSachGiangVien.FirstOrDefaultAsync(x => x.MaGiangVien == id);

            _context.DanhSachGiangVien.Remove(giangVienToDelete);
            await _context.SaveChangesAsync();

            return giangVienToDelete;
        }

        public async Task<GiangVien> TemporarilyDeleteById(string id)
        {
            var giangVienToTemporarilyDeleteById = await _context.DanhSachGiangVien.FirstOrDefaultAsync(x => x.MaGiangVien == id);

            giangVienToTemporarilyDeleteById.TrangThai = -1;
            giangVienToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachGiangVien.Update(giangVienToTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return giangVienToTemporarilyDeleteById;
        }

        public async Task<GiangVien> RestoreById(string id)
        {
            var giangVienToRestoreById = await _context.DanhSachGiangVien.FirstOrDefaultAsync(x => x.MaGiangVien == id);

            giangVienToRestoreById.TrangThai = 1;
            giangVienToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachGiangVien.Update(giangVienToRestoreById);
            await _context.SaveChangesAsync();

            return giangVienToRestoreById;
        }
        public Object GetStatusStatistics(GiangVienParams userParams)
        {
            var result = _context.DanhSachGiangVien.Include(x => x.Khoa).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var ngaySinhBatDau = userParams.NgaySinhBatDau;
            var ngaySinhKetThuc = userParams.NgaySinhKetThuc;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.HoVaTen.ToLower().Contains(keyword.ToLower()) ||
                                           x.MaGiangVien.ToLower().Contains(keyword.ToLower()) ||
                                           x.QueQuan.ToLower().Contains(keyword.ToLower()) ||
                                           x.Email.ToLower().Contains(keyword.ToLower()) ||
                                           x.SoDienThoai.ToLower().Contains(keyword.ToLower()) ||
                                           x.DiaChi.ToLower().Contains(keyword.ToLower()) ||
                                           x.DonViCongTac.ToLower().Contains(keyword.ToLower()) ||
                                           x.HocHam.ToLower().Contains(keyword.ToLower()) ||
                                           x.HocVi.ToLower().Contains(keyword.ToLower()) ||
                                           x.GioiTinh.ToLower().Contains(keyword.ToLower()));
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

            if (ngaySinhBatDau.GetHashCode() != 0 && ngaySinhKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgaySinh >= ngaySinhBatDau && x.NgaySinh <= ngaySinhKetThuc);
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

                    case "HocVi":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.HocVi);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.HocVi);
                        }
                        break;

                    case "HocHam":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.HocHam);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.HocHam);
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
                    case "DonViCongTac":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DonViCongTac);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DonViCongTac);
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

        public ValidationResultDto ValidateBeforeCreate(GiangVienForCreateDto giangVien)
        {
            var totalEmail = _context.DanhSachGiangVien.Count(x => x.Email.ToLower() == giangVien.Email.ToLower());
            var totalSoDienThoai = _context.DanhSachGiangVien.Count(x => x.SoDienThoai.ToLower() == giangVien.SoDienThoai.ToLower());
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

        public ValidationResultDto ValidateBeforeCreateMultiple(ICollection<GiangVienForCreateMultipleDto> danhSachGiangVien)
        {
            var isValid = true;

            for (int i = 0; i < danhSachGiangVien.Count; i++)
            {
                var giangVien = danhSachGiangVien.ElementAt(i);
                var giangVienToString = danhSachGiangVien.ElementAt(i).ToString();
                var count = _context.DanhSachGiangVien.Count(x => x.MaGiangVien == giangVien.MaGiangVien);
                var hasEnoughFields = giangVienToString.Contains("MaGiangVien") && giangVienToString.Contains("HoVaTen") && giangVienToString.Contains("MaKhoa") && giangVienToString.Contains("GioiTinh") && giangVienToString.Contains("NgaySinh") && giangVienToString.Contains("Email") && giangVienToString.Contains("SoDienThoai") && giangVienToString.Contains("QueQuan") && giangVienToString.Contains("DiaChi") && giangVienToString.Contains("DonViCongTac") && giangVienToString.Contains("HocVi") && giangVienToString.Contains("HocHam");
                var isOkayToCreate = true;

                var totalEmail = _context.DanhSachGiangVien.Count(x => x.Email.ToLower() == giangVien.Email.ToLower());
                var totalSoDienThoai = _context.DanhSachGiangVien.Count(x => x.SoDienThoai.ToLower() == giangVien.SoDienThoai.ToLower());

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

        public ValidationResultDto ValidateBeforeUpdate(string id, GiangVienForUpdateDto giangVien)
        {
            var totalEmail = _context.DanhSachGiangVien.Count(x => x.MaGiangVien != id && x.Email.ToLower() == giangVien.Email.ToLower());
            var totalSoDienThoai = _context.DanhSachGiangVien.Count(x => x.MaGiangVien != id && x.SoDienThoai.ToLower() == giangVien.SoDienThoai.ToLower());
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

        public async Task<ICollection<GiangVien>> CreateMultiple(ICollection<GiangVienForCreateMultipleDto> danhSachGiangVien)
        {
            ICollection<GiangVien> temp = new List<GiangVien>();

            for (int i = 0; i < danhSachGiangVien.Count; i++)
            {
                var giangVien = danhSachGiangVien.ElementAt(i);
                var newGiangVien = new GiangVien();

                newGiangVien.MaGiangVien = giangVien.MaGiangVien;
                newGiangVien.HoVaTen = giangVien.HoVaTen;
                newGiangVien.GioiTinh = giangVien.GioiTinh;
                newGiangVien.NgaySinh = giangVien.NgaySinh;
                newGiangVien.Email = giangVien.Email;
                newGiangVien.QueQuan = giangVien.QueQuan;
                newGiangVien.SoDienThoai = giangVien.SoDienThoai;
                newGiangVien.DiaChi = giangVien.DiaChi;
                newGiangVien.DonViCongTac = giangVien.DonViCongTac;
                newGiangVien.HocVi = giangVien.HocVi;
                newGiangVien.HocHam = giangVien.HocHam;
                newGiangVien.ThoiGianTao = giangVien.ThoiGianTao;
                newGiangVien.ThoiGianCapNhat = giangVien.ThoiGianCapNhat;
                newGiangVien.TrangThai = giangVien.TrangThai;

                if (giangVien.MaKhoa.HasValue)
                {
                    newGiangVien.MaKhoa = giangVien.MaKhoa;
                }

                temp.Add(newGiangVien);

                await _context.DanhSachGiangVien.AddAsync(newGiangVien);
                await _context.SaveChangesAsync();
            }

            return temp;
        }
    }
}
