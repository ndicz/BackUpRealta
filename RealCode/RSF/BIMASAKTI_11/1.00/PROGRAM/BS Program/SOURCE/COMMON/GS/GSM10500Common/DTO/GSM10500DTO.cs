using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace GSM10500Common.DTO
{
    public class GSM10500DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; }= "";
        public string CAGEING_CODE {get; set;}= "";
        public string CAGEING_NAME {get; set;}= "";
        public string CROUNDING_MODE {get; set;}= "";
        
        public string CDESCRIPTION {get; set;}= "";
        public int IROUNDING {get; set;}
        public int IDIGIT_LENGTH {get; set;}
        public int IDECIMAL_POINT {get; set;}
        public string CCREATE_BY {get; set;}
        public DateTime DCREATE_DATE {get; set;}
        public string CUPDATE_BY {get; set;}
        public DateTime DUPDATE_DATE {get; set;} = DateTime.Now;
    }
    
    public class GSM10500ListDTO : R_APIResultBaseDTO
    {
        public List<GSM10500DTO> Data { get; set; }
    }
}