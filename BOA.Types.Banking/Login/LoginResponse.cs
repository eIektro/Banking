using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class LoginResponse : ResponseBase
    {
        public bool isLoggedIn { get; set; }
        public string UserName { get; set; }
    }
}
