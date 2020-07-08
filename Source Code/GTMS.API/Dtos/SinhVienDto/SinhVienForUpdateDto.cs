using System;
using System.ComponentModel.DataAnnotations;

namespace GTMS.API.Dtos.SinhVienDto
{
    public class SinhVienForUpdateDto : BaseDto
    {
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
    }
}
