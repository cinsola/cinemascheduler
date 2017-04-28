using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CinemaScheduler.Models
{
    public class ProjectPage : Page
    {
        public virtual event EventHandler<bool> NextButtonStatusChanged;

    }
}
