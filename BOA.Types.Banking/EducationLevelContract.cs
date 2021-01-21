using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    [Serializable]
    public partial class EducationLevelContract : ContractBase
    {
        public EducationLevelContract()
        {

        }

        private int id;
        private string educationLevel;
        private string educationLevelDescription;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public string EducationLevel
        {
            get { return educationLevel; }
            set { educationLevel = value; OnPropertyChanged("EducationLevel"); }
        }

        public string EducationLevelDescription
        {
            get { return educationLevelDescription; }
            set { educationLevelDescription = value; OnPropertyChanged("EducationLevelDescription"); }
        }
    }
}
