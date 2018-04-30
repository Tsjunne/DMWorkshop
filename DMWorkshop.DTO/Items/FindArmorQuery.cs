using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Items
{
    public class FindArmorQuery : IRequest<ArmorInfo[]>
    {
        public string Search { get; set; }
    }
}
