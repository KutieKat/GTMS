using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Models
{
    public class KhoaDaoTao : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaKhoaDaoTao { get; set; }
        public string TenKhoaDaoTao { get; set; }
        public string TenVietTat { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }

        public ICollection<Lop> Lop { get; set; }
    }
}
