using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM02500Common
{
    public class LMM02500InitialProcessDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID {get; set;}
        public string CUSER_ID {get; set;}
        public string CPROPERTY_ID {get; set;}
        public string CPROPERTY_NAME {get; set;}
    }

    public class LMM02500InitialProcessListDTO : R_APIResultBaseDTO
    {
        public List<LMM02500InitialProcessDTO> Data { get; set; }
    }
}