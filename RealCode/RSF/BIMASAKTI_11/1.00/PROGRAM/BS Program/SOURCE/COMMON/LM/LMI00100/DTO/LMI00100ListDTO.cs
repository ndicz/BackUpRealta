using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace LMI00100Common.DTO
{
    public class LMI00100ListDTO : R_APIResultBaseDTO
    {
        public List<LMI00100DTO> Data { get; set; }
    }
}
