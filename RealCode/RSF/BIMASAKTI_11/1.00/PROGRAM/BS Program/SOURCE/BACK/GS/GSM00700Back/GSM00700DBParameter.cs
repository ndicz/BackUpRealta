using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM00700Back
{
    public class GSM00700DBParameter
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
    }
}
