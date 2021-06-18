using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Connector.Banking.Exceptions
{
    [Serializable]
    public class MethodNameNotFoundException : Exception
    {
        public MethodNameNotFoundException(string message) : base (message) { }
    }
}
