using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Models
{
    public class Lop : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public string TenVietTat { get; set; }
        public int MaKhoa { get; set; }
        public Khoa Khoa { get; set; }
        public string HeDaoTao { get; set; }
        public int MaKhoaDaoTao { get; set; }
        public KhoaDaoTao KhoaDaoTao { get; set; }

        public ICollection<SinhVien> SinhVien { get; set; }
    }
}
