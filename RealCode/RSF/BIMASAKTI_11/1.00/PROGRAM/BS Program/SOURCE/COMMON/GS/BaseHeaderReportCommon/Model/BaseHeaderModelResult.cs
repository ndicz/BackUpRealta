using BaseHeaderReportCommon.BaseHeader;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseHeaderReportCommon.Model
{
    public static class GenerateDataModelHeader
    {
        public static BaseHeaderResult DefaultData(BaseHeaderDTO poParam)
        {
            BaseHeaderResult loRtn = new BaseHeaderResult();
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "GSM Cash Flow Parameter",
                CUSER_ID = "HPC",
            };
            loRtn.BaseHeaderData = loParam;

            return loRtn;
        }
    }
}