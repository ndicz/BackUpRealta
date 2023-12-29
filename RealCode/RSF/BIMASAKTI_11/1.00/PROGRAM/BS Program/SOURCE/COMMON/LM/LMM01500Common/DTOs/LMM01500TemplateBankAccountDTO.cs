using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM01500Common.DTOs
{
    public class LMM01500TemplateBankAccountDTO :  R_APIResultBaseDTO
    {
        
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
    
        public string CBANK_CODE { get; set; }
        public string CCB_NAME { get; set; }
        public string CBANK_ACCOUNT { get; set; }
        public string CCB_ACCOUNT_NAME { get; set; }
      
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        
        public string? CINVOICE_TEMPLATE { get; set; } = "";
        public string? CFileName { get; set; }
        public string? CFileExtension { get; set; }
        public string? CFileNameExtension { get; set; }
        public string CSTORAGE_ID { get; set; } = "";
        public bool LINVOICE_TEMPLATE { get; set; }
        public bool LBY_DEPARTMENT { get; set; }
        public Byte[]? OData { get; set; }
    }

    public class LMM01500TemplateBankAccountListDTO : R_APIResultBaseDTO
    {
        public List<LMM01500TemplateBankAccountDTO> Data { get; set; }
    }
}