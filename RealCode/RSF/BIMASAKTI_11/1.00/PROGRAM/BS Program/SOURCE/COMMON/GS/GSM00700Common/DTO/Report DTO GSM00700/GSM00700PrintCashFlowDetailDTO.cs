﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO.Report_DTO_GSM00700
{
    public class GSM0kj
    {
        public string CCASH_FLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CCASH_FLOW_TYPE { get; set; }
        public string CCASH_FLOW_TYPE_DESCR { get; set; }
        public string CYEAR {get; set; }
        public string CPERIOD_NO { get; set; }
        public Decimal NLOCAL_AMOUNT { get; set; }
        public Decimal NBASE_AMOUNT { get; set; }
    }
}