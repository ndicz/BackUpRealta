﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO.Upload_DTO_GSM00720
{
    public class GSM00720UploadCashFlowPlanErrorDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCASH_FLOW_GROUP_CODE { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public decimal NLOCAL_AMOUNT { get; set; }
        public decimal NBASE_AMOUNT { get; set; }
        public bool LEXIST { get; set; }
        public string ErrorMessage { get; set; }
    }
}
