using R_APICommonDTO;

namespace GSM10000Common.DTO
{
    public class GSM10000ActiveInactiveDTO : R_APIResultBaseDTO
    {
        
    }
    public class LMM02000ActiveInactiveParam
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CHOLIDAY_DATE { get; set; }
        public bool LACTIVE { get; set; }
    }
}