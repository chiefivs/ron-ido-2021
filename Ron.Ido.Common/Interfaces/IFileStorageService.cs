using System;

namespace Ron.Ido.Common.Interfaces
{
    public interface IFileStorageService
    {
        string GetTempFilePath(string filename);
        Guid CreateFile(byte[] data);
        Guid CreateTempFile(byte[] data);
        void DeleteTempFile(Guid uid);
        void DeleteFile(Guid uid);
        byte[] GetFileBytes(Guid uid);
    }
}
