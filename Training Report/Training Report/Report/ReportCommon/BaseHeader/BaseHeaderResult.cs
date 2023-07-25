using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon.BaseHeader
{
    public class BaseHeaderResult
    {
        public BaseHeaderDTO BaseHeaderData { get; set; }
    }
    public static class GenerateDataModel
    {
        public static BaseHeaderResult DefaultData()
        {
            BaseHeaderResult loRtn=new BaseHeaderResult()
            {
                BaseHeaderData=new BaseHeaderDTO()
                {
                    CompanyId = "C-01",
                    CompanyName = "Company 01",
                    UserId = "U-01",
                    UserName = "User 01"
                }
            };
            return loRtn;
        }
    }
}
