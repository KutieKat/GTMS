using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.HoiDongBaoVeDto
{
    public class HoiDongBaoVeForViewDto : BaseDto
    {
        public int MaHoiDongBaoVe { get; set; }
        public string TenHoiDongBaoVe { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public HocKy HocKy { get; set; }
    }
}
