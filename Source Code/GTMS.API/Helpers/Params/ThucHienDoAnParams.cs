using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Helpers.Params
{
    public class ThucHienDoAnParams : BaseParams
    {
        public ThucHienDoAnParams()
        {
            MaDoAn = -1;
        }

        public int MaDoAn { get; set; }
        public string MaSinhVien { get; set; }
    }
}
