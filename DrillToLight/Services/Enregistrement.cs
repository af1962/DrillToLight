namespace DrillToLight.Services
{
    internal class EnregistrementFichier : IEnregistrement
    {
        public void Sauvegarde(ObservableCollection<string> collection, string chemin)
        {
            using (StreamWriter ecrire = new StreamWriter(chemin))
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    {
                        ecrire.WriteLine(collection[i]);
                    }
                }
            }
        }
    }
}

