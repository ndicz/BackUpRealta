using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM05500Common.DTO
{
    public class GSM05520DTOGetRateType : R_APIResultBaseDTO
    {
        public string CRATETYPE_CODE { get; set; }
        public string CRATETYPE_DESCRIPTION { get; set; }

    }
}