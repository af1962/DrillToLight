namespace DrillToLight.Interfaces
{
    internal interface IEnregistrement
    {
        void Sauvegarde(ObservableCollection<string> collection, string chemin);
    }
}
