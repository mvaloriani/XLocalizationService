using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLocalizationService
{
    public abstract class LanguageConverterBase 
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;

            if (string.IsNullOrEmpty(parameterString))
                return string.Empty;

            else
            {
                string[] parameters = parameterString.Split('|');

                string r = string.Empty;
                if (parameters.Count() == 1)
                    r = LocalizationService.LS.GetValue(parameters[0]);
                if (parameters.Count() >= 2)
                    r = LocalizationService.LS.GetValue(parameters[0], parameters[1]);
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
