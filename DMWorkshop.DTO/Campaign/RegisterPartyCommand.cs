﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Campaign
{
    public class RegisterPartyCommand : IRequest
    {
        public string Name { get; set; }
        public IEnumerable<string> Members { get; set; } = new string[] { };
    }
}
