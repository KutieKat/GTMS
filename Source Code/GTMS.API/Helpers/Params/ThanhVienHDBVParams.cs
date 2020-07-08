using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Helpers.Params
{
    public class ThanhVienHDBVParams : BaseParams
    {
        public ThanhVienHDBVParams()
        {
            MaChucVuHDBV = -1;
        }

        public string MaGiangVien { get; set; }
        public int MaChucVuHDBV { get; set; }
    }
}
