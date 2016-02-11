using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RuleSetEditor.Common
{
    public class GenericValue<TValue> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TValue value;

        public TValue Value
        {
            get
            {
                return value;
            }
            set
            {
                RaiseSetIfChanged(ref this.value, value);
            }
        }

        public GenericValue(TValue initialValue)
        {
            value = initialValue;
        }

        protected bool RaiseSetIfChanged(ref TValue field, TValue value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}
