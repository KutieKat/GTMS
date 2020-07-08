using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Helpers.Params
{
    public class PhanBienDoAnParams : BaseParams
    {
        public PhanBienDoAnParams()
        {
            MaDoAn = -1;
        }

        public int MaDoAn { get; set; }
        public string MaGiangVien { get; set; }
    }
}
