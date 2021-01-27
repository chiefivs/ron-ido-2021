using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyRonLetterDest
    {
        public ApplyRonLetterDest()
        {
            ApplyRonLetters = new HashSet<ApplyRonLetter>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<ApplyRonLetter> ApplyRonLetters { get; set; }
    }
}
