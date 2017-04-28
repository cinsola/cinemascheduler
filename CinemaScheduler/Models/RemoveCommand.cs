using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CinemaScheduler.Models
{
    public class RemoveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private ObservableCollection<PickedFile> items;
        public RemoveCommand(ObservableCollection<PickedFile> items)
        {
            this.items = items;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var choosenElement = parameter as PickedFile;
            items.Remove(choosenElement);
        }
    }
}
