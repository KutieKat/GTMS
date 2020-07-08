using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.HuongNghienCuuDto
{
    public class HuongNghienCuuForListDto : BaseDto
    {
        public int MaHuongNghienCuu { get; set; }
        public string TenHuongNghienCuu { get; set; }
    }
}
