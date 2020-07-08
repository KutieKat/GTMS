using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Models
{
    public class PhanBienDoAn : BaseModel
    {
        public int MaDoAn { get; set; }
        public DoAn DoAn { get; set; }
        public string MaGiangVien { get; set; }
        public GiangVien GiangVien { get; set; }
        public string NhanXet { get; set; }
    }
}
