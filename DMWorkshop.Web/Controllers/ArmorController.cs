using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DMWorkshop.DTO.Items;

namespace DMWorkshop.Web.Controllers
{
    [Route("api/[controller]")]
    public class ArmorController : Controller
    {
        private readonly IMediator _mediator;

        public ArmorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task Register([FromBody] RegisterArmorCommand command)
        {
            return _mediator.Send(command);
        }
    }
}