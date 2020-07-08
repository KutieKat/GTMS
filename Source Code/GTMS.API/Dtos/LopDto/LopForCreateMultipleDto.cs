using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.LopDto
{
    public class LopForCreateMultipleDto:BaseDto
    {
        [Required]
        public int MaLop { get; set; }

        [Required]
        public string TenLop { get; set; }

        [Required]
        public string TenVietTat { get; set; }

        [Required]
        public int MaKhoa { get; set; }

        [Required]
        public string HeDaoTao { get; set; }

        [Required]
        public int MaKhoaDaoTao { get; set; }

        public override string ToString()
        {
            return "MaLop, TenLop, TenVietTat, MaKhoa, HeDaoTao, MaKhoaDaoTao";
        }
    }
}
