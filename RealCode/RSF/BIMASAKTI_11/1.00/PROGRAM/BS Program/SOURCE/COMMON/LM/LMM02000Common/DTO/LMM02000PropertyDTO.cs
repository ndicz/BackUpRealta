using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using R_APICommonDTO;

namespace LMM02000Common.DTO
{
    public class LMM02000PropertyDTO : R_APIResultBaseDTO
    { 
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CPROPERTY_ID { get; set; } = "";
    }
}
