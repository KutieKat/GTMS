using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.KhoaDto
{
    public class KhoaForCreateMultipleDto : BaseDto
    {
        [Required]
        public int MaKhoa { get; set; }

        [Required]
        public string TenKhoa { get; set; }

        [Required]
        public string TenVietTat { get; set; }

        public override string ToString()
        {
            return "MaKhoa, TenKhoa, TenVietTat";
        }
    }
}
