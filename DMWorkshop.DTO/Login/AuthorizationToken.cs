using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Login
{
    public class AuthorizationToken
    {
        public TokenInfo AccessToken { get; set; }
        public TokenInfo RefreshToken { get; set; }
    }
}
