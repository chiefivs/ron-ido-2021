using Ron.Ido.BM.Models.FileStorage;
using System.Collections.Generic;

namespace Ron.Ido.BM.Models.Dossier
{
    public class ApplyAttachmentDto
    {
        public long Id { get; set; }

        public bool Required { get; set; }

        public bool Given { get; set; }

        public string Description { get; set; }

        public string Error { get; set; }

        public long? AttachmentTypeId { get; set; }

        public string AttachmentTypeName { get; set; }

        public IEnumerable<FileInfoDto> FileInfo{ get; set; }
    }
}
