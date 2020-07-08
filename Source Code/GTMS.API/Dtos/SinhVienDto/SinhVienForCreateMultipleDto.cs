using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.SinhVienDto
{
    public class SinhVienForCreateMultipleDto:BaseDto
    {
        [Required]
        public string MaSinhVien { get; set; }

        [Required]
        public string HoVaTen { get; set; }

        [Required]
        public int MaLop { get; set; }

        [Required]
        public string GioiTinh { get; set; }

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string QueQuan { get; set; }

        [Required]
        public string DiaChi { get; set; }

        [Required]
        public string SoDienThoai { get; set; }
        public int? MaDoAn { get; set; }

        public override string ToString()
        {
            return "MaSinhVien, HoVaTen, MaLop, GioiTinh, NgaySinh, Email, QueQuan, DiaChi, SoDienThoai, MaDoAn";
        }
    }
}
