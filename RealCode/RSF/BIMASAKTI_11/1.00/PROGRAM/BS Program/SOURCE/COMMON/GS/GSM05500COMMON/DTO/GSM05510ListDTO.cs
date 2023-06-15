using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM05500Common.DTO
{
    public class GSM05510ListDTO: R_APIResultBaseDTO
    {
        public List<GSM05510DTO> Data { get; set; }

    
    }

    public class GSM05510GridDTO
    {
        public string CRATETYPE_CODE { get; set; }
        public string CRATETYPE_DESCRIPTION { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
