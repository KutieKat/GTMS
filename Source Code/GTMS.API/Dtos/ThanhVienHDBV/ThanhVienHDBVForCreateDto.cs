using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.ThanhVienHDBV
{
    public class ThanhVienHDBVForCreateDto : BaseDto
    {
        
        [Required]
        public string MaGiangVien { get; set; }

        [Required]
        public int MaChucVuHDBV { get; set; }

        [Required]
        public double Diem { get; set; }

        [Required]
        public string NhanXet { get; set; }
    }
}
