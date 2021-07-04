using MediatR;
using Microsoft.AspNetCore.Http;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Models.FileStorage
{
    public class UploadFilesCommand: IRequest<IEnumerable<FileInfoDto>>
    {
        public IEnumerable<IFormFile> Files { get; private set; }
        public bool CreateImmediatelly { get; set; }

        public UploadFilesCommand(IEnumerable<IFormFile> files, bool createImmediatelly = false)
        {
            Files = files;
            CreateImmediatelly = createImmediatelly;
        }

        public class UploadFilesCommandHandler : IRequestHandler<UploadFilesCommand, IEnumerable<FileInfoDto>>
        {
            private IFileStorageService _storage;
            private FileStorageHelper _helper;

            public UploadFilesCommandHandler(IFileStorageService storage, FileStorageHelper helper)
            {
                _storage = storage;
                _helper = helper;
            }

            public Task<IEnumerable<FileInfoDto>> Handle(UploadFilesCommand request, CancellationToken cancellationToken)
            {
                var list = new List<FileInfoDto>();

                foreach (var file in request.Files)
                {
                    var bytes = new byte[file.Length];
                    file.OpenReadStream().Read(bytes, 0, (int)file.Length);
                    IFileInfo fileInfo;
                    if (request.CreateImmediatelly)
                    {
                        fileInfo = _storage.CreateFile(bytes, Path.GetFileName(file.FileName), file.ContentType);
                        _helper.CreateFileInfo(fileInfo);
                    }
                    else
                    {
                        fileInfo = _storage.CreateTempFile(bytes, Path.GetFileName(file.FileName), file.ContentType);
                    }

                    list.Add(new FileInfoDto { 
                        Uid = fileInfo.Uid,
                        Name = fileInfo.Name,
                        Size = fileInfo.Size,
                        ContentType = fileInfo.ContentType
                    });
                }

                return Task.FromResult(list.AsEnumerable());
            }
        }

    }
}
