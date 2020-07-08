using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.DoAnDto
{
    public class DoAnForCreateDto : BaseDto
    {
        [Required]
        public string TenDoAn { get; set; }
        [Required]
        public string MoTa { get; set; }
        [Required]
        public int MaHuongNghienCuu { get; set; }
        [Required]
        public DateTime ThoiGianBaoCao { get; set; }
        [Required]
        public int MaHocKy { get; set; }
        [Required]
        public string LienKetTaiDoAn { get; set; }
        [Required]
        public double DiemTongKet { get; set; }
        [Required]
        public string NhanXetChung { get; set; }
        [Required]
        public string DiaDiemBaoCao { get; set; }
        public ICollection<DoAnBVDA> BaoVeDoAn { get; set; }
        public ICollection<DoAnDDA> DiemDoAn { get; set; }
        public ICollection<DoAnHDDA> HuongDanDoAn { get; set; }
        public ICollection<DoAnPBDA> PhanBienDoAn { get; set; }
        public ICollection<string> ThucHienDoAn { get; set; }
    }
}
