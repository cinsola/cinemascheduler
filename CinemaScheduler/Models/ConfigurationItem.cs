using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaScheduler.Models
{
    [Serializable]
    public class ConfigurationItem<T>
    {
        public string Name { get; set; }
        public string IsEditable { get; set; }
        public string VisualAppendix { get; set; }
        public T Value { get; set; }
        public ConfigurationItem(string name, T value)
        {
            this.Name = name;
            this.Value = value;
        }
        public ConfigurationItem(string name, string visualAppendix = null)
        {
            this.Name = name;
            this.Value = default(T);
            this.VisualAppendix = visualAppendix;
        }
    }
}
