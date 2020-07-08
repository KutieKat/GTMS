using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.ThanhVienHDBV
{
    public class ThanhVienHDBVForListDto : BaseDto
    {
        public int MaThanhVienHDBV { get; set; }
        public GiangVien GiangVien { get; set; }
        public string ChucVuHDBV { get; set; }
        public double Diem { get; set; }
        public string NhanXet { get; set; }
    }
}
