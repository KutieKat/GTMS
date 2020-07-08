using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.SinhVienDto
{
    public class SinhVienForViewDto : BaseDto
    {
        public string MaSinhVien { get; set; }
        public string HoVaTen { get; set; }
        public Lop Lop { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; }
        public string QueQuan { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public DoAn DoAn { get; set; }
    }
}
