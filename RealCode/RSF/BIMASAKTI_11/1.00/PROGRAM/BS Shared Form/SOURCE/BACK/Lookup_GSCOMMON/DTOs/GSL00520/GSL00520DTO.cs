﻿using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL00520DTO
    {
        // Param
        public string CCOMPANY_ID { get; set; }

        // result
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public char CBSIS { get; set; }
        public string CBSIS_DESCR { get; set; }
        public char CDBCR { get; set; }
        public string CDBCR_DESCR { get; set; }
        public string CCREATE_BY { get; set; }
        public bool LACTIVE { get; set; }
    }
}
