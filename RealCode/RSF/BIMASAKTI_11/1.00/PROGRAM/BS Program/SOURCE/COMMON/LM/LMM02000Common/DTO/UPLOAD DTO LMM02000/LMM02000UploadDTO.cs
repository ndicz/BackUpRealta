using System;
using System.Collections.Generic;
using System.Text;

namespace LMM02000Common.DTO.UPLOAD_DTO_LMM02000
{
    public class LMM02000UploadDTO
    {
        public int NO { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string SalesmanId { get; set; }
        public string SalesmanName { get; set; }
        public bool Active { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string NIK { get; set; }
        public string Gender { get; set; }
        public string SalesmanType { get; set; }
        public string CompanyName { get; set; }
        public string NonActiveDate { get; set; }
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }
    }

    public class LMM02000UploadExcelDTO

    {
        public string SalesmanId { get; set; } = " ";
        public string SalesmanName { get; set; } = " ";
        public bool Active { get; set; }
        public string Address { get; set; } = " ";
        public string EmailAddress { get; set; } = " ";
        public string MobileNo1 { get; set; } = "";
        public string MobileNo2 { get; set; } = "0";
        public string NIK { get; set; } = " ";
        public string Gender { get; set; } = " ";
        public string SalesmanType { get; set; } = " ";
        public string CompanyName { get; set; } = " ";
        public string NonActiveDate { get; set; } = " ";
        public string Notes { get; set; } = " ";
        public string ErrorMessage { get; set; } = " ";
    }



    public class LMM02000UploadRequestDTO
    {
        public int NO { get; set; }
        public string SalesmanId { get; set; }
        public string SalesmanName { get; set; }
        public bool Active { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string NIK { get; set; }
        public string Gender { get; set; }
        public string SalesmanType { get; set; }
        public string CompanyName { get; set; }
        public string NonActiveDate { get; set; }
    }

    public class LMM02000UploadErrorValidateDTO
    {
        public int NO { get; set; }
        public string SalesmanId { get; set; }
        public string SalesmanName { get; set; }
        public bool Active { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo1 { get; set; } = "0";
        public string MobileNo2 { get; set; } = "0";
        public string NIK { get; set; }
        public string Gender { get; set; }
        public string SalesmanType { get; set; }
        public string CompanyName { get; set; }
        public string NonActiveDate { get; set; }
        public string ErrorMessage { get; set; }
        public bool ErrorFlag { get; set; }
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }
    }

    public class LMM02000UploadErrorValidateParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CKEY_GUID { get; set; }
    }
}
