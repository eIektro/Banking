using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class CustomerJobContract : ContractBase
    {
        public int Id
        {
            get => GetProperty<int>();
            set => SetProperty<int>(value);
        }

        public string JobName
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string JobDescription
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }
    }
}
