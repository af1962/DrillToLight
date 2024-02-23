namespace DrillToLight.Services
{
    internal class Conversion : IConversion
    {
        ObservableCollection<string>? conversion;
        public ObservableCollection<string> GetConvertir(ObservableCollection<string> gcodeDrill)
        {
            conversion = new ObservableCollection<string>();
            string recherche = "";
            int k = 0;
            while (recherche !="G00 Z2")
            {
                recherche = gcodeDrill[k];
                k++;
            }

            for (int i = k-1; i < gcodeDrill.Count - 4; i++)
            {

                if (!gcodeDrill[i].Contains('-') && !gcodeDrill[i].Contains("Z0") && !gcodeDrill[i].Contains("Z2"))
                {
                    conversion.Add(gcodeDrill[i]);
                }

                if (gcodeDrill[i].Contains("Z0"))
                {
                    conversion.Add("G0 Z0 M03 S1000");
                }

                if (gcodeDrill[i].Contains("Z2"))
                {
                    conversion.Add("G0 Z0 M03 S0");
                }
            }

            conversion.Add("M03 S0");

            return conversion;
        }
    }
}
