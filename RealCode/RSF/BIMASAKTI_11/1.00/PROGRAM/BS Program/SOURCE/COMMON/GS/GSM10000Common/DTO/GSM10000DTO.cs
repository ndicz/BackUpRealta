using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace GSM10000Common.DTO
{
    public class GSM10000DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CHOLIDAY_DATE { get; set; } 
        public bool LACTIVE { get; set; }
        public string CHOLIDAY_NAME { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }

        public DateTime DHOLIDAY_DATE { get; set; } = DateTime.Now;
    }

    public class GSM10000ListDTO : R_APIResultBaseDTO
    {
        public List<GSM10000DTO> Data { get; set; }
    }
}