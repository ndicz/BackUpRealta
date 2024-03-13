using System.Collections.Generic;
using R_APICommonDTO;

namespace GSM05000Common.DTO
{
    public class GSM05000GridDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANS_NAME { get; set; }
        public string CMODULE_NAME { get; set; }
        
        public string CTRANSACTION { get => CTRANS_NAME + " (" + CTRANS_CODE + ")"; }
        public string MODULE { get => CMODULE_NAME; }
    }

    public class GSM05000GridListDTO : R_APIResultBaseDTO
    {
        public List<GSM05000GridDTO> Data { get; set; }
    }
}