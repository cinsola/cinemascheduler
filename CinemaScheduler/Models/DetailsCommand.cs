using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CinemaScheduler.Models
{
    public class ImportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private PickedFile file;
        public ImportCommand(PickedFile file)
        {
            this.file = file;
        }

        public bool CanExecute(object parameter)
        {
            return true;
            //var choosenElement = parameter as PickedFile;
            //if (choosenElement.IsImported == true)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
        }

        public void Execute(object parameter)
        {
            file.ImportAsync();
        }
    }
}
