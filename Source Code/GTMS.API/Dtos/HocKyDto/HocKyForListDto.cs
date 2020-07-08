using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.HocKyDto
{
    public class HocKyForListDto : BaseDto
    {
        public int MaHocKy { get; set; }
        public string TenHocKy { get; set; }
        public string NamHoc { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }
}
