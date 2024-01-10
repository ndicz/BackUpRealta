﻿using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM03700Common.DTO
{
    public class LMM03700DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID {get; set;}
        public string CPROPERTY_ID {get; set;}
        public string CUSER_ID {get; set;}
        public string CTENANT_CLASSIFICATION_GROUP_ID {get; set;}
        public string CTENANT_CLASSIFICATION_GROUP_NAME {get; set;}
        public string CCREATE_BY {get; set;}
        public DateTime DCREATE_DATE {get; set;}
        public string CUPDATE_BY {get; set;}
        public DateTime DUPDATE_DATE {get; set;}
    }
    
    public class LMM03700ListDTO : R_APIResultBaseDTO
    {
        public List<LMM03700DTO> Data {get; set;}
    }
}