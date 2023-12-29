using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM01500Common.DTOs
{
    public class LMM01500InitialProcessDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CPROPERTY_ID { get; set; } = "";
        
    }

    public class LMM01500InitialProcessListDTO : R_APIResultBaseDTO
    {
        public List<LMM01500InitialProcessDTO> Data { get; set; }
    }
}