using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CinemaScheduler.Components
{
    /// <summary>
    /// Logica di interazione per BreadcrumbComponent.xaml
    /// </summary>
    public partial class BreadcrumbComponent : UserControl, INotifyPropertyChanged
    {
        private Step _currentStep;

        public event PropertyChangedEventHandler PropertyChanged;

        public Step CurrentStep { get { return _currentStep; } set
            {
                if(_currentStep != value)
                {
                    _currentStep = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentStep)));
                    }
                }
            }
        } 
        public BreadcrumbComponent()
        {
            InitializeComponent();
            CurrentStep = Step.Start;
        }
        public enum Step
        {
            Start,
            LoadFiles,
            SetParameters,
            Calendar,
            Final
        }
    }
}
