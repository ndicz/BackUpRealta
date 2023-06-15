using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO
{
    public class GSM00720ListDTO : R_APIResultBaseDTO
    {
        public List<GSM00720DTO> Data { get; set; }
    }
}
