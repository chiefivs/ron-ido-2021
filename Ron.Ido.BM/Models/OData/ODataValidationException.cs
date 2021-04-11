using System;
using System.Collections.Generic;

namespace Ron.Ido.BM.Models.OData
{
    public class ODataValidationException: Exception
    {
        public Dictionary<string, List<string>> Errors { get; private set; }

        public ODataValidationException(Dictionary<string, List<string>> errors): base()
        {
            Errors = errors;
        }
    }
}
