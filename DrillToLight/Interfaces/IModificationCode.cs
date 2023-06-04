namespace DrillToLight.Interfaces
{
    internal interface IModificationCode
    {
        ObservableCollection<string> GetModif(ObservableCollection<string> collection, string p, string s, string newP, string newS);
    }
}
