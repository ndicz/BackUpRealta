using R_APICommonDTO;

namespace LMM03700Common.DTO
{
    public class LMM03700InitialProcessDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
    }
}