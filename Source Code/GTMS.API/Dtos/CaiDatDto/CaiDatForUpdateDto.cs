using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.CaiDatDto
{
    public class CaiDatForUpdateDto : BaseDto
    {
        [Required]
        public string TenDonViChuQuan { get; set; }

        [Required]
        public string TenKhoa { get; set; }
    }
}
