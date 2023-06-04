namespace DrillToLight.Services
{
    internal class ModificationCode : IModificationCode
    {
        // ObservableCollection<string> code;
        public ObservableCollection<string> GetModif(ObservableCollection<string> code, string currentP, string currentS, string newP, string newS)
        {
            //code = new ObservableCollection<string>(collection);
            newP = "S" + newP;
            newS = "F" + newS;

            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].Contains(currentP))
                {
                    code[i] = code[i].Replace(currentP, newP);
                }

                if (code[i].Contains(currentS))
                {
                    code[i] = code[i].Replace(currentS, newS);
                }
            }

            return code;
        }
    }
}
