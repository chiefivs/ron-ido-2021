using Ron.Ido.Common.Attributes;
using System;

namespace Ron.Ido.BM.Models.FileStorage
{
    [TypeScriptModule("odata")]
    public class FileInfoDto
    {
        public Guid? Uid { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public string ContentType { get; set; }

        public string BytesBase64 { get; set; }
    }
}