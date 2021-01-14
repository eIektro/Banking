using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class EducationLevelContract : ContractBase
    {
        public int Id
        {
            get => GetProperty<int>();
            set => SetProperty<int>(value);
        }

        public string EducationLevel
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string EducationLevelDescription
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }
    }
}
