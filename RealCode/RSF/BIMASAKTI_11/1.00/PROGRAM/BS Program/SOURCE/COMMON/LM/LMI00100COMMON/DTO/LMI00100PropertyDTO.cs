﻿using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMI00100Common.DTO
{
    public class LMI00100PropertyDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CPROPERTY_ID { get; set; }
    }
}
