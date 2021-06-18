using SoCBanking.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Types.Banking
{
    [Serializable]
    public partial class CustomerContract : ContractBase //Domain class
    {
        public CustomerContract()
        {

        }

        private int? customerId;
        private string customerName;
        private string customerLastName;
        private string citizenshipId;
        private string motherName;
        private string fatherName;
        private string placeOfBirth;
        private int? jobId;
        private int? educationLvId;
        private int? branchId;
        private DateTime? dateOfBirth;
        private List<CustomerPhoneContract> phoneNumbers;
        private List<CustomerEmailContract> emails;
        private string jobName;
        private string educationLevelName;
        private string branchName;


        public int? CustomerId
        {
            get { return customerId; }
            set { customerId = value;
                OnPropertyChanged("CustomerId");
            }
            
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value;
                OnPropertyChanged("CustomerName");
            }
        }

        public string CustomerLastName
        {
            get { return customerLastName; }
            set { customerLastName = value;
                OnPropertyChanged("CustomerLastName");
            }

        }

        public string CitizenshipId
        {
            get { return citizenshipId; }
            set { citizenshipId = value;
                OnPropertyChanged("CitizenshipId");
            }

        }

        public string MotherName
        {
            get { return motherName; }
            set { motherName = value;
                OnPropertyChanged("MotherName");
            }

        }

        public string FatherName
        {
            get { return fatherName; }
            set { fatherName = value;
                OnPropertyChanged("FatherName");
            }

        }

        public string PlaceOfBirth
        {
            get { return placeOfBirth; }
            set { placeOfBirth = value;
                OnPropertyChanged("PlaceOfBirth");
            }

        }

        public int? JobId
        {
            get { return jobId; }
            set { jobId = value;
                OnPropertyChanged("jobId");
            }

        }

        public int? EducationLvId
        {
            get { return educationLvId; }
            set { educationLvId = value;
                OnPropertyChanged("EducationLvId");
            }

        }

        public int? BranchId
        {
            get { return branchId; }
            set { branchId = value;
                OnPropertyChanged("BranchId");
            }
        }
        public DateTime? DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }

        }

        public List<CustomerPhoneContract> PhoneNumbers
        {
            get { return phoneNumbers; }
            set { phoneNumbers = value;
                OnPropertyChanged("PhoneNumbers");
            }
        }

        public List<CustomerEmailContract> Emails
        {
            get { return emails; }
            set { emails = value;
                OnPropertyChanged("Emails");
            }
        }

        public string JobName
        {
            get { return jobName; }
            set { jobName = value;
                OnPropertyChanged("JobName");
            }

        }

        public string EducationLevelName
        {
            get { return educationLevelName; }
            set { educationLevelName = value;
                OnPropertyChanged("EducationLevelName");
            }

        }

        public string BranchName
        {
            get { return branchName; }
            set { branchName = value;
                OnPropertyChanged("BranchName");
            }
        }
    }
}
