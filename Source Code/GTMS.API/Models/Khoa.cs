using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTMS.API.Models
{
    public class Khoa : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public string TenVietTat { get; set; }

        public ICollection<GiangVien> GiangVien { get; set; }
        public ICollection<Lop> Lop { get; set; }
    }
}
