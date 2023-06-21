using R_APICommonDTO;
using System;

namespace LMI00100Common.DTO
{
    public class LMI00100DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CBANK_CODE { get; set; }
        public string CBANK_NAME { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
