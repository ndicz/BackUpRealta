using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM02300Common.DTO
{
    public class GSM02300DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }

        public string CPROPERTY_TYPE_NAME { get; set; }
        public string CPROPERTY_TYPE_CODE { get; set; }
        public bool LSINGLE_UNIT { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }

        
    }
}
