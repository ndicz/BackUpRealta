using System;
using System.Collections.Generic;
using System.Text;

namespace LMM02000Common.DTO.UPLOAD_DTO_LMM02000
{
    public class LMM02000UploadSalesmanDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string SalesmanId { get; set; }
        public string SalesmanName { get; set; }
        public string Active { get; set; }
        public string NonActiveDate { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string NIK { get; set; }
        public string Gender { get; set; }
        public string SalesmanType { get; set; }
        public string CompanyName { get; set; }
        public bool LEXIST { get; set; }
        public bool LOVERWRITE { get; set; }
    }
}
