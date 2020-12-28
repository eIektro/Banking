using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Connector.Banking.Exceptions
{
    public class RequestedClassNotFoundException : Exception
    {
        
        public RequestedClassNotFoundException(string message) : base (message)
        {
            
        }
    }
}
