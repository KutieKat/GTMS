using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Dtos
{
    public class ValidationResultDto
    {
        public bool IsValid { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
