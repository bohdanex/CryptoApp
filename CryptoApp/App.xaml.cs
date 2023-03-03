using CryptoApp.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml;

namespace CryptoApp
{
    public partial class App : Application
    {
        private static string _theme;
        private static string _localization;
        private static CryptoCurrency _currency;

        public static List<CryptoCurrency> TopTenCurrenciesList { get; set; } = new List<CryptoCurrency>();

        public static string Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                UpdateConfig(value, nameof(Theme));
                Application.Current.Resources.MergedDictionaries[1].Source = new Uri($"/Themes/{value}Theme.xaml", UriKind.Relative);
            }
        }

        public static string Localization
        {
            get => _localization;
            set
            {
                _localization = value;
                UpdateConfig(value, nameof(Localization));

                //Thread.CurrentThread.CurrentCulture = new CultureInfo(_localization.Substring(0,2)+"-US");
                //Thread.CurrentThread.CurrentUICulture = new CultureInfo(_localization);
            }
        }

        public delegate void LoadCryptoCurrency();
        public static event LoadCryptoCurrency CryptoCurrencies_Loaded;

        public App()
        {
            LoadConfig();
        }

        private static void LoadConfig()
        {
            XmlDocument appSettingsXml = new XmlDocument();
            appSettingsXml.Load("../../../AppSettings.xml");

            _theme = appSettingsXml.SelectSingleNode("configuration/theme").FirstChild.InnerText ?? "Ocean";

            _localization = appSettingsXml.SelectSingleNode("configuration/localization").FirstChild.InnerText ?? "English";
        }

        private static void UpdateConfig(string value, string propertyCaller)
        {
            if (propertyCaller == nameof(Theme))
            {
                string fileConfigpath = "../../../AppSettings.xml";
                XmlDocument configXml = new XmlDocument();
                configXml.Load(fileConfigpath);
                configXml.DocumentElement.FirstChild.FirstChild.InnerText = value;
                configXml.Save(fileConfigpath);
            }
            else if (propertyCaller == nameof(Localization))
            {
                string fileConfigpath = "../../../AppSettings.xml";
                XmlDocument configXml = new XmlDocument();
                configXml.Load(fileConfigpath);
                configXml.DocumentElement.LastChild.FirstChild.InnerText = value;
                configXml.Save(fileConfigpath);
            }
        }

        async private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            TopTenCurrenciesList = (List<CryptoCurrency>)await CryptoCurrencyRepository.GetCryptoCurrenciesAsync();
            CryptoCurrencies_Loaded?.Invoke();
        }
    }
}
