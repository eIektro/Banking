using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Types.Banking
{
    public class LoginResponse : ResponseBase
    {
        public bool isLoggedIn { get; set; }
        public string UserName { get; set; }
    }
}
