using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM05500Common.DTO
{
    public class GSM05520ListDTO : R_APIResultBaseDTO
    {
        public List<GSM05520DTO> Data { get; set; }

        
    }

    public class GSM05520GridDTO
    {
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime CCREATE_BY { get; set; }
        public string CRATE_DATE { get; set; }

    }
}
