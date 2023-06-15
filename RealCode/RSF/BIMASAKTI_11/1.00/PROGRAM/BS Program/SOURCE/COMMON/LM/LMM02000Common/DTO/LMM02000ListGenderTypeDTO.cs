using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM02000Common.DTO
{
    public class LMM02000ListGenderTypeDTO : R_APIResultBaseDTO
    {
        public List<LMM02000GenderTypeDTO> Data { get; set; }
    }
}
