using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Models
{
    public class ThanhVienHDBV : BaseModel
    {
        //public int MaThanhVienHDBV { get; set; }
        public int MaDoAn { get; set; }
        public DoAn DoAn { get; set; }
        public string MaGiangVien { get; set; }
        public GiangVien GiangVien { get; set; }
        public string ChucVu { get; set; }
    }
}
