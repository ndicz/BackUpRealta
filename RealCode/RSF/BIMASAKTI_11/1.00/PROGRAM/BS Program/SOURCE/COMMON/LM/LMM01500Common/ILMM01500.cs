using System.Collections.Generic;
using LMM01500Common.DTOs;
using R_CommonFrontBackAPI;

namespace LMM01500Common
{
    public interface ILMM01500 : R_IServiceCRUDBase<LMM01500GeneralInfoDTO>    
    {
        IAsyncEnumerable<LMM01500InitialProcessDTO> GetInitialProcessProperty();
        IAsyncEnumerable<LMM01500GeneralInfoDTO> GetInvoiceGroupList();
        // IAsyncEnumerable<LMM01500GeneralInfoDTO> GetInvoiceGroupDetail();
        LMM01500GeneralInfoDTO GetActiveInactive(LMM01500GeneralInfoDTO poParamDto);

    }
}