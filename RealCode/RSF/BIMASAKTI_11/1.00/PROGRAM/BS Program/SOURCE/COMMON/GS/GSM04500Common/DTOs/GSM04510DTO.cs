using System;
using System.Collections.Generic;
using System.Globalization;

namespace GSM04500Common.DTOs
{
    public class GSM04510DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_LOGIN_ID { get; set; }
        public string CJOURNAL_GROUP_TYPE { get; set; }
        public string CJOURNAL_GROUP_CODE { get; set; }
        public string CGOA_NAME { get; set; }
        public string CGOA_CODE { get; set; }
        public string CJRNGRP_CODE { get; set; }
        public string CJRNGRP_TYPE { get; set; }
        public bool LDEPARTMENT_MODE { get; set; }
        
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME  { get; set; }
  
  
        
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; } = DateTime.Now;
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; } = DateTime.Now;
        private string _CGOA { get; set; }
        
        public string CGOA
        {
            get => _CGOA;
            set
            {
                _CGOA = $"{CGOA_NAME} ( {CGOA_CODE} )";
            }
        }
    }
    public class GSM04510ListDTO
    {
        public List<GSM04510DTO>  Data { get; set; }
    }
}