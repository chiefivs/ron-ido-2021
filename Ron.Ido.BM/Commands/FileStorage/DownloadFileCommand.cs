using MediatR;
using Ron.Ido.Common.Interfaces;
using Ron.Ido.EM;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.FileStorage
{
    public class DownloadFileCommand: IRequest<byte[]>
    {
        public Guid FileUid { get; private set; }

        public DownloadFileCommand(Guid uid)
        {
            FileUid = uid;
        }
    }

    public class DownloadFileCommandHandler : IRequestHandler<DownloadFileCommand, byte[]>
    {
        private IFileStorageService _storage;

        public DownloadFileCommandHandler(IFileStorageService storage)
        {
            _storage = storage;
        }

        public Task<byte[]> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {
            var bytes = _storage.GetFileBytes(request.FileUid);

            if (bytes == null)
                return null;

            return Task.FromResult(bytes);
        }
    }
}
