using LMM00200Common.DTO_s;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM00200Common
{
    public interface ILMM00200 : R_IServiceCRUDBase<LMM00200DTO>
    {
        IAsyncEnumerable<LMM00200StreamDTO> GetUserParamList();
        LMM00200ActiveInactiveParamDTO GetActiveParam();
    }
}
