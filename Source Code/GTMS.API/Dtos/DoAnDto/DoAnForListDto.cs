using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.DoAnDto
{
    public class DoAnForListDto : BaseDto
    {
        public int MaDoAn { get; set; }
        public string TenDoAn { get; set; }
        public string MoTa { get; set; }
        public HuongNghienCuu HuongNghienCuu { get; set; }
        public DateTime ThoiGianBaoCao { get; set; }
        public HocKy HocKy { get; set; }
        public string LienKetTaiDoAn { get; set; }
        public double DiemTongKet { get; set; }
        public string NhanXetChung { get; set; }
        public string DiaDiemBaoCao { get; set; }
    }
}
