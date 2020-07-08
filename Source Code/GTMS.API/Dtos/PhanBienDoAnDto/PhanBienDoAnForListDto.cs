using GTMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.PhanBienDoAnDto
{
    public class PhanBienDoAnForListDto : BaseDto
    {
        public DoAn DoAn { get; set; }
        public GiangVien GiangVien { get; set; }
    }
}
