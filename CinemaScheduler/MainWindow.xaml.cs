using CinemaScheduler.Models;
using CinemaScheduler.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CinemaScheduler
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Visibility _backButtonVisibility;
        public bool _isNextButtonEnabled;

        public event PropertyChangedEventHandler PropertyChanged;

        public Visibility BackButtonVisibility { get { return _backButtonVisibility; } set { if (value != _backButtonVisibility) { _backButtonVisibility = value; OnPropertyChanged(); } } }
        public bool IsNextButtonEnabled { get { return _isNextButtonEnabled; } set { if (value != _isNextButtonEnabled) { _isNextButtonEnabled = value; OnPropertyChanged(); } } }
        public MainWindow()
        {
            InitializeComponent();
            NavigationFactory.Build(this);
            BackButtonVisibility = Visibility.Collapsed;
            this.DataContext = this;
            this.IsNextButtonEnabled = true;
            Page initialPage = NavigationFactory.GetDisplayPage(Components.BreadcrumbComponent.Step.Start);
            framedNavigation.Navigate(initialPage);
        }

        private void GoNext(object sender, RoutedEventArgs e)
        {
            Page page = NavigationFactory.Next();
            if (page is InitialView) { BackButtonVisibility = Visibility.Collapsed; } else { BackButtonVisibility = Visibility.Visible; }
            framedNavigation.Navigate(page);
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Page page = NavigationFactory.Prev();
            if (page is InitialView) { BackButtonVisibility = Visibility.Collapsed; } else { BackButtonVisibility = Visibility.Visible; }
            framedNavigation.Navigate(page);
        }

        public void OnPropertyChanged([CallerMemberName]string property = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
