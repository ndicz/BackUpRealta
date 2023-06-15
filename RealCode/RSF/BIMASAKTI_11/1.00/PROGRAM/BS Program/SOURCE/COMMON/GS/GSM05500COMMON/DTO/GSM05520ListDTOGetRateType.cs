using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM05500Common.DTO
{
    public class GSM05520ListDTOGetRateType : R_APIResultBaseDTO
    {
        public List<GSM05520DTOGetRateType> Data { get; set; }
    }
}
