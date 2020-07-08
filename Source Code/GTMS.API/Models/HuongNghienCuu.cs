using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Models
{
    public class HuongNghienCuu : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHuongNghienCuu { get; set; }
        public string TenHuongNghienCuu { get; set; }

        public ICollection<DoAn> DoAn { get; set; }
    }
}
