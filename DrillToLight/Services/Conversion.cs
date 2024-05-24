namespace DrillToLight.Services
{
    internal class Conversion : IConversion
    {
        ObservableCollection<string>? gcodeLaser;
        public ObservableCollection<string> GetConvertir(ObservableCollection<string> gcodeDrill)
        {
            gcodeLaser = new ObservableCollection<string>();
            string str = "";
            string [] tab;

            // Recherche le dernier Z
            int fin = gcodeDrill.Count;
            while (!str.Contains('Z'))
            {
                str = gcodeDrill[fin - 1];
                fin--;
            }

            // Recherche le premier Z
            str = "";
            int debut = 0;
            while (!str.Contains('Z'))
            {
                str = gcodeDrill[debut];
                debut++;
            }

            for (int i = debut - 1; i < fin; i++)
            {
                // Remplace tous les Fxx par F150. Les F sur les lignes avec Z seront de toutes façons supprimés par le code suivant
                if (gcodeDrill[i].Contains('F'))
                {
                    tab = gcodeDrill[i].Split(' ');
                    gcodeDrill[i] = gcodeDrill[i].Replace(tab[1], "F150");
                }

                // Adaptation course au laser
                if (gcodeDrill[i].Contains('Z'))
                {
                    if (gcodeDrill[i].Contains("Z0"))
                    {
                        gcodeLaser.Add("G0 Z0 M03 S100");
                    }
                    else
                    {
                        if (!gcodeDrill[i].Contains('-'))
                        {
                            gcodeLaser.Add("G0 Z0 M03 S0");
                        }
                    }
                }
                else
                {
                    gcodeLaser.Add(gcodeDrill[i]);
                }

            }

            // Arrêt du laser à la fin
            gcodeLaser.Add("M03 S0");

            return gcodeLaser;
        }
    }
}
