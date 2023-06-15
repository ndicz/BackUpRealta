using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO
{
    public class GSM00710CashFlowTypeListDTO : R_APIResultBaseDTO
    {
        public List<GSM00710CashFlowTypeDTO> Data { get; set; }
    }
}
