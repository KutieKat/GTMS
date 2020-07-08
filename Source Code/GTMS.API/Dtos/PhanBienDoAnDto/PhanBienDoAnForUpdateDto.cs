using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.PhanBienDoAnDto
{
    public class PhanBienDoAnForUpdateDto : BaseDto
    {
        [Required]
        public int MaDoAn { get; set; }

        [Required]
        public string MaGiangVien { get; set; }
    }
}
