using System;
using System.Collections.Generic;

namespace GSM04500Common.DTOs
{
    public class GSM04520DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_LOGIN_ID { get; set; }

        public string CJOURNAL_GRP_CODE { get; set; }
        public string CJOURNAL_GRP_NAME { get; set; }
        public string CJOURNAL_GROUP_TYPE { get; set; }
        
        public string CJRNGRP_CODE { get; set; }
        public string CJRNGRP_TYPE { get; set; }
        public string CGOA_CODE { get; set; }
        public string CGOA_NAME { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CGL_ACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CREATE_DATE { get; set; }
        
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; } = DateTime.Now;
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; } = DateTime.Now;
    }
    
    public class GSM04520ListDTO
    {
        public List<GSM04520DTO>  Data { get; set; }
    }
}