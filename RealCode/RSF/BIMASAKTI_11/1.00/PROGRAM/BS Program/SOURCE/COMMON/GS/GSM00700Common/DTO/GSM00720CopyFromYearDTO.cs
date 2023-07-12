using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO
{
    public class GSM00720CopyFromYearDTO : R_APIResultBaseDTO
    {
        public string CFROM_CASH_FLOW_FLAG { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CYEAR { get; set; }

    }
}
