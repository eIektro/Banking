using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class CityContract : ContractBase
    {
        public int id
        {
            get => GetProperty<int>();
            set => SetProperty<int>(value);
        }
        public string name
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string code
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

    }
}
