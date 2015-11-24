using System;
using System.Globalization;
using System.Windows.Data;
using RuleSetEditor.ViewModels;

namespace RuleSetEditor.Views
{
    public sealed class ViewToCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ViewModelBase)
                if (((ViewModelBase)value).ViewStack is RuleSetViewModel)
                    return ((RuleSetViewModel)((ViewModelBase)value).ViewStack).IsOpen((Type)parameter);
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
