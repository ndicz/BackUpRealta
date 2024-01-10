using System.Collections.Generic;
using LMM03700Common;
using LMM03700Common.DTO;
using R_BusinessObjectFront;

namespace LMM03700Model.Model
{
    public class LMM03700Model : R_BusinessObjectServiceClientBase<LMM03700DTO>, ILMM03700
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_CHECKPOINT_NAME = "api/LMM03700";
        private const string DEFAULT_MODULE = "LM";
        public LMM03700Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
        ) : base(
            pcHttpClientName,
            pcRequestServiceEndPoint,
            DEFAULT_MODULE,
            plSendWithContext,
            plSendWithToken)
        {
        }

        public IAsyncEnumerable<LMM03700DTO> GetTenantClasificationGroupStream()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<LMM03700InitialProcessDTO> GetInitialProcessStream()
        {
            throw new System.NotImplementedException();
        }
    }
}