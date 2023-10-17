﻿using LMM03700Common.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03700Back
{
    public class MoveTenantDBParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CTENANT_CLASSIFICATION_GROUP_ID { get; set; }
        public string CFROM_TENANT_CLASSIFICATION_ID { get; set; }
        public string CTO_TENANT_CLASSIFICATION_ID { get; set; }
        public string CUSER_ID { get; set; }
        public List<string> LIST_CTENANT_ID { get; set; }
    }
}
