﻿using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace DrillToLight.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string version;

        // Vitesse courante
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ModificationCodeCommand))]
        private string speedCurrent = string.Empty;

        // Vitesse souhaitéé
        [ObservableProperty]
        private string speedNew = string.Empty;

        // Puissance courante
        [ObservableProperty]
        private string powerCurrent = string.Empty;

        // Puissance souhaitée
        [ObservableProperty]
        private string powerNew = string.Empty;

        // Chemin du nouveau fichier
        [ObservableProperty]
        private string cheminNomNouveauFichier = string.Empty;

        // Chemin du fichier original
        [ObservableProperty]
        private string cheminFichierOriginal = string.Empty;

        // Gcode original
        [ObservableProperty]
        ObservableCollection<string> gcodeOriginal;

        // Gcode modifié
        [ObservableProperty]
        ObservableCollection<string> gcodeModif;

        [ObservableProperty]
        private bool chargement;

        /// <summary>
        /// Commande Bouton Parcourir
        /// </summary>
        [RelayCommand]
        private void Parcourir()
        {
            CheminFichierOriginal = _dialogue.Fichier();

            if (!string.IsNullOrEmpty(CheminFichierOriginal))
            {
                Task tache = ExecuteParcourir();
                Chargement = true;
            }
        }

        /// <summary>
        /// Affichage des gcodes dans les listBox
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteParcourir()
        {
            GcodeOriginal.Clear(); GcodeModif.Clear();

            await Task.Run(() =>
            {
                GcodeOriginal = _Lecture.GetGcode(CheminFichierOriginal);
                GcodeModif = _conversion.GetConvertir(GcodeOriginal);
                Chargement = false;
            });
            Analyse();
            NewFile();
        }

        /// <summary>
        /// Bouton modification
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanExecuteModificationCode))]
        private void ModificationCode()
        {
            GcodeModif = _modificationCode.GetModif(GcodeModif, PowerCurrent, SpeedCurrent, PowerNew, SpeedNew);
            if (SpeedNew != "") { SpeedCurrent = SpeedNew; }
            if (PowerNew != "") { PowerCurrent = PowerNew; }
            SpeedNew = "";
            PowerNew = "";
            NewFile();
        }

        /// <summary>
        /// CanExecute ModificationCode
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteModificationCode()
        {
            return (!string.IsNullOrEmpty(SpeedCurrent) && !string.IsNullOrEmpty(PowerCurrent));
        }

        /// <summary>
        /// Commande Bouton Enregistrer
        /// </summary>
        [RelayCommand]
        private void Enregistrement()
        {
            _enregistrement.Sauvegarde(GcodeModif, CheminNomNouveauFichier);
            _dialogue.ShowMessage("Enregistrement", "Fichier modifié enregistré");
        }

        /// <summary>
        /// Chemin du fichier de sortie
        /// </summary>
        private void NewFile()
        {
            CheminNomNouveauFichier = CheminFichierOriginal.Insert(CheminFichierOriginal.Length - 3, "-Laser - S" + PowerCurrent + "- F" + SpeedCurrent);
        }

        // Services
        IDialogue _dialogue;
        ILecture _Lecture;
        IConversion _conversion;
        IEnregistrement _enregistrement;
        IModificationCode _modificationCode;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="dialogue"></param>
        /// <param name="lecture"></param>
        /// <param name="conversion"></param>
        /// <param name="enregistrement"></param>
        /// <param name="modificationCode"></param>
        public MainViewModel(IDialogue dialogue, ILecture lecture, IConversion conversion, IEnregistrement enregistrement, IModificationCode modificationCode)
        {
            _dialogue = dialogue;
            _Lecture = lecture;
            _conversion = conversion;
            _enregistrement = enregistrement;
            GcodeOriginal = new ObservableCollection<string>();
            GcodeModif = new ObservableCollection<string>();
            _modificationCode = modificationCode;

            Version = "Convertisseur de Gcode - Version du 26/05/2024";
        }

        /// <summary>
        /// Détermine la puissance et la vitesse par défaut
        /// se trouve en début de code de GcodeModif
        /// </summary>
        public void Analyse()
        {
            string[] tab;
            tab = GcodeModif[2].Split(' ');
            PowerCurrent = tab[3][1..]; // Chaîne à partir du 1er caractère pour éviter le doublon à l'affichage
            tab = GcodeModif[3].Split(' ');
            SpeedCurrent = tab[1][1..];
        }
    }
}
