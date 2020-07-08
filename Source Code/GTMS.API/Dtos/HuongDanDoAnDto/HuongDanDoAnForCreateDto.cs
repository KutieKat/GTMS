using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.HuongDanDoAnDto
{
    public class HuongDanDoAnForCreateDto : BaseDto
    {
        [Required]
        public string MaGiangVien { get; set; }

        [Required]
        public int MaDoAn { get; set; }

        [Required]
        public double Diem { get; set; }

        [Required]
        public string NhanXet { get; set; }
    }
}
