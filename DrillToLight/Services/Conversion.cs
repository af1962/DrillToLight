using DrillToLight.Interfaces;
using System.Collections.ObjectModel;

namespace DrillToLight.Services
{
    internal class Conversion : IConversion
    {
        // Création d'une nouvelle collection sinon les modifications portent aussi sur GcodeOriginal
        ObservableCollection<string> conversion;
        public ObservableCollection<string> GetConvertir(ObservableCollection<string> gcodeDrill)
        {
            conversion = new ObservableCollection<string>(gcodeDrill);

            // Effacement des 10 preimères lignes
            for (int i = 0; i <= 9; i++)
            {
                conversion.RemoveAt(0);
            }

            // Effacement des 4 dernières lignes
            for (int i = 0; i <= 3; i++)
            {
                conversion.RemoveAt(conversion.Count - 1);
            }

            // Ajout d'une ligne à la fin pour arrêter le faisceau
            conversion.Add("M03 S0");

            // Remplacement Drill pour laser
            for (int i = conversion.Count - 1; i >= 0; i--)
            {
                if (conversion[i].Contains("Z0"))
                {
                    conversion[i] = "M03 S1000";
                }

                if (conversion[i].Contains("Z2"))
                {
                    conversion[i] = "M03 S0";
                }

                if (conversion[i].Contains("-"))
                {
                    conversion.RemoveAt(i);
                }

            }

            return conversion;
        }
    }
}
