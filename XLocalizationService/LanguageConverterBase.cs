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

                if (parameters.Count() == 1)
                    return LocalizationService.LS.GetValue(parameters[0]);
                if (parameters.Count() >= 2)
                    return LocalizationService.LS.GetValue(parameters[0], parameters[1]);
                else
                    return string.Empty;
            }


               
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
