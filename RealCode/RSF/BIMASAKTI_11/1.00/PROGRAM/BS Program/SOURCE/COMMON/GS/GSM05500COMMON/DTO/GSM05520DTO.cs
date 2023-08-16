using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM05500Common.DTO
{
    public class GSM05520DTO : R_APIResultBaseDTO
    {
        public string CLOCAL_CURRENCY { get; set; }
        public string CLOCAL_CURRENCY_NAME { get; set; }
        public string CBASE_CURRENCY { get; set; }
        public string CBASE_CURRENCY_NAME { get; set; }
        public string CUSER_ID { get; set; } 
        public string CCOMPANY_ID { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CRATETYPE_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public Decimal NLBASE_RATE_AMOUNT { get; set; }
        public Decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public Decimal NBBASE_RATE_AMOUNT { get; set; }
        public Decimal NBCURRENCY_RATE_AMOUNT { get; set; }
        public DateTime DUPDATE_DATE { get; set; } 
        public string CUPDATE_BY { get; set; }
        public string CCREATE_BY { get; set; }
        public string CRATE_DATE { get; set; } = DateTime.Today.ToString("yyyymmdd");



    }
}
