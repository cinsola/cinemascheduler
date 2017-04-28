using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CinemaScheduler.Models
{
    [Serializable]
    public class PickedFile : INotifyPropertyChanged
    {
        public EventHandler FileStatusChanged;
        public PickedFile(string name)
        {
            this.FileName = name;
            ImportCommand = new ImportCommand(this);
            IsWrongFile = false;
            IsConversionRequired = true;
            IsConversionOk = false;
            IsPerformingConversion = false;
        }

        public ICommand ImportCommand { get; private set; }
        public List<BusinessLogic.Vector> ImportResult { get; private set; }
        private string _fileName;
        public string FileName { get { return _fileName; } set { if(_fileName != value) { _fileName = value; OnPropertyChanged(); } } }
        public bool _isPerformingConversion;
        public bool IsPerformingConversion { get { return _isPerformingConversion; } set { if (_isPerformingConversion != value) { _isPerformingConversion = value; OnPropertyChanged(); } } }

        public bool _isWrongFile;
        public bool IsWrongFile { get { return _isWrongFile; } set { if (_isWrongFile != value) { _isWrongFile = value; OnPropertyChanged(); } } }

        public bool _isConversionRequired;
        public bool IsConversionRequired { get { return _isConversionRequired; } set { if (_isConversionRequired != value) { _isConversionRequired = value; OnPropertyChanged(); } } }

        public bool IsConversionOk { get { return _isConversionOk; } set { if (_isConversionOk != value) { _isConversionOk = value; OnPropertyChanged(); } } }
        public bool _isConversionOk;
        private int _actors;
        private int _lines;
        private int _rings;

        public int Actors { get { return _actors; } set { if (value != _actors) { _actors = value; OnPropertyChanged(); } } }
        public int Lines { get { return _lines; } set { if (value != _lines) { _lines = value; OnPropertyChanged(); } } }
        public int Rings { get { return _rings; } set { if (value != _rings) { _rings = value; OnPropertyChanged(); } } }
        public void ImportAsync()
        {
            IsPerformingConversion = true;
            IsWrongFile = false;
            IsConversionRequired = false;
            IsConversionOk = false;
            Task.Run(() =>
            {
                try
                {
                    ImportResult = BusinessLogic.ExcelHelper.Importer.Import(FileName);
                    if (ImportResult != null && ImportResult.Count > 0)
                    {
                        return true;
                    } else { return false; }
                }
                catch { return false; }
            }).ContinueWith(f =>
            {
                if(f.Result == true)
                {
                    IsPerformingConversion = false;
                    IsWrongFile = false;
                    IsConversionRequired = false;
                    IsConversionOk = true;
                    Actors = ImportResult.Select(x => x.Actor).Distinct().Count();
                    Rings = ImportResult.Count;
                    Lines = (int)ImportResult.Select(x => x.Lines).Sum();
                } else
                {
                    IsPerformingConversion = false;
                    IsWrongFile = true;
                    IsConversionRequired = false;
                    IsConversionOk = false;
                }

                FileStatusChanged?.Invoke(this, new EventArgs());
            });
        }
        public void OnPropertyChanged([CallerMemberName]string property = null)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
