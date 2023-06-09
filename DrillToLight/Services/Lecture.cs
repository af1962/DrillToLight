﻿namespace DrillToLight.Services
{
    internal class Lecture : ILecture
    {
        ObservableCollection<string>? lecture;
        public ObservableCollection<string> GetGcode(string file)
        {
                     
            lecture = new ObservableCollection<string>();

            using (StreamReader lire = new StreamReader(file, System.Text.Encoding.UTF8))
            {
                string? ligne;
                while ((ligne = lire.ReadLine()) != null)
                {
                    lecture.Add(ligne);
                }
            }

            return lecture;
        }
    }
}
