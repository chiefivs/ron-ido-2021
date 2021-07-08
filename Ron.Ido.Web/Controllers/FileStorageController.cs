﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.FileStorage;
using Ron.Ido.BM.Models.FileStorage;
using Ron.Ido.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ron.Ido.Web.Controllers
{
    [ApiController, NoCodegen]
    public class FileStorageController : ControllerBase
    {
        private IMediator _mediator;

        public FileStorageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/storage/upload")]
        public async Task<IEnumerable<FileInfoDto>> Upload()
        {
            return await _mediator.Send(new UploadFilesCommand(Request.Form.Files));
        }

        [HttpGet]
        [Route("api/storage/download/{uid}")]
        public async Task<ActionResult> Download(Guid uid)
        {
            var download = await _mediator.Send(new DownloadFileCommand(uid));
            if (download == null)
                return NotFound();

            return File(download.Bytes, download.ContentType, download.Name);
        }
    }
}
