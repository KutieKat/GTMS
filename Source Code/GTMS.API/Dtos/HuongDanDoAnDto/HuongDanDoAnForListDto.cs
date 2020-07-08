using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.HuongDanDoAnDto
{
    public class HuongDanDoAnForListDto : BaseDto
    {
        public GiangVien GiangVien { get; set; }
        public DoAn DoAn { get; set; }
        public double Diem { get; set; }
        public string NhanXet { get; set; }
    }
}
