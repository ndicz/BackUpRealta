using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO
{
    public class GSM00700ListDTO : R_APIResultBaseDTO
    {
        public List<GSM00700DTO> Data { get; set; }

    }
}
