﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Models.FileStorage;
using Ron.Ido.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}