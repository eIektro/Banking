using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class BranchContract
    {

        public int? Id { get; set; }
       
        public string BranchName { get; set; }
       
        public int? CityId { get; set; }

        public DateTime? DateOfLaunch { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public string MailAdress { get; set; }

        public string City { get; set; }

    }
}
