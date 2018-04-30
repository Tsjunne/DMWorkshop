using DMWorkshop.DTO.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Items
{
    public class RegisterArmorCommand : ArmorInfo, IRequest
    {
    }
}
