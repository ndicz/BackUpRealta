using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM05500Common.DTO
{
    public class GSM05520DTOLocalBaseCurrency : R_APIResultBaseDTO
    {
        public string CLOCAL_CURRENCY { get; set; }
        public string CLOCAL_CURRENCY_NAME {get; set; }
        public string CBASE_CURRENCY { get; set; }
        public string CBASE_CURRENCY_NAME { get; set; }
    }
}
