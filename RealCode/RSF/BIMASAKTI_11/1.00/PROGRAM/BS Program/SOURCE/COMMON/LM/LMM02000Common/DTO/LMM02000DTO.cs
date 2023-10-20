using System;
using LMM02000Common;
using R_APICommonDTO;

namespace LMM02000Common.DTO
{
    public class LMM02000DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CRATETYPE_CODE { get; set; }


        public bool LACTIVE { get; set; } = true;
        public string CSALESMAN_ID { get; set; }
        public string CSALESMAN_NAME { get; set; }
        public string CGENDER_DESCR { get; set; }
        public string CMOBILE_PHONE1 { get; set; }
        public string CMOBILE_PHONE2 { get; set; }
        public string CEMAIL { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }


        public string CADDRESS { get; set; }
        public string CID_NO { get; set; }
        public string CGENDER { get; set; }
        public string CSALESMAN_TYPE { get; set; }
        public string CEXT_COMPANY_NAME { get; set; }

 
    }
}
