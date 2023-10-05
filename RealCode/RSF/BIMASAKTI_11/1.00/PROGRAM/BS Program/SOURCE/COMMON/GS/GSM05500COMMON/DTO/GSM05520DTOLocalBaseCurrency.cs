using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM05500Common.DTO
{
    public class GSM05520DTOLocalBaseCurrency : R_APIResultBaseDTO
    {
        public string CLOCAL_CURRENCY_CODE { get; set; }
        public string CLOCAL_CURRENCY_NAME {get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }
        public string CBASE_CURRENCY_NAME { get; set; }
    }

    public class GSM00520DTOLocalBaseListCurrency : R_APIResultBaseDTO
    {
        public List<GSM05520DTOLocalBaseCurrency> Data { get; set; }
    }

}
