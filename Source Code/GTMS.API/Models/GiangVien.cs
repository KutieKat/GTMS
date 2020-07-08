using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Models
{
    public class GiangVien : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MaGiangVien { get; set; }
        public string HoVaTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string QueQuan { get; set; }
        public string DiaChi { get; set; }
        public string DonViCongTac { get; set; }
        public int? MaKhoa { get; set; }
        public Khoa Khoa { get; set; }
        public string HocVi { get; set; }
        public string HocHam { get; set; }

        public ICollection<PhanBienDoAn> PhanBienDoAn { get; set; }
        public ICollection<HuongDanDoAn> HuongDanDoAn { get; set; }
        public ICollection<ThanhVienHDBV> ThanhVienHDBV { get; set; }
        public ICollection<DiemDoAn> DiemDoAn { get; set; }
    }
}
