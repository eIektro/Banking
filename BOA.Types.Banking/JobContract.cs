using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    [Serializable]
    public partial class JobContract : ContractBase
    {
        public JobContract()
        {

        }

        private int id;
        private string jobName;
        private string jobDescription;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public string JobName
        {
            get { return jobName; }
            set { jobName = value; OnPropertyChanged("JobName"); }
        }

        public string JobDescription
        {
            get { return jobDescription; }
            set { jobDescription = value; OnPropertyChanged("JobDescription"); }
        }
    }
}
