using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM03700Common.DTO
{
    public class LMM03710DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID {get; set;}
        public string CUSER_ID {get; set;}
        public string CPROPERTY_ID {get; set;}
        public string CTENANT_CLASSIFICATION_GROUP_ID {get; set;}
        public string CTENANT_CLASSIFICATION_GROUP_NAME {get; set;}
        public string CTENANT_CLASSIFICATION_ID {get; set;}
        public string CTENANT_CLASSIFICATION_NAME {get; set;}
        public string CCREATE_BY {get; set;}
        public DateTime DCREATE_DATE {get; set;}
        public string CUPDATE_BY {get; set;}
        public DateTime DUPDATE_DATE {get; set;}
        
   
        public string CTENANT_ID {get; set;}
        public string CTENANT_NAME {get; set;}
        public string CEMAIL {get; set;}
        public string CADDRESS {get; set;}
        public string CTENANT_GROUP_ID {get; set;}
        public string CTENANT_GROUP_NAME {get; set;}
        public string CTENANT_CATEGORY_NAME {get; set;}
        public string CTENANT_TYPE_NAME {get; set;}
        public string CUNIT_NAME {get; set;}
        public string CPHONE1 {get; set;}
    }
    
    public class LMM03710ListDTO : R_APIResultBaseDTO
    {
        public List<LMM03710DTO> Data {get; set;}
    }
}