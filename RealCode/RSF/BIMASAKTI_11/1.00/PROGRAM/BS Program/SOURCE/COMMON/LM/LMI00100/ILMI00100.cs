using System;
using System.Collections.Generic;
using System.Text;
using LMI00100Common.DTO;
using R_CommonFrontBackAPI;

namespace LMI00100Common
{
    public interface ILMI00100 : R_IServiceCRUDBase<LMI00100DTO>
    {
        LMI00100ListDTO GetAllLMI00100List();
        LMI00100ListPropertyDTO GetLMI00100Property();
    }
}
