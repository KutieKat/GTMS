using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Helpers.Params
{
    public class SinhVienParams : BaseParams
    {
        public SinhVienParams()
        {
            MaLop = -1;
          
        }

        public int MaLop { get; set; }
    
        public DateTime NgaySinhBatDau { get; set; }
        public DateTime NgaySinhKetThuc { get; set; }
    }
}
