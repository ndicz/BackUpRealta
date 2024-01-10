using System.Collections.Generic;
using LMM03700Common.DTO;
using R_CommonFrontBackAPI;

namespace LMM03700Common
{
    public interface ILMM03700 : R_IServiceCRUDBase<LMM03700DTO>
    {
        IAsyncEnumerable<LMM03700DTO> GetTenantClasificationGroupStream();
        IAsyncEnumerable<LMM03700InitialProcessDTO> GetInitialProcessStream();
    }
}