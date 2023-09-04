using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace GSM05500Common.DTO
{
    public class GSM05500DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }

    public class GSM05500ListDTO : R_APIResultBaseDTO
    {
        public List<GSM05500DTO> Data { get; set; }

    }
}
