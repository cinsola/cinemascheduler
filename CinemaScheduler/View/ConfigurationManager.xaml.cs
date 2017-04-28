using CinemaScheduler.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

namespace CinemaScheduler.View
{
    /// <summary>
    /// Logica di interazione per ConfigurationManager.xaml
    /// </summary>
    /// 

    public partial class ConfigurationManager : ProjectPage, INotifyPropertyChanged
    {
        public ConfigurationManager()
        {
            DisplayedConfiguration = Models.ConfigurationManager.DefaultSettings;
            Presets = Models.ConfigurationManager.SavedSettings;
            NewConfigurationPromptVisible = Visibility.Hidden;
            InitializeComponent();
            DataContext = this;
            this.Loaded += ConfigurationManager_Loaded;
        }

        private void ConfigurationManager_Loaded(object sender, RoutedEventArgs e)
        {
            NextButtonStatusChanged?.Invoke(this, true);
        }

        public override event EventHandler<bool> NextButtonStatusChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public Visibility _newConfigurationPromptVisible;
        public Models.ConfigurationManager _displayedConfiguration;
        public Models.ConfigurationManager DisplayedConfiguration
        {
            get { return _displayedConfiguration; }
            set
            {
                if (value != _displayedConfiguration)
                {
                    _displayedConfiguration = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayedConfiguration)));
                }
            }
        }
        public Visibility NewConfigurationPromptVisible
        {
            get { return _newConfigurationPromptVisible; }
            set
            {
                if (value != _newConfigurationPromptVisible)
                {
                    _newConfigurationPromptVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewConfigurationPromptVisible)));
                }
            }
        }
        public ObservableCollection<Models.ConfigurationManager> Presets { get; set; }

        /// <summary>
        /// Salva configurazione corrente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveNew(object sender, RoutedEventArgs e)
        {
            if(configurationNameView.Text.Trim().Length < 3 || configurationNameView.Text.Trim().Length > 30)
            {
                MessageBox.Show("Il nome della configurazione deve essere fra i 3 e i 30 caratteri", "Impossibile aggiungere", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if(Presets.Any(x => x.Name == configurationNameView.Text))
            {
                MessageBox.Show("Esiste già una configurazione con questo nome", "Impossibile aggiungere", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                var clone = (Models.ConfigurationManager)DisplayedConfiguration.Clone();
                clone.Name = configurationNameView.Text.Trim();
                Models.ConfigurationManager.SavedSettings.Add(clone);
                DisplayedConfiguration = clone;
                NewConfigurationPromptVisible = Visibility.Hidden;
                configurationNameView.Clear();
            }
        }

        /// <summary>
        /// Elimina configurazione corrente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCurrent(object sender, RoutedEventArgs e)
        {
            if(Presets.Count > 1)
            {
                Presets.Remove(DisplayedConfiguration);
                DisplayedConfiguration = Presets.First();
            } else
            {
                MessageBox.Show("Deve esserci almeno una configurazione", "Impossibile cancellare", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #region prompt
        public void UndoRequireConfigurationPrompt(object sender, RoutedEventArgs e)
        {
            NewConfigurationPromptVisible = Visibility.Hidden;
        }
        private void RequireConfigurationPrompt(object sender, RoutedEventArgs e)
        {
            NewConfigurationPromptVisible = Visibility.Visible;
        }
        #endregion
        #region helpers
        private void ForceNumber(object sender, TextCompositionEventArgs e)
        {
            float result = 0;
            if(float.TryParse(e.Text, out result) || e.Text == ".")
            {
                e.Handled = false;
            } else
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}
