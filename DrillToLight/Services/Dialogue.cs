﻿using Microsoft.Win32;

namespace DrillToLight.Services
{
    internal class Dialogue : IDialogue
    {

        /// <summary>
        /// Fichier
        /// </summary>
        /// <returns></returns>
        public string Fichier()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }

        /// <summary>
        /// Message d'erreur
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void ShowError(string title, string message)
        {
            System.Windows.MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.No);
        }

        /// <summary>
        /// Message standard
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void ShowMessage(string title, string message)
        {
            System.Windows.MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Message Stop
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void ShowStop(string title, string message)
        {
            System.Windows.MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Stop);

        }

        /// <summary>
        /// Messsage Yes/No
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool YesNo(string title, string message)
        {
            return MessageBoxResult.Yes == System.Windows.MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

        }
    }
}
