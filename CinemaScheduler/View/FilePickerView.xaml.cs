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
    public partial class FilePickerView : ProjectPage
    {
        public override event EventHandler<bool> NextButtonStatusChanged;
        public FilePickerView()
        {
            InitializeComponent();
            this.Loaded += FilePickerView_Loaded;
        }
        
        /// <summary>
        /// Verifica iniziale. Evita che per errore il tasto "AVANTI" sia attivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilePickerView_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionChanged(this, files.PickedFiles);
        }

        /// <summary>
        /// Logica per mostrare il tasto avanti come attivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectionChanged(object sender, System.Collections.ObjectModel.ObservableCollection<PickedFile> e)
        {
            if(this.NextButtonStatusChanged != null)
            {
                if (e.Count == 0) { NextButtonStatusChanged(this, false); }
                else if (e.All(x => x.IsConversionOk == true)) { NextButtonStatusChanged(this, true); }
                else { NextButtonStatusChanged(this, false); }
            }
        }
    }
}
