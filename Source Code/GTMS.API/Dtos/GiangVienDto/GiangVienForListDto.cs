using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.GiangVienDto
{
    public class GiangVienForListDto : BaseDto
    {
        public string MaGiangVien { get; set; }
        public string HoVaTen { get; set; }
        public Khoa Khoa { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string QueQuan { get; set; }
        public string DiaChi { get; set; }
        public string DonViCongTac { get; set; }
        public string HocVi { get; set; }
        public string HocHam { get; set; }
    }
}
