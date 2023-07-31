using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO.Upload_DTO
{
    public class GSM00710UploadCashFlowSaveDTO
    {
        public Int32 NO { get; set; }
        public string CSEQ { get; set; }
        public string CCASHFLOW_GROUP_CODE { get; set; }
        public string CCASHFLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CCASHFLOW_TYPE { get; set; }
        public bool LEXIST { get; set; }
    }
}
