using System.Runtime.InteropServices;
using Lookup_APCOMMON;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00400;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;

namespace Lookup_APSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupController : ControllerBase, IPublicLookup

    {
        [HttpPost]
        public IAsyncEnumerable<APL00100DTO> APL00100SupplierLookUp()
        {
            var loException = new R_Exception();
            IAsyncEnumerable<APL00100DTO> loRtn = null;
        }
        [HttpPost]
        public IAsyncEnumerable<APL00110DTO> APL00110SupplierInfoLookUp()
        {
            throw new NotImplementedException();
            
        }
        [HttpPost]
        public IAsyncEnumerable<APL00200DTO> APL00200ExpenditureLookUp()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IAsyncEnumerable<APL00300DTO> APL00300ProductLookUp()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IAsyncEnumerable<APL00400DTO> APL00400ProductAllocationLookUp()
        {
            throw new NotImplementedException();
        }
        
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }
    }
}