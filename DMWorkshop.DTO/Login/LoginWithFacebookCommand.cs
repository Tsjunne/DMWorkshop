using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Login
{
    public class LoginWithFacebookCommand : IRequest<AuthorizationToken>
    {
        public string FacebookToken { get; set; }
    }
}
