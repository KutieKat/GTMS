using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Models
{
    public class DoAn : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDoAn { get; set; }
        public string TenDoAn { get; set; }
        public string MoTa { get; set; }
        public int MaHuongNghienCuu { get; set; }
        public HuongNghienCuu HuongNghienCuu { get; set; }
        public DateTime ThoiGianBaoCao { get; set; }
        public int MaHocKy { get; set; }
        public HocKy HocKy { get; set; }
        public string LienKetTaiDoAn { get; set; }
        public double DiemTongKet { get; set; }
        public string NhanXetChung { get; set; }
        public string DiaDiemBaoCao { get; set; }

        public ICollection<PhanBienDoAn> PhanBienDoAn { get; set; }
        public ICollection<HuongDanDoAn> HuongDanDoAn { get; set; }
        public ICollection<ThanhVienHDBV> ThanhVienHDBV { get; set; }
        public ICollection<SinhVien> SinhVien { get; set; }
        public ICollection<DiemDoAn> DiemDoAn { get; set; }
    }
}
