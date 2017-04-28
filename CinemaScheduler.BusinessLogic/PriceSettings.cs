using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaScheduler.BusinessLogic
{
    public static class PriceSettings
    {
        public static float MaxTurnDuration = 60 * 3; //180minuti
        public static float MaxLinesPerTurn = 140;
        public static float LinesPerMinute = 0.7f; //righe al minuto per attore
        public static float PresenceCoin = 76f; //gettone presenza
        public static float LinePrice = 2f; //costo riga
        public static float MaxLinesPerTurnPerActor = 90; //massimo numero di righe per turno per attore
        public static VectorGroup WorkingGroup { get; set; }
        public static List<Vector> OriginalSource { get; set; }
        public static float SeparatedColumnLimit = 12;
    }
}
