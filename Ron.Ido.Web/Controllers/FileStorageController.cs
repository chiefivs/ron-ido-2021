﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.FileStorage;
using Ron.Ido.BM.Models.FileStorage;
using Ron.Ido.Common.Attributes;
using Ron.Ido.Web.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ron.Ido.Web.Controllers
{
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        private IMediator _mediator;

        public FileStorageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/files/download/{uid}")]
        [AuthorizedFor]
        public async Task<byte[]> GetBytes(Guid uid)
        {
            return await _mediator.Send(new DownloadFileCommand(uid));
        }
    }
}
