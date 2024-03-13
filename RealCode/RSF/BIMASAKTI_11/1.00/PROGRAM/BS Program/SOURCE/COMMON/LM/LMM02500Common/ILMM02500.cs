using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace LMM02500Common
{
    public interface ILMM02500 : R_IServiceCRUDBase<LMM02500DTO>
    {
        IAsyncEnumerable<LMM02500InitialProcessDTO> GetInitialProcessStream();
        IAsyncEnumerable<LMM02500DTO>GetTenantGroupListStream();
        
    }
}