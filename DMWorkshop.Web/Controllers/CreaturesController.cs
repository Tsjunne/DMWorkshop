﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DMWorkshop.DTO.Characters;
using MediatR;
using System.Threading;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace DMWorkshop.Web.Controllers
{
    [Route("api/[controller]")]
    public class CreaturesController : Controller
    {
        private readonly IMediator _mediator;

        public CreaturesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task Register([FromBody] RegisterCreatureCommand command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }

        [HttpGet]
        public Task<IEnumerable<CreatureReadModel>> Find(FindCreaturesQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query ?? new FindCreaturesQuery(), cancellationToken);
        }

        [HttpGet("{name}")]
        public Task<CreatureReadModel> Get(GetCreatureQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query ?? new GetCreatureQuery(), cancellationToken);
        }

        [HttpGet("{name}/portrait")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 3600)]
        public async Task<IActionResult> GetImage(GetPortraitQuery query, CancellationToken cancellationToken)
        {
            return new FileStreamResult(await _mediator.Send(query ?? new GetPortraitQuery(), cancellationToken), "image/jpeg");
        }

        [HttpPost("{name}/portrait")]
        public async Task<IActionResult> PostImage(string name, IFormFile image, CancellationToken cancellationToken)
        {
            var command = new RegisterPortraitCommand
            {
                Name = name,
                Image = image.OpenReadStream()
            };

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}