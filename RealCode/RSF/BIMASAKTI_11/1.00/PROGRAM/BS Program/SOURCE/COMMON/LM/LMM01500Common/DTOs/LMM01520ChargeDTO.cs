using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM01500Common.DTOs
{
    public class LMM01520ChargeDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_CODE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }

    public class LMM01520ChargeListDTO : R_APIResultBaseDTO
    {
        public List<LMM01520ChargeDTO> Data { get; set; }
    }
}