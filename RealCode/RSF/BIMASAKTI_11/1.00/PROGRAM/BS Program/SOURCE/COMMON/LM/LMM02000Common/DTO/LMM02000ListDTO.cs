using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace LMM02000Common.DTO
{
    public class LMM02000ListDTO : R_APIResultBaseDTO
    {
        public List<LMM02000DTO> Data { get; set; }
    }
}
