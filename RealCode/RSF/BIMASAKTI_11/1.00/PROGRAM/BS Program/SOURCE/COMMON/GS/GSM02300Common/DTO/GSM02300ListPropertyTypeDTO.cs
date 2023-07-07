using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02300Common.DTO
{
    public class GSM02300ListPropertyTypeDTO : R_APIResultBaseDTO
    {
        public List<GSM02300PropertyTypeDTO> Data { get; set; }

    }
}
