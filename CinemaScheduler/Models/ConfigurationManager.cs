using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CinemaScheduler.Models
{
    [Serializable]
    public class ConfigurationManager : ObservableCollection<ConfigurationGroup>, ICloneable
    {
        public string Name { get; set; }
        public ConfigurationManager(string name)
        {
            this.Name = name;
            SavedSettings.Add(this);
            DefaultSettings = this;
        }

        public static ObservableCollection<ConfigurationManager> SavedSettings { get; set; }
        public static ConfigurationManager DefaultSettings { get; set; }
        static ConfigurationManager()
        {
            SavedSettings = new ObservableCollection<ConfigurationManager>();
            var coins = new ConfigurationGroup("Gettoni presenza", stringConfig: null, boolConfig: null,
                floatConfig: new ObservableCollection<ConfigurationItem<float>>(new ConfigurationItem<float>[] {
                    new ConfigurationItem<float>("Costo presenza per turno", "€"),
                    new ConfigurationItem<float>("Costo presenza a giornata", "€")
                }));
            var reading = new ConfigurationGroup("Lettura del copione", stringConfig: null,
                boolConfig: new ObservableCollection<ConfigurationItem<bool>>(new ConfigurationItem<bool>[] {
                    new ConfigurationItem<bool>("Consenti sforamenti sul numero delle righe per turno"),
                }),
                floatConfig: new ObservableCollection<ConfigurationItem<float>>(new ConfigurationItem<float>[] {
                    new ConfigurationItem<float>("Costo per riga", "€"),
                    new ConfigurationItem<float>("Numero massimo di righe lette per turno", "righe"),
                    new ConfigurationItem<float>("Sfora al massimo fino a tot righe per per turno", "righe extra"),
                    new ConfigurationItem<float>("Velocità di lettura degli attori esperti (righe/minuto)", "righe/minuto"),
                    new ConfigurationItem<float>("Velocità di lettura degli attori (righe/minuto)", "righe/minuto"),
                    new ConfigurationItem<float>("Velocità di lettura degli attori inesperti, bambini o anziani (righe/minuto)", "righe/minuto")
                }));
            var turno = new ConfigurationGroup("Gestione del turno", stringConfig: null,
                boolConfig: new ObservableCollection<ConfigurationItem<bool>>(new ConfigurationItem<bool>[] {
                    new ConfigurationItem<bool>("Consenti sforamenti sulla durata del turno"),
                    new ConfigurationItem<bool>("Abilita lavorazioni di sabato (ottimizza i tempi più dei costi)"),
                    new ConfigurationItem<bool>("Abilita quarto turno di lavorazione (ottimizza i tempi più dei costi)"),
                }),
                floatConfig: new ObservableCollection<ConfigurationItem<float>>(new ConfigurationItem<float>[] {
                    new ConfigurationItem<float>("Durata massima del turno (in minuti)", "minuti"),
                    new ConfigurationItem<float>("Sfora sulla durata massima del turno fino a tot minuti (durata totale)", "minuti extra"),
                    new ConfigurationItem<float>("Moltiplicatore costo lavorazione di sabato", "x"),
                    new ConfigurationItem<float>("Moltiplicatore costo lavorazione sul quarto turno", "x")
                }));
            SavedSettings = LoadSettings();
            DefaultSettings = new ConfigurationManager("Configurazione di default");
            DefaultSettings.Add(coins);
            DefaultSettings.Add(reading);
            DefaultSettings.Add(turno);
        }

        public static ObservableCollection<ConfigurationManager> LoadSettings()
        {
            try
            {
                string appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CinemaScheduler");
                if (!Directory.Exists(appData))
                {
                    Directory.CreateDirectory(appData);
                }
                string fileName = Path.Combine(appData, "savedSettings.dat");
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    BinaryFormatter bs = new BinaryFormatter();
                    return (ObservableCollection<ConfigurationManager>)bs.Deserialize(fs);

                }
            }
            catch (FileNotFoundException)
            {
                return new ObservableCollection<ConfigurationManager>();
            }
        }

        public object Clone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return formatter.Deserialize(ms);
            }
        }
    }

    [Serializable]
    public class ConfigurationGroup
    {
        public string Name { get; set; }
        public ObservableCollection<ConfigurationItem<string>> StringItems { get; set; }
        public ObservableCollection<ConfigurationItem<bool>> BoolItems { get; set; }
        public ObservableCollection<ConfigurationItem<float>> FloatItems { get; set; }
        public ConfigurationGroup(string name, ObservableCollection<ConfigurationItem<string>> stringConfig,
            ObservableCollection<ConfigurationItem<bool>> boolConfig,
            ObservableCollection<ConfigurationItem<float>> floatConfig)
        {
            this.Name = name;
            this.StringItems = stringConfig;
            this.BoolItems = boolConfig;
            this.FloatItems = floatConfig;
        }
    }


}
