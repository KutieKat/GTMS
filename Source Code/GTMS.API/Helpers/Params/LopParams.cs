using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Helpers.Params
{
    public class LopParams : BaseParams
    {
        public LopParams()
        {
            MaKhoa = -1;
            MaKhoaDaoTao = -1;
        }

        public int MaKhoa { get; set; }
        public int MaKhoaDaoTao { get; set; }
    }
}
