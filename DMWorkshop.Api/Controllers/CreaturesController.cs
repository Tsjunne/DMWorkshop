﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DMWorkshop.DTO.Creatures;
using MediatR;
using System.Threading;

namespace DMWorkshop.Api.Controllers
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

        [HttpGet("{name}")]
        public Task<CreatureReadModel> Get(string name, CancellationToken cancellationToken)
        {
            var query = new GetCreatureQuery { Name = name };
            return _mediator.Send(query, cancellationToken);
        }
    }
}