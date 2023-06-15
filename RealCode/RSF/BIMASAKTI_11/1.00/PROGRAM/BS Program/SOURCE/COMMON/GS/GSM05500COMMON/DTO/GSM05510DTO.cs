using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM05500Common.DTO
{
    public class GSM05510DTO : R_APIResultBaseDTO
    {
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CRATETYPE_CODE { get; set; }
        public string CRATETYPE_DESCRIPTION { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
