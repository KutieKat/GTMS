using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.ThucHienDoAnDto
{
    public class ThucHienDoAnForViewDto : BaseDto
    {
        public DoAn DoAn { get; set; }
        public SinhVien SinhVien { get; set; }
    }
}
