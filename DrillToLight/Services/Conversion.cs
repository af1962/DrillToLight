namespace DrillToLight.Services
{
    internal class Conversion : IConversion
    {
        ObservableCollection<string>? gcodeLaser;
        public ObservableCollection<string> GetConvertir(ObservableCollection<string> gcodeDrill)
        {
            gcodeLaser = new ObservableCollection<string>();
            string str = "";

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
                if (gcodeDrill[i].Contains('Z'))
                {
                    if (gcodeDrill[i].Contains("Z0"))
                    {
                        gcodeLaser.Add("G0 Z0 M03 S1000");
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
            gcodeLaser.Add("M03 S0");

            return gcodeLaser;
        }
    }
}
