using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Campaign
{
    public class DeletePartyCommand : IRequest
    {
        public string Name { get; set; }
    }
}
