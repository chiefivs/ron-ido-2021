using Ron.Ido.Common.Interfaces;
using System;

namespace Ron.Ido.FileStorage
{
    [Serializable]
    public class FileMeta<TFileInfo> where TFileInfo : IFileInfo, new()
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string ContentType { get; set; }

        public FileMeta()
        {

        }

        public FileMeta(TFileInfo fileInfo)
        {
            Uid = fileInfo.Uid;
            Name = fileInfo.Name;
            Size = fileInfo.Size;
            ContentType = fileInfo.ContentType;
        }

        public TFileInfo GetFileInfo()
        {
            return new TFileInfo
            {
                Uid = Uid,
                Name = Name,
                Size = Size,
                ContentType = ContentType
            };
        }
    }
}
