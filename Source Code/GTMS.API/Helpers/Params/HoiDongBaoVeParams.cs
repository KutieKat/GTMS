using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Helpers.Params
{
    public class HoiDongBaoVeParams : BaseParams
    {
        public HoiDongBaoVeParams()
        {
            MaHocKy = -1;
        }

        public int MaHocKy { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }
}
