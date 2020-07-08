using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTMS.API.Dtos.DoAnDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using GTMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GTMS.API.Data.DoAnData
{
    public class DoAnRepository : IDoAnRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;
        public DoAnRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<DoAn> Create(DoAnForCreateDto doAn)
        {
            var danhSachDoAn = await _context.DanhSachDoAn.OrderByDescending(x => x.MaDoAn).FirstOrDefaultAsync();
            var maDoAn = 0;

            if (danhSachDoAn == null)
            {
                maDoAn = 0;
            }
            else
            {
                maDoAn = danhSachDoAn.MaDoAn + 1;
            }

            var newDoAn = new DoAn
            {
                MaDoAn = maDoAn,
                TenDoAn = doAn.TenDoAn,
                MoTa = doAn.MoTa,
                MaHuongNghienCuu = doAn.MaHuongNghienCuu,
                LienKetTaiDoAn = doAn.LienKetTaiDoAn,
                ThoiGianBaoCao = doAn.ThoiGianBaoCao,
                MaHocKy = doAn.MaHocKy,
                DiemTongKet = 8,
                NhanXetChung = doAn.NhanXetChung,
                DiaDiemBaoCao = doAn.DiaDiemBaoCao,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachDoAn.AddAsync(newDoAn);
            await _context.SaveChangesAsync();

            // Hướng dẫn đồ án
            for (int i = 0; i < doAn.HuongDanDoAn.Count; i++)
            {
                var record = new HuongDanDoAn
                {
                    MaDoAn = maDoAn,
                    MaGiangVien = doAn.HuongDanDoAn.ElementAt(i).MaGiangVien,
                    NhanXet = doAn.HuongDanDoAn.ElementAt(i).NhanXet
                };

                await _context.DanhSachHuongDanDoAn.AddAsync(record);
                await _context.SaveChangesAsync();
            }

            // Phản biện đồ án
            for (int i = 0; i < doAn.PhanBienDoAn.Count; i++)
            {
                var record = new PhanBienDoAn
                {
                    MaDoAn = maDoAn,
                    MaGiangVien = doAn.PhanBienDoAn.ElementAt(i).MaGiangVien,
                    NhanXet = doAn.PhanBienDoAn.ElementAt(i).NhanXet
                };

                await _context.DanhSachPhanBienDoAn.AddAsync(record);
                await _context.SaveChangesAsync();
            }

            // Hội đồng bảo vệ
            for (int i = 0; i < doAn.BaoVeDoAn.Count; i++)
            {
                var record = new ThanhVienHDBV
                {
                    MaDoAn = maDoAn,
                    MaGiangVien = doAn.BaoVeDoAn.ElementAt(i).MaGiangVien,
                    ChucVu = doAn.BaoVeDoAn.ElementAt(i).ChucVu
                };

                await _context.DanhSachThanhVienHDBV.AddAsync(record);
                await _context.SaveChangesAsync();
            }

            // Điểm đồ án
            for (int i = 0; i < doAn.DiemDoAn.Count; i++)
            {
                var record = new DiemDoAn
                {
                    MaDoAn = maDoAn,
                    MaGiangVien = doAn.DiemDoAn.ElementAt(i).MaGiangVien,
                    Diem = doAn.DiemDoAn.ElementAt(i).Diem,
                    HeSo = doAn.DiemDoAn.ElementAt(i).HeSo
                };

                await _context.DanhSachDiemDoAn.AddAsync(record);
                await _context.SaveChangesAsync();
            }

            // Thực hiện đồ án
            for (int i = 0; i < doAn.ThucHienDoAn.Count; i++)
            {
                var oldRecord = _context.DanhSachSinhVien.AsNoTracking().FirstOrDefault(x => x.MaSinhVien == doAn.ThucHienDoAn.ElementAt(i));
                var sinhVienToUpdate = new SinhVien
                {
                    MaSinhVien = doAn.ThucHienDoAn.ElementAt(i),
                    HoVaTen = oldRecord.HoVaTen,
                    MaLop = oldRecord.MaLop,
                    GioiTinh = oldRecord.GioiTinh,
                    NgaySinh = oldRecord.NgaySinh,
                    Email = oldRecord.Email,
                    QueQuan = oldRecord.QueQuan,
                    DiaChi = oldRecord.DiaChi,
                    SoDienThoai = oldRecord.SoDienThoai,
                    ThoiGianTao = oldRecord.ThoiGianTao,
                    ThoiGianCapNhat = DateTime.Now,
                    TrangThai = oldRecord.TrangThai,
                    MaDoAn = maDoAn
                };

                _context.DanhSachSinhVien.Update(sinhVienToUpdate);
                await _context.SaveChangesAsync();
            }

            return newDoAn;
        }

        public async Task<DoAn> PermanentlyDeleteById(int id)
        {
            var doAnToDelete = await _context.DanhSachDoAn.FirstOrDefaultAsync(x => x.MaDoAn == id);

            _context.DanhSachDoAn.Remove(doAnToDelete);
            await _context.SaveChangesAsync();
            return doAnToDelete;
        }

        public async Task<PagedList<DoAn>> GetAll(DoAnParams userParams)
        {
            var result = _context.DanhSachDoAn
                .Include(x => x.HuongNghienCuu)
                .Include(x => x.HuongDanDoAn)
                .Include(x => x.PhanBienDoAn)
                .Include(x => x.ThanhVienHDBV)
                .AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var maHuongNghienCuu = userParams.MaHuongNghienCuu;
            var thoiGianBaoCaoBatDau = userParams.ThoiGianBaoCaoBatDau;
            var thoiGianBaoCaoKetThuc = userParams.ThoiGianBaoCaoKetThuc;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenDoAn.ToLower().Contains(keyword.ToLower()) || x.LienKetTaiDoAn.ToLower().Contains(keyword.ToLower()) || x.MaDoAn.ToString() == keyword);
            }

            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
            }

            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
            }

            if (maHuongNghienCuu > -1)
            {
                result = result.Where(x => x.MaHuongNghienCuu == maHuongNghienCuu);
            }

            if (trangThai == -1 || trangThai == 1)
            {
                result = result.Where(x => x.TrangThai == trangThai);
            }

            if (thoiGianBaoCaoBatDau.GetHashCode() != 0 && thoiGianBaoCaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianBaoCao >= thoiGianBaoCaoBatDau && x.ThoiGianBaoCao <= thoiGianBaoCaoKetThuc);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
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

                    case "TenDoAn":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenDoAn);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenDoAn);
                        }
                        break;
                    case "MoTa":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MoTa);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MoTa);
                        }
                        break;
                    case "MaHuongNghienCuu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaHuongNghienCuu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaDoAn);
                        }
                        break;
                    case "LienKetTaiDoAn":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.LienKetTaiDoAn);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.LienKetTaiDoAn);
                        }
                        break;
                    case "ThoiGianBaoCao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianBaoCao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianBaoCao);
                        }
                        break;
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

                    case "DiemTongKet":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DiemTongKet);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DiemTongKet);
                        }
                        break;

                    case "NhanXetChung":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NhanXetChung);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NhanXetChung);
                        }
                        break;
                    case "DiaDiemBaoCao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DiaDiemBaoCao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DiaDiemBaoCao);
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

            return await PagedList<DoAn>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<DoAn> GetById(int id)
        {
            var result = await _context.DanhSachDoAn
                .Include(x => x.HuongNghienCuu)
                .Include(x => x.HuongDanDoAn)
                .Include(x => x.PhanBienDoAn)
                .Include(x => x.ThanhVienHDBV)
                .Include(x => x.DiemDoAn)
                .Include(x => x.SinhVien)
                .Include(x => x.HocKy)
                .FirstOrDefaultAsync(x => x.MaDoAn == id);

                //.Include(x => x.HuongNghienCuu)
                //                              .Include(x => x.HocKy)
                //                              .Include(x => x.PhanBienDoAn)
                //                              .Include(x => x.HuongDanDoAn)
                //                              .Include(x => x.DiemDoAn)
                //                              .Include(x => x.SinhVien).FirstOrDefaultAsync(x => x.MaDoAn == id);
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

        public async Task<DoAn> UpdateById(int id, DoAnForUpdateDto doAn)
        {
            var oldRecord = await _context.DanhSachDoAn.AsNoTracking().FirstOrDefaultAsync(x => x.MaDoAn == id);
            var doAnToUpdate = new DoAn
            {
                MaDoAn = id,
                TenDoAn = doAn.TenDoAn,
                MoTa = doAn.MoTa,
                MaHuongNghienCuu = doAn.MaHuongNghienCuu,
                LienKetTaiDoAn = doAn.LienKetTaiDoAn,
                ThoiGianBaoCao = doAn.ThoiGianBaoCao,
                MaHocKy = doAn.MaHocKy,
                DiemTongKet = doAn.DiemTongKet,
                NhanXetChung = doAn.NhanXetChung,
                DiaDiemBaoCao = doAn.DiaDiemBaoCao,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
            };

            _context.DanhSachDoAn.Update(doAnToUpdate);
            await _context.SaveChangesAsync();
            return doAnToUpdate;
        }

        public async Task<DoAn> TemporarilyDeleteById(int id)
        {
            var doAnToTemporarilyDeleteById = await _context.DanhSachDoAn.FirstOrDefaultAsync(x => x.MaDoAn == id);

            doAnToTemporarilyDeleteById.TrangThai = -1;
            doAnToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachDoAn.Update(doAnToTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return doAnToTemporarilyDeleteById;
        }

        public async Task<DoAn> RestoreById(int id)
        {
            var doAnToRestoreById = await _context.DanhSachDoAn.FirstOrDefaultAsync(x => x.MaDoAn == id);

            doAnToRestoreById.TrangThai = 1;
            doAnToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachDoAn.Update(doAnToRestoreById);
            await _context.SaveChangesAsync();

            return doAnToRestoreById;
        }

        public Object GetStatusStatistics(DoAnParams userParams)
        {
            var result = _context.DanhSachDoAn.Include(x => x.HuongNghienCuu)
                                              .Include(x => x.PhanBienDoAn)
                                              .Include(x => x.HuongDanDoAn)
                                              .Include(x => x.DiemDoAn)
                                              .Include(x => x.SinhVien).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var maHuongNghienCuu = userParams.MaHuongNghienCuu;
            var thoiGianBaoCaoBatDau = userParams.ThoiGianBaoCaoBatDau;
            var thoiGianBaoCaoKetThuc = userParams.ThoiGianBaoCaoKetThuc;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenDoAn.ToLower().Contains(keyword.ToLower()) || x.LienKetTaiDoAn.ToLower().Contains(keyword.ToLower()) || x.MaDoAn.ToString() == keyword);
            }

            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
            }

            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
            }

            if (maHuongNghienCuu > -1)
            {
                result = result.Where(x => x.MaHuongNghienCuu == maHuongNghienCuu);
            }

            if (trangThai == -1 || trangThai == 1)
            {
                result = result.Where(x => x.TrangThai == trangThai);
            }

            if (thoiGianBaoCaoBatDau.GetHashCode() != 0 && thoiGianBaoCaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianBaoCao >= thoiGianBaoCaoBatDau && x.ThoiGianBaoCao <= thoiGianBaoCaoKetThuc);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
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

                    case "TenDoAn":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenDoAn);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenDoAn);
                        }
                        break;
                    case "MoTa":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MoTa);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MoTa);
                        }
                        break;
                    case "MaHuongNghienCuu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaHuongNghienCuu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaDoAn);
                        }
                        break;
                    case "LienKetTaiDoAn":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.LienKetTaiDoAn);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.LienKetTaiDoAn);
                        }
                        break;
                    case "ThoiGianBaoCao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianBaoCao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianBaoCao);
                        }
                        break;
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

                    case "DiemTongKet":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DiemTongKet);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DiemTongKet);
                        }
                        break;

                    case "NhanXetChung":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NhanXetChung);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NhanXetChung);
                        }
                        break;
                    case "DiaDiemBaoCao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DiaDiemBaoCao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DiaDiemBaoCao);
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
    }
}
