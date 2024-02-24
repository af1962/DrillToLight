namespace DrillToLight.Services
{
    internal class Conversion : IConversion
    {
        ObservableCollection<string>? conversion;
        public ObservableCollection<string> GetConvertir(ObservableCollection<string> gcodeDrill)
        {
            conversion = new ObservableCollection<string>();
            string str = "";
            int k = 0;
            while (!str.Contains('Z'))
            {
                str = gcodeDrill[k];
                k++;
            }

            for (int i = k - 1; i < gcodeDrill.Count - 4; i++)
            {

                if (gcodeDrill[i].Contains('Z'))
                {
                    if (gcodeDrill[i].Contains("Z0"))
                    {
                        conversion.Add("G0 Z0 M03 S1000");
                    }
                    else
                    {
                        if (!gcodeDrill[i].Contains('-'))
                        {
                            conversion.Add("G0 Z0 M03 S0");
                        }
                    }
                }
                else
                {
                    conversion.Add(gcodeDrill[i]);
                }
            }
            conversion.Add("M03 S0");

            return conversion;
        }
    }
}
