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
    public class MonsterListsController : Controller
    {
        private readonly IMediator _mediator;

        public MonsterListsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task Register([FromBody] RegisterMonsterListCommand command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }

        [HttpGet]
        public Task<IEnumerable<CreatureListReadModel>> Find(GetMonsterListsQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query ?? new GetMonsterListsQuery(), cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task Delete(string id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new DeleteMonsterListCommand { Name = id }, cancellationToken);
        }
    }
}