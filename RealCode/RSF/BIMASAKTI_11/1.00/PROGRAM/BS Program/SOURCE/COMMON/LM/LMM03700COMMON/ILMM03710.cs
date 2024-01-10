using System.Collections.Generic;
using LMM03700Common.DTO;
using R_CommonFrontBackAPI;

namespace LMM03700Common
{
    public interface ILMM03710 : R_IServiceCRUDBase<LMM03710DTO>
    {
        IAsyncEnumerable<LMM03710DTO> GetTenanClasificationGroupListStream();
        IAsyncEnumerable<LMM03710DTO> GetTenanClasificationListStream();
        IAsyncEnumerable<LMM03710DTO> GetTenantListStream();

        IAsyncEnumerable<LMM03710DTO> AssignAvailableTenantListStream();
        IAsyncEnumerable<LMM03710DTO> AssignSelectedTenantListStream();


        IAsyncEnumerable<LMM03710DTO> MoveTenantDropownStream();
        IAsyncEnumerable<LMM03710DTO> MoveGetTenantListStream();

        LMM03700Dump AssignTenant(LMM03700ParamDTO poParam);
        LMM03700Dump MoveTenant(LMM03700ParamDTO poParam);
    }
}