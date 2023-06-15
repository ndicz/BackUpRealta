using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO
{
    public class GSM00710ListDTO : R_APIResultBaseDTO
    {
        public List<GSM00710DTO> Data { get; set; }
    }
}
