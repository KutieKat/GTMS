using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.KhoaDaoTaoDto
{
    public class KhoaDaoTaoForUpdateDto : BaseDto
    {
        [Required]
        public string TenKhoaDaoTao { get; set; }

        [Required]
        public string TenVietTat { get; set; }

        [Required]
        public DateTime ThoiGianBatDau { get; set; }

        [Required]
        public DateTime ThoiGianKetThuc { get; set; }
   
    }
}
