using DrillToLight.Interfaces;
using System.Collections.ObjectModel;
using System.IO;

namespace DrillToLight.Services
{
    internal class Lecture : ILecture
    {
        ObservableCollection<string>? lecture;
        public ObservableCollection<string> GetGcode(string file)
        {
            lecture = new ObservableCollection<string>();

            using (StreamReader lire = new StreamReader(file))
            {
                string? ligne;
                while ((ligne = lire.ReadLine()) != null)
                {
                    lecture.Add(ligne);
                }
                
                lire.Close();
            }

            return lecture;
        }
    }
}
