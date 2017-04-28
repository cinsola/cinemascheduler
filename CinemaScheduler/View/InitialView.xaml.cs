using CinemaScheduler.Models;
using System;
using System.Collections.Generic;
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
    public partial class InitialView : ProjectPage
    {
        public override event EventHandler<bool> NextButtonStatusChanged;
        public InitialView()
        {
            InitializeComponent();
            this.Loaded += InitialView_Loaded;
        }

        /// <summary>
        /// Il bottone "AVANTI" per questa pagina è sempre visibile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitialView_Loaded(object sender, RoutedEventArgs e)
        {
            NextButtonStatusChanged?.Invoke(this, true);
        }
    }
}
