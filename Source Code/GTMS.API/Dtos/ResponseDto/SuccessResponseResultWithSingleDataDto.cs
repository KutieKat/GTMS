﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos.ResponseDto
{
    public class SuccessResponseResultWithSingleDataDto : ResponseResultDto
    {
        public Object Data { get; set; }
    }
}
