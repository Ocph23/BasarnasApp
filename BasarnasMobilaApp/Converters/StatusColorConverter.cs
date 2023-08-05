using BasarnasApp.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasarnasMobilaApp.Converters
{
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (StatusLaporan)value;
            return data switch
            {
                StatusLaporan.Baru=> Color.FromHex("#577590"),
                StatusLaporan.Batal=> Color.FromHex("#f94144"),
                StatusLaporan.Selesai => Color.FromHex("#43aa8b"),
                StatusLaporan.Menunggu=> Color.FromHex("#f8961e"),
                StatusLaporan.Ditangani=> Colors.GreenYellow,
                _ => Color.FromHex("#277da1"),
            };


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
