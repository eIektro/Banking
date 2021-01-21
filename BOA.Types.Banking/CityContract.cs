using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    [Serializable]
    public partial class CityContract : ContractBase
    {
        public CityContract()
        {

        }

        private int id;
        private string name;
        private string code;

        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Code
        {
            get { return code; }
            set { code = value;
                OnPropertyChanged("Code");
            }
        }

    }
}
