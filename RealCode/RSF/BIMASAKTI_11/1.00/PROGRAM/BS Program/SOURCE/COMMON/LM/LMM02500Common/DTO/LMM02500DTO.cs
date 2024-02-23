using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM02500Common
{
    public class LMM02500DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID {get; set;}
        public string CPROPERTY_ID {get; set;} 
        public string CUSER_ID {get; set;}
        public string CTENANT_GROUP_ID {get; set;}
        public string CTENANT_GROUP_NAME {get; set;}
        public string CADDRESS {get; set;}
        public string CEMAIL {get; set;}
        public string CPHONE1 {get; set;}
        public string CPHONE2 {get; set;}
        public string CPIC_NAME {get; set;}
        public string CPIC_POSITION {get; set;}
        public string CPIC_EMAIL {get; set;}
        public string CPIC_MOBILE_PHONE1 {get; set;}
        public string CPIC_MOBILE_PHOpublic {get; set;} 
        public string CCREATE_BY {get; set;}
        public DateTime DCREATE_DATE {get; set;}
        public string CUPDATE_BY {get; set;}
        public DateTime DUPDATE_DATE {get; set;}
    }

    public class LMM02500ListDTO : R_APIResultBaseDTO
    {
        public List<LMM02500DTO> Data { get; set; }
    }
}