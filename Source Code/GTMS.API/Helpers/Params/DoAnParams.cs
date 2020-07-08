using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Helpers.Params
{
    public class DoAnParams : BaseParams
    {
        public DoAnParams()
        {
            MaHuongNghienCuu = -1;
        }
        public int MaHuongNghienCuu { get; set; }
        public DateTime ThoiGianBaoCaoBatDau { get; set; }
        public DateTime ThoiGianBaoCaoKetThuc { get; set; }
    }
}
