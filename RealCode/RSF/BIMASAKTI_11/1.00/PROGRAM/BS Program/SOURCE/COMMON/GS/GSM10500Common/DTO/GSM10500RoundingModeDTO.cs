using System.Collections.Generic;
using R_APICommonDTO;

namespace GSM10500Common.DTO
{
    public class GSM10500RoundingModeDTO : R_APIResultBaseDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }
    public class GSM10500RoundingModeListDTO :R_APIResultBaseDTO
    {
        public List<GSM10500RoundingModeDTO> Data { get; set; }
    }
}