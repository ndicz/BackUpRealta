using System;
using System.Collections.Generic;
using System.Data.Common;
using R_APICommonDTO;

namespace GSM10500Common.DTO
{
    public class GSM10510DTO
    {
        public string CCOMPANY_ID {get; set;}
        public string CAGEING_CODE {get; set;} = "";
        public string CAGEING_NAME {get; set;}= "";
        public int ICOLUMN_NO {get; set;}
        public string CDESCRIPTION {get; set;}= "";
        public int IFROM_DAYS {get; set;}
        public int ITO_DAYS {get; set;}
        public string CCREATE_BY {get; set;}
        public DateTime DCREATE_DATE {get; set;}
        public string CUPDATE_BY {get; set;}
        public DateTime DUPDATE_DATE {get; set;}
        public string CUSER_ID { get; set; }
    }
    public class GSM10510ListDTO : R_APIResultBaseDTO
    {
        public List<GSM10510DTO> Data { get; set; }
    }
}