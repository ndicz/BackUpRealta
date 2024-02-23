using System;
using R_APICommonDTO;

namespace GSM05000Common.DTO
{
    public class GSM05000SingleDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
        public bool LDEPT_MODE { get; set; }
        public string CDEPT_DELIMITER { get; set; }
        public bool LTRANSACTION_MODE { get; set; }
        public string CTRANSACTION_DELIMITER { get; set; }
        public string CPERIOD_MODE { get; set; }
        public string CPERIOD_DELIMITER { get; set; }
        public string CYEAR_FORMAT { get; set; }
        public int INUMBER_LENGTH { get; set; }
        public string CNUMBER_DELIMITER { get; set; }
        public string CPREFIX_DELIMITER { get; set; }
        public string CSUFFIX { get; set; }
        public string CSQUENCE01 { get; set; }
        public string CSQUENCE02 { get; set; }
        public string CSQUENCE03 { get; set; }
        public string CSQUENCE04 { get; set; }
        public bool LAPPROVAL_FLAG { get; set; }
        public bool LUSE_THIRD_PARTY { get; set; }
        public string CAPPROVAL_MODE { get; set; }
        public bool LAPPROVAL_DEPT { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }

}