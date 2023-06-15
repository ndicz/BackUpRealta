using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace LMM02000Common.DTO
{
    public class LMM02010ListDTO : R_APIResultBaseDTO
    {
        public List<LMM02010DTO> Data { get; set; }
    }
}
