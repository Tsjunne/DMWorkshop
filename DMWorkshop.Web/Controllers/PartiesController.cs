using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DMWorkshop.DTO.Campaign;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DMWorkshop.Web.Controllers
{
    [Route("api/[controller]")]
    public class PartiesController : Controller
    {
        private readonly IMediator _mediator;

        public PartiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task Register([FromBody] RegisterPartyCommand command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }
    }
}