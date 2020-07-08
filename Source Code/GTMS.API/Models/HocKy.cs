using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Models
{
    public class HocKy : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHocKy { get; set; }
        public string TenHocKy { get; set; }
        public string NamHoc { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public ICollection<DoAn> DoAn { get; set; }

    }
}
