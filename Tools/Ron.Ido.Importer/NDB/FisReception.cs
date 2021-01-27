using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class FisReception
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PassportType { get; set; }
        public string PassportSer { get; set; }
        public string PassportNum { get; set; }
        public DateTime? PassportDate { get; set; }
        public string DepartCode { get; set; }
        public string PassportGivenBy { get; set; }
        public string Citizenship { get; set; }
        public string DocSer { get; set; }
        public string DocNum { get; set; }
        public DateTime? DocDate { get; set; }
        public string DocOrg { get; set; }
        public string ToOrg { get; set; }
        public string LearnLevel { get; set; }
        public string LearnDepart { get; set; }
        public string LearnPrograms { get; set; }
        public string ForControlCount { get; set; }
        public string ForSpecialQuota { get; set; }
        public string Act { get; set; }
        public string SvidNum { get; set; }
        public DateTime? SvidDate { get; set; }
        public string BlankNum { get; set; }
        public int? ConclusionId { get; set; }
        public int? ResultType { get; set; }
    }
}
