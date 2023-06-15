using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace LMM02000Common.DTO
{
    public class LMM02000ListSalesmanTypeDTO : R_APIResultBaseDTO
    {
        public List<LMM02000SalesmanTypeDTO> Data { get; set; }
    }
}
 