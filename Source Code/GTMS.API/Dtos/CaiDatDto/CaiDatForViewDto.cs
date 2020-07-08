using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.CaiDatDto
{
    public class CaiDatForViewDto : BaseDto
    {
        public int MaCaiDat { get; set; }
        public string TenDonViChuQuan { get; set; }
        public string TenKhoa { get; set; }
    }
}
