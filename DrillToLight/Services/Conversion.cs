namespace DrillToLight.Services
{
    internal class Conversion : IConversion
    {
        ObservableCollection<string>? conversion;
        public ObservableCollection<string> GetConvertir(ObservableCollection<string> gcodeDrill)
        {
            conversion = new ObservableCollection<string>();

            for (int i = 10; i <= gcodeDrill.Count - 5; i++)
            {

                if (!gcodeDrill[i].Contains("-") && !gcodeDrill[i].Contains("Z0") && !gcodeDrill[i].Contains("Z2"))
                {
                    conversion.Add(gcodeDrill[i]);
                }

                if (gcodeDrill[i].Contains("Z0"))
                {
                    conversion.Add("M03 S1000");
                }

                if (gcodeDrill[i].Contains("Z2"))
                {
                    conversion.Add("M03 S0");
                }
            }

            conversion.Add("M03 S0");

            return conversion;
        }
    }
}
