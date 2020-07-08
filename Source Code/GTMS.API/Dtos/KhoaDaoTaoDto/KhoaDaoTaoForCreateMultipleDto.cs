using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.KhoaDaoTaoDto
{
    public class KhoaDaoTaoForCreateMultipleDto:BaseDto
    {
        [Required]
        public int MaKhoaDaoTao { get; set; }
        [Required]
        public string TenKhoaDaoTao { get; set; }

        [Required]
        public string TenVietTat { get; set; }

        [Required]
        public DateTime ThoiGianBatDau { get; set; }

        [Required]
        public DateTime ThoiGianKetThuc { get; set; }
        public override string ToString()
        {
            return "MaKhoaDaoTao, TenKhoaDaoTao, TenVietTat, ThoiGianBatDau, ThoiGianKetThuc";
        }
    }
}
