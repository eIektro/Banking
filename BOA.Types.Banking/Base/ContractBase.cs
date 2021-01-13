using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BOA.Types.Banking.Base
{
    public class ContractBase : INotifyPropertyChanged
    {

        //public bool MyProperty
        //{
        //    get => GetProperty<int>();
        //    set => SetProperty<int>(value);
        //}

        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public T GetProperty<T>([CallerMemberName] string propertyName = "")
        {
            EnsureElement<T>(propertyName);
            return (T)_values[propertyName];
        }

        
        public void SetProperty<T>(object value, [CallerMemberName] string propertyName = "")
        {
            EnsureElement<T>(propertyName);
            if (_values[propertyName] == value)
            {
                return;
            }
            _values[propertyName] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void EnsureElement<T>(string propertyName)
        {
            if (!_values.ContainsKey(propertyName))
            {
                _values.Add(propertyName, default(T));
            }
        }
    }
    
}
