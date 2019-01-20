using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DMWorkshop.DTO.Campaign;
using DMWorkshop.DTO.Characters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMWorkshop.Web.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private readonly IMediator _mediator;

        public PlayersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public Task Register([FromBody] RegisterPlayerCommand command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }

        [HttpGet]
        public Task<IEnumerable<CreatureReadModel>> Find(FindPartyQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query ?? new FindPartyQuery(), cancellationToken);
        }

        [HttpGet("{name}")]
        public Task<CreatureReadModel> Get(GetPlayerQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query ?? new GetPlayerQuery(), cancellationToken);
        }

        [HttpGet("{name}/portrait")]
        [ResponseCache(Duration = 3600)]
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

        [HttpDelete("{id}")]
        public Task Delete(string id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new DeletePlayerCommand { Name = id }, cancellationToken);
        }
    }
}