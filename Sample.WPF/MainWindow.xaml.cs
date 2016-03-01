using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XLocalizationService;

namespace Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this is a file as content
            StreamReader sr = new StreamReader("Vocabulary.csv");
            LocalizationService.LS.ChangeVocabulary(sr.BaseStream);

            foreach ( var l in LocalizationService.LS.SupportedLanguages)
                this.LanguagesComboBox.Items.Add(l) ;

            this.LanguagesComboBox.SelectedIndex=0;

            refreshLabel();

            
        }


        private void LanguagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = this.LanguagesComboBox.SelectedIndex;
            if(index>=0 && index<LocalizationService.LS.SupportedLanguages.Count)
                LocalizationService.LS.SetLanguage(LocalizationService.LS.SupportedLanguages[index]);

            refreshLabel();
        }

        private void refreshLabel()
        {
            this.Sample1TextBlock.Text = LocalizationService.LS["#example"];
            this.Sample2TextBlock.Text = LocalizationService.LS["#example2"];
            this.KeyTextBlock.Text = LocalizationService.LS["#key"];
            this.KeyButton.Content= LocalizationService.LS["#testbutton"];

            this.ResultTextBlock.Text = LocalizationService.LS[this.KeyTextBox.Text];

        }

        private void KeyButton_Click(object sender, RoutedEventArgs e)
        {
            this.ResultTextBlock.Text= LocalizationService.LS[this.KeyTextBox.Text];
        }
    }
}
