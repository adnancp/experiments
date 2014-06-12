using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfApplication1.Utils
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan span = (TimeSpan)value;
            if (span == TimeSpan.MinValue)
            {
                return "0 seconds";
            }
            string formatted = string.Format("{0}{1}{2}{3}",
            span.Duration().Days > 0 ? string.Format("{0:0} Day{1}, ", span.Days, span.Days == 1 ? String.Empty : "s") : string.Empty,
            span.Duration().Hours > 0 ? string.Format("{0:0} Hour{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "s") : string.Empty,
            span.Duration().Minutes > 0 ? string.Format("{0:0} Minute{1}, ", span.Minutes, span.Minutes == 1 ? String.Empty : "s") : string.Empty,
            span.Duration().Seconds > 0 ? string.Format("{0:0} Second{1}", span.Seconds, span.Seconds == 1 ? String.Empty : "s") : string.Empty);
            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);
            if (string.IsNullOrEmpty(formatted)) formatted = "0 Seconds";
            return formatted;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            var parts = strValue.Split(' ');
            if (parts.Length != 2)
                return null;

            int val;
            if (!int.TryParse(parts[0], out val))
                return null;

            var measure = parts[1];
            
            switch (measure)
            {
                case "Second":
                case "Seconds":
                    return TimeSpan.FromSeconds(val);

                case "Minute":
                case "Minutes":
                    return TimeSpan.FromMinutes(val);
                case "Hour":
                case "Hours":
                    return TimeSpan.FromHours(val);
                case "Day":
                case "Days":
                    return TimeSpan.FromDays(val);
                default:
                    return null;
            }
        }
    }


}
