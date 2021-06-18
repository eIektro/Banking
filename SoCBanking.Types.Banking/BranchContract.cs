using SoCBanking.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Types.Banking
{
    [Serializable]
    public partial class BranchContract : ContractBase
    {

        public BranchContract()
        {
                
        }

        private int? id;
        private string branchName;
        private int? cityId;
        private DateTime? dateOfLaunch;
        private string adress;
        private string phoneNumber;
        private string mailAdress;
        private string city;


        public int? Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged("Id");
            }

        }

        public string BranchName
        {
            get { return branchName; }
            set { branchName = value;
                OnPropertyChanged("BranchName");
            }

        }

        public int? CityId
        {
            get { return cityId; }
            set { cityId = value;
                OnPropertyChanged("CityId");
            }

        }

        public DateTime? DateOfLaunch
        {
            get { return dateOfLaunch; }
            set { dateOfLaunch = value;
                OnPropertyChanged("DateOfLaunch");
            }

        }

        public string Adress
        {
            get { return adress; }
            set { adress = value;
                OnPropertyChanged("Adress");
            }

        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }

        }

        public string MailAdress
        {
            get { return mailAdress; }
            set { mailAdress = value;
                OnPropertyChanged("MailAdress");
            }

        }

        public string City
        {
            get { return city; }
            set { city = value;
                OnPropertyChanged("City");
            }

        }


    }
}
