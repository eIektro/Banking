using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class CustomerJobContract
    {
        public int Id { get; set; }

        public string JobName { get; set; }

        public string JobDescription { get; set; }
    }
}
