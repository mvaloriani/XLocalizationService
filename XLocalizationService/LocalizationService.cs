using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XLocalizationService
{

    public class LocalizationService
    {
        private Dictionary<String, String> lang = new Dictionary<String, String>();
        private List<List<String>> csv = new List<List<string>>();

        private List<String> _suppLang = new List<String>();
        public List<String> SupportedLanguages
        {
            get
            {
                return _suppLang;
            }
        }

        private string _currentLang = "";
        public string CurrentLang
        {
            get
            {
                return _currentLang;
            }
        }


        private static LocalizationService _ls;
        public static LocalizationService LS
        {
            get
            {
                if (_ls == null)
                    _ls = new LocalizationService();
                return _ls;
            }
        }

        public static LocalizationService NewXLS()
        {
            return new LocalizationService();
        }

        private LocalizationService()
        {
            _currentLang = "en-US";

            //olny the sample value
            ChangeVocabulary("XLocalizationService.SampleVocabulary.csv");
        }



        public void ChangeVocabulary(Stream stream)
        {
            string s = ReadFile(stream);
            updateVocabulary(s);
        }

        private void ChangeVocabulary(String path)
        {
            string s = ReadFile(path);
            updateVocabulary(s);
        }

        private void updateVocabulary(string s)
        {
            csv = CSVHelper.Parse(s);
            _suppLang = csv[0].GetRange(1, csv[0].Count - 1);

            var t = _currentLang;
            _currentLang = "";
            SetLanguage(t);
        }




        public void SetLanguage(string language)
        {

            if (language == _currentLang)
                return;


            if (!csv[0].Contains(language))
            {
                throw (new Exception("language not supported"));
            }

            this.lang.Clear();
            int l = csv[0].IndexOf(language);
            foreach (var row in csv)
            {
                if (row[0] != "" && row[0].ToLower() != "key")
                {
                    lang.Add(row[0], row[l]);
                }
            }

            _currentLang = language;

        }


        public string this[string key]
        {
            get
            {
                return GetValue(key);
            }
        }

        public string GetValue(string key, string def = "")
        {

            key = key.ToLower();
            if (lang.ContainsKey(key))
            {
                return lang[key];
            }
            else {

                Debug.WriteLine(key + " not found in localization service. Return default value: " + def);
                return def;
            }
        }



        public string ShortCodeToLanguage(string shortCode)
        {
            return GetValue("#" + shortCode.ToLower(), shortCode);

        }


        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <returns>The file.</returns>
        /// <param name="filepath">Filepath Namespace.Folder.Name.csv.</param>
        private static String ReadFile(String filepath)
        {
            var assembly = typeof(LocalizationService).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(filepath);

            return ReadFile(stream);
        }



        private static String ReadFile(Stream stream)
        {

            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }

    }
}

