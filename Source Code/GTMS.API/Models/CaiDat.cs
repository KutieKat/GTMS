using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Models
{
    public class CaiDat : BaseModel
    {
        public int MaCaiDat { get; set; }
        public string TenDonViChuQuan { get; set; }
        public string TenKhoa { get; set; }
    }
}
