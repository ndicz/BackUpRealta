using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM02000Common.DTO 
{
    public class LMM02000ActiveInactiveDTO : R_APIResultBaseDTO

    
    {
   
    }

    public class LMM02000ActiveInactiveParam
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public bool LACTIVE { get; set; }
        public string CSALESMAN_ID { get; set; }
    }

}
