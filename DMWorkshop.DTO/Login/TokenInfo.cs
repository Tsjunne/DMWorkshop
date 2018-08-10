using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Login
{
    public class TokenInfo
    {
        public string Token { get; set; }
        public long Expiry { get; set; }
    }
}
