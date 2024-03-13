using LMM02500Common;
using R_BusinessObjectFront;

namespace LMM02500Model.Model
{
    public class LMM02510Model : R_BusinessObjectServiceClientBase<LMM02510DTO>, ILMM02510
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM02510";
        private const string DEFAULT_MODULE = "LM";

        public LMM02510Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }
    }
}