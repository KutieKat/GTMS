using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.KhoaDaoTaoDto
{
    public class KhoaDaoTaoForListDto : BaseDto
    {
        public int MaKhoaDaoTao { get; set; }
        public string TenKhoaDaoTao { get; set; }
        public string TenVietTat { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }
}
