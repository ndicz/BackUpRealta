using System;
using System.Collections.Generic;
using System.Text;
using LMM02000Common.DTO;
using R_CommonFrontBackAPI;

namespace LMM02000Common
{
    public interface ILMM02000 : R_IServiceCRUDBase<LMM02000DTO>

    {
        IAsyncEnumerable<LMM02000DTO> GetAllLMM02000Stream();
        LMM02000ListDTO GetAllLMM02000List();

        LMM02010ListDTO GetAllLMM02010List();

        LMM02000ListPropertyDTO GetLMM02000Property();

        IAsyncEnumerable<LMM02000PropertyDTO> GetAllLMM02000PropertyStream();

        LMM02000ListGenderTypeDTO GetGender();

        LMM02000ListSalesmanTypeDTO GetSalesmanType();

        LMM02000ActiveInactiveDTO GetActiveInactive(LMM02000ActiveInactiveParam poParamDto);

        LMM02000Template GetTemplate();
    }
}
