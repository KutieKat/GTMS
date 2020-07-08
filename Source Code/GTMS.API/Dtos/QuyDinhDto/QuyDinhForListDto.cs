using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.QuyDinhDto
{
    public class QuyDinhForListDto : BaseDto
    {
        public int MaQuyDinh { get; set; }
        public string TenQuyDinh { get; set; }
        public DateTime ThoiGianBatDauHieuLuc { get; set; }
    }
}
