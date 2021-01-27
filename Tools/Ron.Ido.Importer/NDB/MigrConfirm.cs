using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class MigrConfirm
    {
        public string Country { get; set; }
        public decimal? CountryId { get; set; }
        public string School { get; set; }
        public decimal? SchoolId { get; set; }
        public string Docs { get; set; }
        public decimal? DocsId { get; set; }
        public decimal? Order { get; set; }
        public string Equivalent { get; set; }
    }
}
