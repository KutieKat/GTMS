using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.HocKyDto
{
    public class HocKyForCreateMultipleDto:BaseDto
    {
        [Required]
        public int MaHocKy { get; set; }

        [Required]
        public string TenHocKy { get; set; }

        [Required]
        public string NamHoc { get; set; }

        [Required]
        public DateTime ThoiGianBatDau { get; set; }

        [Required]
        public DateTime ThoiGianKetThuc { get; set; }

        public override string ToString()
        {
            return "MaHocKy, TenHocKy, NamHoc, ThoiGianBatDau, ThoiGianKetThuc";
        }
    }
}
