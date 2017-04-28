using CinemaScheduler.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CinemaScheduler.Components
{
    /// <summary>
    /// Logica di interazione per FilePicker.xaml
    /// </summary>
    public partial class FilePickerComponent : UserControl
    {
        public ICommand DeleteCommand { get; private set; }
        public ObservableCollection<PickedFile> PickedFiles { get; private set; }
        public event EventHandler<ObservableCollection<PickedFile>> FilesCollectionChanged;
        public FilePickerComponent()
        {
            InitializeComponent();
            PickedFiles = new ObservableCollection<PickedFile>();
            DeleteCommand = new RemoveCommand(PickedFiles);
            DataContext = this;
            PickedFiles.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FilesCollectionChanged?.Invoke(this, PickedFiles);
        }

        private void AddFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Riepiloghi (*.xls,xlsx)|*.xls;*.xlsx";
            if(fileDialog.ShowDialog() == true)
            {
                PickedFile pickedFile = new PickedFile(fileDialog.FileName);
                PickedFiles.Add(pickedFile);
                pickedFile.FileStatusChanged += (s, args) =>
                {
                    FilesCollectionChanged(this, PickedFiles);
                };
            }
        }
    }


}
