using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class CustomerContract //Domain class
    {
        public int? CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerLastName { get; set; }

        public string CitizenshipId { get; set; }

        public string MotherName { get; set; }

        public string FatherName { get; set; }

        public string PlaceOfBirth { get; set; }

        public int?  JobId { get; set; }

        public int? EducationLvId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<CustomerPhoneContract> PhoneNumbers { get; set; }

        public List<CustomerEmailContract> Emails { get; set; }
    }
}
