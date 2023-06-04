namespace DrillToLight.Interfaces
{
    internal interface ILecture
    {
        ObservableCollection<string> GetGcode(string chemin);
    }
}
