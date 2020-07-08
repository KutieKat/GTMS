using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Models
{
    public class DiemDoAn:BaseModel
    {
        public int MaDoAn { get; set; }
        public DoAn DoAn { get; set; }
        public string MaGiangVien { get; set; }
        public GiangVien GiangVien { get; set; }
        public double Diem { get; set; }
        public int HeSo { get; set; }
    }
}
