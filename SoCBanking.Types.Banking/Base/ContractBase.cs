using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SoCBanking.Types.Banking.Base
{
    [Serializable]
    public abstract class ContractBase : INotifyPropertyChanged
    {
        public ContractBase()
        {
           
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged Method Declaration
        /// Auto Generated Documentation
        /// BOA method standard is described as below:
        /// Naming convention must be in Pascal-Case, naming standard must be in English and clear.
        /// TODO: More detail
        /// </summary>
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
 
    }
    
}
