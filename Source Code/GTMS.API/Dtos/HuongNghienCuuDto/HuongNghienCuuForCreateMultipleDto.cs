using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.HuongNghienCuuDto
{
    public class HuongNghienCuuForCreateMultipleDto:BaseDto
    {
        [Required]
        public int MaHuongNghienCuu { get; set; }

        [Required]
        public string TenHuongNghienCuu { get; set; }

        public override string ToString()
        {
            return "MaHuongNghienCuu, TenHuongNghienCuu";
        }
    }
}
