using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.ThucHienDoAnDto
{
    public class ThucHienDoAnForCreateDto : BaseDto
    {
        [Required]
        public int MaDoAn { get; set; }

        [Required]
        public string MaSinhVien { get; set; }
    }
}
