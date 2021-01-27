using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Country
    {
        public Country()
        {
            ApplyCreatorCitizenships = new HashSet<Apply>();
            ApplyCreatorCountries = new HashSet<Apply>();
            ApplyDocCountries = new HashSet<Apply>();
            ApplyOwnerCitizenships = new HashSet<Apply>();
            ApplyOwnerCountries = new HashSet<Apply>();
            ApplyRonCreatorCountries = new HashSet<ApplyRon>();
            ApplyRonDocCountries = new HashSet<ApplyRon>();
            ApplySchoolCountries = new HashSet<Apply>();
            ConclusionCreatorCitizenships = new HashSet<Conclusion>();
            ConclusionSchoolCountries = new HashSet<Conclusion>();
            ConfirmedSchoolNames = new HashSet<ConfirmedSchoolName>();
            DuplicateCreatorCountries = new HashSet<Duplicate>();
            DuplicateDocCountryNavigations = new HashSet<Duplicate>();
            Foundations = new HashSet<Foundation>();
            Ratifications = new HashSet<Ratification>();
            Schools = new HashSet<School>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string A2code { get; set; }
        public string A3code { get; set; }
        public string IsgaCode { get; set; }
        public bool? LegalizationNeeded { get; set; }
        public string LegalizationComment { get; set; }
        public int OrderNum { get; set; }
        public int? RegionId { get; set; }
        public int? LegalizationId { get; set; }
        public double? CoordX { get; set; }
        public double? CoordY { get; set; }
        public int? EiisCode { get; set; }
        public string NameEng { get; set; }

        public virtual Legalization Legalization { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Apply> ApplyCreatorCitizenships { get; set; }
        public virtual ICollection<Apply> ApplyCreatorCountries { get; set; }
        public virtual ICollection<Apply> ApplyDocCountries { get; set; }
        public virtual ICollection<Apply> ApplyOwnerCitizenships { get; set; }
        public virtual ICollection<Apply> ApplyOwnerCountries { get; set; }
        public virtual ICollection<ApplyRon> ApplyRonCreatorCountries { get; set; }
        public virtual ICollection<ApplyRon> ApplyRonDocCountries { get; set; }
        public virtual ICollection<Apply> ApplySchoolCountries { get; set; }
        public virtual ICollection<Conclusion> ConclusionCreatorCitizenships { get; set; }
        public virtual ICollection<Conclusion> ConclusionSchoolCountries { get; set; }
        public virtual ICollection<ConfirmedSchoolName> ConfirmedSchoolNames { get; set; }
        public virtual ICollection<Duplicate> DuplicateCreatorCountries { get; set; }
        public virtual ICollection<Duplicate> DuplicateDocCountryNavigations { get; set; }
        public virtual ICollection<Foundation> Foundations { get; set; }
        public virtual ICollection<Ratification> Ratifications { get; set; }
        public virtual ICollection<School> Schools { get; set; }
    }
}
