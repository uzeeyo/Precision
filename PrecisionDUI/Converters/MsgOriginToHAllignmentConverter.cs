using System;
using System.Globalization;
using Precision.Model;
using System.Windows;
using System.Windows.Data;

namespace Precision.Converters
{
    [ValueConversion(typeof(Message.MessageOrigin), typeof(HorizontalAlignment))]
    public class MsgOriginToHAllignmentConverter : IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {

            if ((Message.MessageOrigin)value == Message.MessageOrigin.Incoming)
            {
                return HorizontalAlignment.Left;
            }
            else return HorizontalAlignment.Right;
        }
        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
