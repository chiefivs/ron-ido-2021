using System;

namespace Ron.Ido.Common.Interfaces
{
    public interface IFileInfo
    {
        Guid Uid { get; set; }
        string Name { get; set; }
        int Size { get; set; }
        string ContentType { get; set; }
    }
}
