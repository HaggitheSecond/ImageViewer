using System;
using System.Globalization;
using System.Windows.Data;

namespace ImageViewer.Converters
{
    public class StringTruncateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            var lenghtString = parameter as string;

            if (text == null || lenghtString == null)
                return null;

            if (int.TryParse(lenghtString, out int lenght) == false)
                return null;

            if (text.Length <= lenght)
                return text;

            text = text.Remove(lenght, text.Length - lenght);
            text += "...";

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}