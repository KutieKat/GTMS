using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.HuongNghienCuuDto
{
    public class HuongNghienCuuForCreateDto : BaseDto
    {
        [Required]
        public string TenHuongNghienCuu { get; set; }
    }
}
