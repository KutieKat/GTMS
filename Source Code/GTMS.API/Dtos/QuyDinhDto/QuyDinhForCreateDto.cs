using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.QuyDinhDto
{
    public class QuyDinhForCreateDto : BaseDto
    {
        [Required]
        public string TenQuyDinh { get; set; }

        [Required]
        public DateTime ThoiGianBatDauHieuLuc { get; set; }
        public int? SoSVTHToiThieu { get; set; }
        public int? SoSVTHToiDa { get; set; }
        public int? SoGVHDToiThieu { get; set; }
        public int? SoGVHDToiDa { get; set; }
        public int? SoGVPBToiThieu { get; set; }
        public int? SoGVPBToiDa { get; set; }
        public int? SoTVHDToiThieu { get; set; }
        public int? SoTVHDToiDa { get; set; }
        public int? SoCTHDToiThieu { get; set; }
        public int? SoCTHDToiDa { get; set; }
        public int? SoTKHDToiDa { get; set; }
        public int? SoTKHDToiThieu { get; set; }
        public int? SoUVHDToiThieu { get; set; }
        public int? SoUVHDToiDa { get; set; }
        public int? SoChuSoThapPhan { get; set; }
        public double? DiemSoToiThieu { get; set; }
        public double? DiemSoToiDa { get; set; }
        public int? HeSoGVHD { get; set; }
        public int? HeSoGVPB { get; set; }
        public int? HeSoTVHD { get; set; }
    }
}
