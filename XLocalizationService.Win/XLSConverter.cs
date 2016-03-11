using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace XLocalizationService.Converter
{
    public class XLSConverter : MarkupExtension, IValueConverter
    {
        private LocalizationService ls;
        private static XLSConverter _converter = null;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
            {
                _converter = new XLSConverter();
            }
            return _converter;
        }


        public XLSConverter() { }

        public XLSConverter(LocalizationService ls)
        {
            this.ls = ls;
        }



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;

            if (string.IsNullOrEmpty(parameterString))
                return string.Empty;

            else {
                string[] parameters = parameterString.Split('|');
                string r = string.Empty;

                if (ls == null)
                {
                   
                    if (parameters.Count() == 1)
                        r = LocalizationService.LS.GetValue(parameters[0]);
                    if (parameters.Count() >= 2)
                        r = LocalizationService.LS.GetValue(parameters[0], parameters[1]);                 

                }
                else
                {
                    if (parameters.Count() == 1)
                        r =ls.GetValue(parameters[0]);
                    if (parameters.Count() >= 2)
                        r = ls.GetValue(parameters[0], parameters[1]);
                }

                if (parameters.Count() >= 3)
                    r = r + parameters[2];
                if (parameters.Count() >= 4)
                    r = parameters[3] + r;

                return r;

            }




        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
