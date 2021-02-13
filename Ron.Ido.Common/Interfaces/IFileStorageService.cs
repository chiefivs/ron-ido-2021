using System;

namespace Ron.Ido.Common.Interfaces
{
    public interface IFileStorageService
    {
        string GetTempFilePath(string filename);
        IFileInfo CreateFile(byte[] data, string filename, string contentType);
        IFileInfo CreateTempFile(byte[] data, string filename, string contentType);
        IFileInfo SaveFile(Guid uid);
        Guid SaveFile(byte[] bytes);
        void DeleteFile(Guid uid);
        byte[] GetFileBytes(Guid uid);
        IFileInfo GetFileInfo(Guid uid);
    }
}
