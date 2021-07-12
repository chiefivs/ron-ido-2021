using Ron.Ido.BM.Interfaces;
using Ron.Ido.BM.Models.FileStorage;
using Ron.Ido.Common.DependencyInjection;
using Ron.Ido.Common.Interfaces;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using System;

namespace Ron.Ido.BM.Services
{
    public class FileStorageHelper: IDependency
    {
        private AppDbContext _context;
        private IFileStorageService _storage;
        private IIdentityService _identity;

        public FileStorageHelper(AppDbContext context, IFileStorageService storage, IIdentityService identity)
        {
            _context = context;
            _storage = storage;
            _identity = identity;
        }

        public FileInfo CreateFileInfo(FileInfoDto dto)
        {
            if (string.IsNullOrEmpty(dto.BytesBase64))
                return null;

            dto.Uid = _storage.CreateFile(Convert.FromBase64String(dto.BytesBase64));

            return _createFileInfo(new FileInfo
            {
                Uid = dto.Uid.Value,
                ContentType = dto.ContentType,
                Name = dto.Name,
                Size = dto.Size
            });
        }

        private FileInfo _createFileInfo(FileInfo fileinfo)
        {
            fileinfo.CreateTime = DateTime.Now;
            fileinfo.Source = "N";
            fileinfo.CreatedById = _identity?.Identity?.Id;
            _context.FileInfos.Add(fileinfo);
            _context.SaveChanges();

            return fileinfo;
        }
    }
}
