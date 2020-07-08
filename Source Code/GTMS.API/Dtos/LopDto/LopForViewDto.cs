using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.LopDto
{
    public class LopForViewDto : BaseDto
    {
        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public string TenVietTat { get; set; }
        public Khoa Khoa { get; set; }
        public string HeDaoTao { get; set; }
        public KhoaDaoTao KhoaDaoTao { get; set; }
    }
}
