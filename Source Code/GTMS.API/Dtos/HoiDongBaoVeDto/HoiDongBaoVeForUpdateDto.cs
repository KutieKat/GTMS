using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.HoiDongBaoVeDto
{
    public class HoiDongBaoVeForUpdateDto : BaseDto
    {
        [Required]
        public string TenHoiDongBaoVe { get; set; }

        [Required]
        public DateTime ThoiGianBatDau { get; set; }

        [Required]
        public DateTime ThoiGianKetThuc { get; set; }

        [Required]
        public int MaHocKy { get; set; }
    }
}
