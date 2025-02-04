﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.TaiKhoanDto
{
    public class TaiKhoanForViewDto : BaseDto
    {
        public int MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string PhanQuyen { get; set; }
        public string HoVaTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; }
        public string QueQuan { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string MaGiangVien { get; set; }
        public string MaSinhVien { get; set; }
    }
}
