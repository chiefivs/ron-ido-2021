using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Models.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.Ido.Web.Controllers
{
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        [HttpPost]
        [Route("api/storage/upload")]
        public async Task<IEnumerable<FileInfoDto>> Upload()
        {
            //            var result = await _mediator.Send(new UploadCommand(Request.Form.Files));
            var file = Request.Form.Files.First();

            var bytes = new byte[file.Length];
            file.OpenReadStream().Read(bytes, 0, (int)file.Length);
            return new FileInfoDto[] {
                new FileInfoDto
                {
                    Name = file.FileName,
                    Size = (int)file.Length,
                    Uid = Guid.NewGuid()
                }
            };
        }
    }
}
