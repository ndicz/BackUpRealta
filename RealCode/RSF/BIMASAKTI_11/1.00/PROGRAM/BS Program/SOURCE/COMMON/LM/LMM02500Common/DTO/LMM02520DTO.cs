using System;
using R_APICommonDTO;

namespace LMM02500Common
{
    public class LMM02520DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID {get; set;}
        public string CPROPERTY_ID {get; set;}
        public string CTENANT_ID {get; set;}
        public string CTENANT_NAME {get; set;}
        public string CPHONE1 {get; set;}
        public string CEMAIL {get; set;}
        public string CTENANT_GROUP_ID {get; set;}
        public string CTENANT_GROUP_NAME {get; set;}
        public string CCREATE_BY {get; set;}
        public DateTime DCREATE_DATE {get; set;}
        public string CUPDATE_BY {get; set;}
        public DateTime DUPDATE_DATE {get; set;}
    }
    
    public class LMM02520ListDTO : R_APIResultBaseDTO
    {
        public LMM02520DTO Data { get; set; }
    }
}