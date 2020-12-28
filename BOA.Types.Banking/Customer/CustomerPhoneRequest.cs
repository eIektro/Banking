using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking.Customer
{
    public class CustomerPhoneRequest : RequestBase
    {
        public CustomerPhoneContract DataContract { get; set; }
    }
}
