using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.HocKyDto
{
    public class HocKyForUpdateDto : BaseDto
    {
        [Required]
        public string TenHocKy { get; set; }

        [Required]
        public DateTime ThoiGianBatDau { get; set; }

        [Required]
        public string NamHoc { get; set; }

        [Required]
        public DateTime ThoiGianKetThuc { get; set; }
    }
}
