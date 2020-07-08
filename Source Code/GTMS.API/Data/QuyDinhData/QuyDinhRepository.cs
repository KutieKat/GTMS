using GTMS.API.Dtos;
using GTMS.API.Dtos.QuyDinhDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Data.QuyDinhData
{
    public class QuyDinhRepository : IQuyDinhRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public QuyDinhRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<QuyDinh> Create(QuyDinhForCreateDto quyDinh)
        {
            var danhSachQuyDinh = await _context.DanhSachQuyDinh.OrderByDescending(x => x.MaQuyDinh).FirstOrDefaultAsync();
            var maQuyDinh = 0;

            if (danhSachQuyDinh == null)
            {
                maQuyDinh = 0;
            }
            else
            {
                maQuyDinh = danhSachQuyDinh.MaQuyDinh + 1;
            }

            var newQuyDinh = new QuyDinh();
            newQuyDinh.MaQuyDinh = maQuyDinh;
            newQuyDinh.TenQuyDinh = quyDinh.TenQuyDinh;
            newQuyDinh.ThoiGianBatDauHieuLuc = quyDinh.ThoiGianBatDauHieuLuc;
            newQuyDinh.SoSVTHToiThieu = quyDinh.SoSVTHToiThieu;
            newQuyDinh.SoSVTHToiDa = quyDinh.SoSVTHToiDa;
            newQuyDinh.SoGVHDToiThieu = quyDinh.SoGVHDToiThieu;
            newQuyDinh.SoGVHDToiDa = quyDinh.SoGVHDToiDa;
            newQuyDinh.SoGVPBToiThieu = quyDinh.SoGVPBToiThieu;
            newQuyDinh.SoGVPBToiDa = quyDinh.SoGVPBToiDa;
            newQuyDinh.SoTVHDToiThieu = quyDinh.SoTVHDToiThieu;
            newQuyDinh.SoTVHDToiDa = quyDinh.SoTVHDToiDa;
            newQuyDinh.SoCTHDToiThieu = quyDinh.SoCTHDToiThieu;
            newQuyDinh.SoCTHDToiDa = quyDinh.SoCTHDToiDa;
            newQuyDinh.SoTKHDToiThieu = quyDinh.SoTKHDToiThieu;
            newQuyDinh.SoTKHDToiDa = quyDinh.SoTKHDToiDa;
            newQuyDinh.SoUVHDToiThieu = quyDinh.SoUVHDToiThieu;
            newQuyDinh.SoUVHDToiDa = quyDinh.SoUVHDToiDa;
            newQuyDinh.SoChuSoThapPhan = quyDinh.SoChuSoThapPhan;
            newQuyDinh.DiemSoToiThieu = quyDinh.DiemSoToiThieu;
            newQuyDinh.DiemSoToiDa = quyDinh.DiemSoToiDa;
            newQuyDinh.HeSoGVHD = quyDinh.HeSoGVHD;
            newQuyDinh.HeSoGVPB = quyDinh.HeSoGVPB;
            newQuyDinh.HeSoTVHD = quyDinh.HeSoTVHD;
            newQuyDinh.ThoiGianTao = DateTime.Now;
            newQuyDinh.ThoiGianCapNhat = DateTime.Now;
            newQuyDinh.TrangThai = 1;                

            await _context.DanhSachQuyDinh.AddAsync(newQuyDinh);
            await _context.SaveChangesAsync();

            return newQuyDinh;
        }

        public async Task<ICollection<QuyDinh>> CreateMultiple(ICollection<QuyDinhForCreateMultipleDto> danhSachQuyDinh)
        {
            ICollection<QuyDinh> temp = new List<QuyDinh>();

            for (int i = 0; i < danhSachQuyDinh.Count; i++)
            {
                var quyDinh = danhSachQuyDinh.ElementAt(i);

                var newQuyDinh = new QuyDinh();
                newQuyDinh.MaQuyDinh = quyDinh.MaQuyDinh;
                newQuyDinh.TenQuyDinh = quyDinh.TenQuyDinh;
                newQuyDinh.ThoiGianBatDauHieuLuc = quyDinh.ThoiGianBatDauHieuLuc;
                newQuyDinh.SoSVTHToiThieu = quyDinh.SoSVTHToiThieu;
                newQuyDinh.SoSVTHToiDa = quyDinh.SoSVTHToiDa;
                newQuyDinh.SoGVHDToiThieu = quyDinh.SoGVHDToiThieu;
                newQuyDinh.SoGVHDToiDa = quyDinh.SoGVHDToiDa;
                newQuyDinh.SoGVPBToiThieu = quyDinh.SoGVPBToiThieu;
                newQuyDinh.SoGVPBToiDa = quyDinh.SoGVPBToiDa;
                newQuyDinh.SoTVHDToiThieu = quyDinh.SoTVHDToiThieu;
                newQuyDinh.SoTVHDToiDa = quyDinh.SoTVHDToiDa;
                newQuyDinh.SoCTHDToiThieu = quyDinh.SoCTHDToiThieu;
                newQuyDinh.SoCTHDToiDa = quyDinh.SoCTHDToiDa;
                newQuyDinh.SoTKHDToiThieu = quyDinh.SoTKHDToiThieu;
                newQuyDinh.SoTKHDToiDa = quyDinh.SoTKHDToiDa;
                newQuyDinh.SoUVHDToiThieu = quyDinh.SoUVHDToiThieu;
                newQuyDinh.SoUVHDToiDa = quyDinh.SoUVHDToiDa;
                newQuyDinh.SoChuSoThapPhan = quyDinh.SoChuSoThapPhan;
                newQuyDinh.DiemSoToiThieu = quyDinh.DiemSoToiThieu;
                newQuyDinh.DiemSoToiDa = quyDinh.DiemSoToiDa;
                newQuyDinh.HeSoGVHD = quyDinh.HeSoGVHD;
                newQuyDinh.HeSoGVPB = quyDinh.HeSoGVPB;
                newQuyDinh.HeSoTVHD = quyDinh.HeSoTVHD;
                newQuyDinh.ThoiGianTao = DateTime.Now;
                newQuyDinh.ThoiGianCapNhat = DateTime.Now;
                newQuyDinh.TrangThai = 1;

               

                temp.Add(newQuyDinh);

                await _context.DanhSachQuyDinh.AddAsync(newQuyDinh);
                await _context.SaveChangesAsync();
            }

            return temp;
        }

        public async Task<PagedList<QuyDinh>> GetAll(QuyDinhParams userParams)
        {
            var result = _context.DanhSachQuyDinh.AsQueryable();
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
                result = result.Where(x => x.TenQuyDinh.ToLower().Contains(keyword.ToLower()) || x.MaQuyDinh.ToString() == keyword);
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
                    case "MaQuyDinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaQuyDinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaQuyDinh);
                        }
                        break;

                    case "TenQuyDinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenQuyDinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenQuyDinh);
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

                    case "ThoiGianBatDauHieuLuc":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianBatDauHieuLuc);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianBatDauHieuLuc);
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

            return await PagedList<QuyDinh>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<QuyDinh> GetByDate(DateTime date)
        {
            var result = await _context.DanhSachQuyDinh.OrderByDescending(x => x.ThoiGianBatDauHieuLuc <= date).FirstOrDefaultAsync();

            return result;
        }

        public async Task<QuyDinh> GetById(int id)
        {
            var result = await _context.DanhSachQuyDinh.FirstOrDefaultAsync(x => x.MaQuyDinh == id);

            return result;
        }

        public Object GetStatusStatistics(QuyDinhParams userParams)
        {
            var result = _context.DanhSachQuyDinh.AsQueryable();
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
                result = result.Where(x => x.TenQuyDinh.ToLower().Contains(keyword.ToLower()) || x.MaQuyDinh.ToString() == keyword);
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
                    case "MaQuyDinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaQuyDinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaQuyDinh);
                        }
                        break;

                    case "TenQuyDinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenQuyDinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenQuyDinh);
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

                    case "ThoiGianBatDauHieuLuc":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianBatDauHieuLuc);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianBatDauHieuLuc);
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

        public int GetTotalItems()
        {
            return _totalItems;
        }

        public int GetTotalPages()
        {
            return _totalPages;
        }

        public Task<QuyDinh> PermanentlyDeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<QuyDinh> RestoreById(int id)
        {
            var quyDinhToRestoreById = await _context.DanhSachQuyDinh.FirstOrDefaultAsync(x => x.MaQuyDinh == id);

            quyDinhToRestoreById.TrangThai = 1;
            quyDinhToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachQuyDinh.Update(quyDinhToRestoreById);
            await _context.SaveChangesAsync();

            return quyDinhToRestoreById;
        }

        public async Task<QuyDinh> TemporarilyDeleteById(int id)
        {
            var quyDinhToTemporarilyDeleteById = await _context.DanhSachQuyDinh.FirstOrDefaultAsync(x => x.MaQuyDinh == id);

            quyDinhToTemporarilyDeleteById.TrangThai = -1;
            quyDinhToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachQuyDinh.Update(quyDinhToTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return quyDinhToTemporarilyDeleteById;
        }

        public async Task<QuyDinh> UpdateById(int id, QuyDinhForUpdateDto quyDinh)
        {
            var oldRecord = await _context.DanhSachQuyDinh.AsNoTracking().FirstOrDefaultAsync(x => x.MaQuyDinh == id);

            var quyDinhToUpdate = new QuyDinh();
            quyDinhToUpdate.MaQuyDinh = id;
            quyDinhToUpdate.TenQuyDinh = quyDinh.TenQuyDinh;
            quyDinhToUpdate.ThoiGianBatDauHieuLuc = quyDinh.ThoiGianBatDauHieuLuc;
            quyDinhToUpdate.SoSVTHToiThieu = quyDinh.SoSVTHToiThieu;
            quyDinhToUpdate.SoSVTHToiDa = quyDinh.SoSVTHToiDa;
            quyDinhToUpdate.SoGVHDToiThieu = quyDinh.SoGVHDToiThieu;
            quyDinhToUpdate.SoGVHDToiDa = quyDinh.SoGVHDToiDa;
            quyDinhToUpdate.SoGVPBToiThieu = quyDinh.SoGVPBToiThieu;
            quyDinhToUpdate.SoGVPBToiDa = quyDinh.SoGVPBToiDa;
            quyDinhToUpdate.SoTVHDToiThieu = quyDinh.SoTVHDToiThieu;
            quyDinhToUpdate.SoTVHDToiDa = quyDinh.SoTVHDToiDa;
            quyDinhToUpdate.SoCTHDToiThieu = quyDinh.SoCTHDToiThieu;
            quyDinhToUpdate.SoCTHDToiDa = quyDinh.SoCTHDToiDa;
            quyDinhToUpdate.SoTKHDToiThieu = quyDinh.SoTKHDToiThieu;
            quyDinhToUpdate.SoTKHDToiDa = quyDinh.SoTKHDToiDa;
            quyDinhToUpdate.SoUVHDToiThieu = quyDinh.SoUVHDToiThieu;
            quyDinhToUpdate.SoUVHDToiDa = quyDinh.SoUVHDToiDa;
            quyDinhToUpdate.SoChuSoThapPhan = quyDinh.SoChuSoThapPhan;
            quyDinhToUpdate.DiemSoToiThieu = quyDinh.DiemSoToiThieu;
            quyDinhToUpdate.DiemSoToiDa = quyDinh.DiemSoToiDa;
            quyDinhToUpdate.HeSoGVHD = quyDinh.HeSoGVHD;
            quyDinhToUpdate.HeSoGVPB = quyDinh.HeSoGVPB;
            quyDinhToUpdate.HeSoTVHD = quyDinh.HeSoTVHD;
            quyDinhToUpdate.ThoiGianTao = oldRecord.ThoiGianTao;
            quyDinhToUpdate.ThoiGianCapNhat = DateTime.Now;
            quyDinhToUpdate.TrangThai = 1;
                            

            _context.DanhSachQuyDinh.Update(quyDinhToUpdate);
            await _context.SaveChangesAsync();

            return quyDinhToUpdate;
        }

        public ValidationResultDto ValidateBeforeCreate(QuyDinhForCreateDto quyDinh)
        {
            var totalTenQuyDinh = _context.DanhSachQuyDinh.Count(x => x.TenQuyDinh.ToLower() == quyDinh.TenQuyDinh.ToLower());
            var totalThoiGianBatDauHieuLuc = _context.DanhSachQuyDinh.Count(x => x.ThoiGianBatDauHieuLuc == quyDinh.ThoiGianBatDauHieuLuc);
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenQuyDinh >= 1 || totalThoiGianBatDauHieuLuc >= 1)
            {
                if (totalTenQuyDinh >= 1)
                {
                    Errors.Add("tenQuyDinh", new string[] { "tenQuyDinh is duplicated!" });
                }

                if (totalThoiGianBatDauHieuLuc >= 1)
                {
                    Errors.Add("thoiGianBatDauHieuLuc", new string[] { "thoiGianBatDauHieuLuc is duplicated!" });
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

        public ValidationResultDto ValidateBeforeCreateMultiple(ICollection<QuyDinhForCreateMultipleDto> danhSachQuyDinh)
        {
            var isValid = true;

            for (int i = 0; i < danhSachQuyDinh.Count; i++)
            {
                var quyDinh = danhSachQuyDinh.ElementAt(i);
                var quyDinhToString = danhSachQuyDinh.ElementAt(i).ToString();
                var count = _context.DanhSachKhoa.Count(x => x.MaKhoa == quyDinh.MaQuyDinh);
                var hasEnoughFields = quyDinhToString.Contains("MaQuyDinh") && quyDinhToString.Contains("TenQuyDinh") && quyDinhToString.Contains("ThoiGianBatDauHieuLuc");
                var isOkayToCreate = true;

                var totalTenQuyDinh = _context.DanhSachQuyDinh.Count(x => x.TenQuyDinh.ToLower() == quyDinh.TenQuyDinh.ToLower());
                var totalThoiGianBatDauHieuLuc = _context.DanhSachQuyDinh.Count(x => x.ThoiGianBatDauHieuLuc == quyDinh.ThoiGianBatDauHieuLuc);

                if (totalTenQuyDinh >= 1 || totalThoiGianBatDauHieuLuc >= 1)
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

        public ValidationResultDto ValidateBeforeUpdate(int id, QuyDinhForUpdateDto quyDinh)
        {
            var totalTenQuyDinh = _context.DanhSachQuyDinh.Count(x => x.MaQuyDinh != id && x.TenQuyDinh.ToLower() == quyDinh.TenQuyDinh.ToLower());
            var totalThoiGianBatDauHieuLuc = _context.DanhSachQuyDinh.Count(x => x.MaQuyDinh != id && x.ThoiGianBatDauHieuLuc == quyDinh.ThoiGianBatDauHieuLuc);
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenQuyDinh > 0 || totalThoiGianBatDauHieuLuc > 0)
            {
                if (totalTenQuyDinh > 0)
                {
                    Errors.Add("tenQuyDinh", new string[] { "tenQuyDinh is duplicated!" });
                }

                if (totalThoiGianBatDauHieuLuc > 0)
                {
                    Errors.Add("thoiGianBatDauHieuLuc", new string[] { "thoiGianBatDauHieuLuc is duplicated!" });
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
    }
}
