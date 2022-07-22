using System;
using System.Globalization;
using Precision.Model;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace Precision.Converters
{
    [ValueConversion(typeof(Message.MessageOrigin), typeof(SolidColorBrush))]
    public class MsgOriginToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            var brush = new SolidColorBrush();

            if ((Message.MessageOrigin)value == Message.MessageOrigin.Incoming)
            {
                brush.Color = theme.PrimaryDark.Color;
                return brush;
            }
            else
            {
                brush.Color = theme.SecondaryDark.Color;
                return brush;
            }
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
