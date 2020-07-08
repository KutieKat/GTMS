using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.KhoaDto
{
    public class KhoaForListDto : BaseDto
    {
        public int MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public string TenVietTat { get; set; }
    }
}
