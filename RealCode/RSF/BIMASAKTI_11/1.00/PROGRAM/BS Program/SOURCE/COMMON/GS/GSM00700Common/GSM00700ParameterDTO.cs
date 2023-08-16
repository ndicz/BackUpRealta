using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common
{
    public class GSM00700ParameterDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCASH_FLOW_GROUP_CODE { get; set; }
        public string CUSER_ID { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCYEAR { get; set; }

        public string CFROM_CASH_FOW_FLAG { get; set; }
        public string CFROM_CASH_FLOW_CODE { get; set; }
        public string CFROM_YEAR { get; set; }
        public string CTO_CASH_FLOW_CODE { get; set; }
        public string CTO_YEAR { get; set; }
        public string CUSER_LOGIN_ID { get; set; }


        public string CCASH_FLOW_GROUP { get; set; }
        public string CYEAR { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_RATE { get; set; }
        public Int32 INO_PERIOD_FROM { get; set; }
        public Int32 INO_PERIOD_TO { get; set; }
    }
}
