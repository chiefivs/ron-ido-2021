using MediatR;
using Ron.Ido.BM.Models.FileStorage;
using Ron.Ido.Common.Interfaces;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.FileStorage
{
    public class DownloadFileCommand: IRequest<DownloadFileData>
    {
        public Guid FileUid { get; private set; }

        public DownloadFileCommand(Guid uid)
        {
            FileUid = uid;
        }
    }

    public class DownloadFileCommandHandler : IRequestHandler<DownloadFileCommand, DownloadFileData>
    {
        private AppDbContext _dbcontext;
        private IFileStorageService _storage;

        public DownloadFileCommandHandler(AppDbContext dbcontext, IFileStorageService storage)
        {
            _dbcontext = dbcontext;
            _storage = storage;
        }

        public Task<DownloadFileData> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {
            var fileinfo = _dbcontext.Find<FileInfo>(request.FileUid) ?? _storage.GetFileInfo(request.FileUid);
            var bytes = _storage.GetFileBytes(request.FileUid);

            if (fileinfo == null || bytes == null)
                return null;

            return Task.FromResult(new DownloadFileData
            {
                Name = fileinfo.Name,
                ContentType = fileinfo.ContentType,
                Bytes = bytes
            });
        }
    }
}
