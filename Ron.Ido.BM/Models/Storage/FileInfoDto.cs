using Ron.Ido.Common.Attributes;
using Ron.Ido.Common.Interfaces;
using System;

namespace Ron.Ido.BM.Models.Storage
{
    [TypeScriptModule("odata")]
    public class FileInfoDto
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }
    }
}