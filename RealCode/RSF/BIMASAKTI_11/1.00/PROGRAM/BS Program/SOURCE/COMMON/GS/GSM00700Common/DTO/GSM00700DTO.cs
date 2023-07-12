using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO
{
    public class GSM00700DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }

        public string CCASH_FLOW_GROUP_CODE { get; set; }
        public string CCASH_FLOW_GROUP_NAME { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }

        //this for combo box
        public string CCASH_FLOW_GROUP_TYPE_DESCR { get; set; }
        public string CCASH_FLOW_GROUP_TYPE { get; set; }

    }
    public class GSM00700ListDTO : R_APIResultBaseDTO
    {
        public List<GSM00700DTO> Data { get; set; }

    }
}
