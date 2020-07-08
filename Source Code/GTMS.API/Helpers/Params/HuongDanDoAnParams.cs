using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Helpers.Params
{
    public class HuongDanDoAnParams : BaseParams
    {
        public HuongDanDoAnParams()
        {
            MaDoAn = -1;
        }

        public string MaGiangVien { get; set; }
        public int MaDoAn { get; set; }
    }
}
