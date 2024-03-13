using System.Collections.Generic;

namespace GSM05000Common.DTO
{
    public class GSM05000LimiterDTO
    {
        public string CCODE {get; set;}
        public string CDESCRIPTION {get; set;}
    }
    public class GSM05000LimiterListDTO
    {
        public List<GSM05000LimiterDTO> Data { get; set; }
    }
}