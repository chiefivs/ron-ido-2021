using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyRon
    {
        public ApplyRon()
        {
            ApplyRonAppeals = new HashSet<ApplyRonAppeal>();
            ApplyRonDocuments = new HashSet<ApplyRonDocument>();
            ApplyRonLetters = new HashSet<ApplyRonLetter>();
            ApplyRonStatusHistories = new HashSet<ApplyRonStatusHistory>();
        }

        public string BarCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreatorCountryId { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public string CreatorSurname { get; set; }
        public string CreatorMailIndex { get; set; }
        public string CreatorCityName { get; set; }
        public string CreatorStreet { get; set; }
        public string CreatorCorpus { get; set; }
        public string CreatorBuilding { get; set; }
        public string CreatorBlock { get; set; }
        public string CreatorFlat { get; set; }
        public string CreatorPhone { get; set; }
        public string CreatorEmail { get; set; }
        public int? DocCountryId { get; set; }
        public int? DocTypeId { get; set; }
        public string EduDocType { get; set; }
        public string DocBlankNum { get; set; }
        public string DocRegNum { get; set; }
        public string DocDate { get; set; }
        public int? DocDateYear { get; set; }
        public string DocFullName { get; set; }
        public string SchoolName { get; set; }
        public string Speciality { get; set; }
        public string Qualification { get; set; }
        public int StatusId { get; set; }
        public DateTime? StatusChangeTime { get; set; }
        public int? StatusChangeUserId { get; set; }

        public virtual Country CreatorCountry { get; set; }
        public virtual Country DocCountry { get; set; }
        public virtual ApplyDocType DocType { get; set; }
        public virtual ApplyRonStatus Status { get; set; }
        public virtual User StatusChangeUser { get; set; }
        public virtual ICollection<ApplyRonAppeal> ApplyRonAppeals { get; set; }
        public virtual ICollection<ApplyRonDocument> ApplyRonDocuments { get; set; }
        public virtual ICollection<ApplyRonLetter> ApplyRonLetters { get; set; }
        public virtual ICollection<ApplyRonStatusHistory> ApplyRonStatusHistories { get; set; }
    }
}
