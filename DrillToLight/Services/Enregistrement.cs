using DrillToLight.Interfaces;
using System.Collections.ObjectModel;
using System.IO;

namespace DrillToLight.Services
{
    internal class Enregistrement : IEnregistrement
    {
        void IEnregistrement.Enregistrement(ObservableCollection<string> collection, string chemin)
        {
            using (StreamWriter ecrire = new StreamWriter(chemin))
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    {
                        ecrire.WriteLine(collection[i]);
                    }
                }
                ecrire.Close();
            }
        }
    }
}

