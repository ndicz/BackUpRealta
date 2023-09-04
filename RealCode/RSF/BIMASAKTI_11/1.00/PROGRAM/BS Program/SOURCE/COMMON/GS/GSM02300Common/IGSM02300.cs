using System;
using System.Collections.Generic;
using System.Text;
using GSM02300Common.DTO;
using R_CommonFrontBackAPI;

namespace GSM02300Common
{
    public interface IGSM02300 : R_IServiceCRUDBase<GSM02300DTO>
    {
        //GSM02300ListDTO GetAllProperty();
        //GSM02300ListPropertyTypeDTO GetPropertyType();

        IAsyncEnumerable<GSM02300DTO> GetAllPropertyStream();
        IAsyncEnumerable<GSM02300PropertyTypeDTO> GetPropertyTypeStream();
    }
}
