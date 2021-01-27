using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ReLinkSchoolToDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostIndex { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool CreatedFromApply { get; set; }
        public string CityName { get; set; }
        public string Shingle { get; set; }
        public int? TypeId { get; set; }
        public int? CountryId { get; set; }
    }
}
