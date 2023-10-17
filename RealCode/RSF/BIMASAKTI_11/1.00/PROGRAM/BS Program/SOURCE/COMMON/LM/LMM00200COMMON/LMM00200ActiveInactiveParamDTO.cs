using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM00200Common
{
    public class LMM00200ActiveInactiveParamDTO : R_APIResultBaseDTO
    {
    }

    public class LMM00200ActiveInactiveParam
    {
        public string CCOMPANY_ID { get; set; }
        public string CCODE { get; set; }
        public bool LACTIVE { get; set; }
        public string CUSER_ID { get; set; }
    }
}
