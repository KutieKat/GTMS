﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.GiangVienDto
{
    public class GiangVienForCreateDto : BaseDto
    {
        [Required]
        public string HoVaTen { get; set; }

        public int? MaKhoa { get; set; }

        [Required]
        public string GioiTinh { get; set; }

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string SoDienThoai { get; set; }

        [Required]
        public string QueQuan { get; set; }

        [Required]
        public string DiaChi { get; set; }

        [Required]
        public string DonViCongTac { get; set; }

        //[Required]
        public string HocVi { get; set; }

        //[Required]
        public string HocHam { get; set; }
    }
}
