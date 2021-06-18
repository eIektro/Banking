using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Business.Banking.Exceptions
{
    [Serializable]
    public class FalseConnectionStringException : Exception
    {
        public FalseConnectionStringException(string message) : base(message)
        {

        }
    }
}
