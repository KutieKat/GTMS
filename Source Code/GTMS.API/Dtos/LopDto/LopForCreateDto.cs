using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.LopDto
{
    public class LopForCreateDto : BaseDto
    {
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
    }
}