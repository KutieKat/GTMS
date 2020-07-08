using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.ChucVuHDBVDto
{
    public class ChucVuHDBVForCreateDto : BaseDto
    {
        [Required]
        public string TenChucVuHDBV { get; set; }
    }
}
