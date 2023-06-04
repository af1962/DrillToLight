using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace DrillToLight.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        // Vitesse courante
        [ObservableProperty]
        private string speedCurrent;

        // Vitesse souhaitéé
        [ObservableProperty]
        private string speedNew;

        // Puissance courante
        [ObservableProperty]
        private string powerCurrent;

        // Puissance souhaitée
        [ObservableProperty]
        private string powerNew;

        // Chemin du nouveau fichier
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnregistrementCommand))]
        private string cheminNomNouveauFichier;

        // Chemin du fichier original
        [ObservableProperty]
        private string? cheminFichierOriginal;

        // Gcode original
        [ObservableProperty]
        ObservableCollection<string> gcodeOriginal;

        // Gcode modifié
        [ObservableProperty]
        ObservableCollection<string> gcodeModif;

        [ObservableProperty]
        private bool infoChargement;

        // Commande Bouton Parcourir
        [RelayCommand]
        private void Parcourir()
        {
            InfoChargement = true;
            CheminFichierOriginal = _dialogue.Fichier();
            Task tache = ExecuteParcourir();
        }

        // Affichage des gcodes dans les listBox
        public async Task ExecuteParcourir()
        {
            GcodeOriginal.Clear(); GcodeModif.Clear();

            await Task.Run(() =>
            {
                GcodeOriginal = _Lecture.GetGcode(CheminFichierOriginal);
                GcodeModif = _conversion.GetConvertir(GcodeOriginal);
                InfoChargement = false;
            });
            CheminNomNouveauFichier = CheminFichierOriginal.Insert(CheminFichierOriginal.Length - 3, "-Laser");
            Analyse();
        }

        // Bouton modification
        [RelayCommand]
        private void ModificationCode()
        {
            GcodeModif = _modificationCode.GetModif(GcodeModif, PowerCurrent, SpeedCurrent, PowerNew, SpeedNew);
            PowerCurrent = "S" + PowerNew;
            SpeedCurrent = "F" + SpeedNew;
            SpeedNew = "";
            PowerNew = "";
        }

        // Commande Bouton Enregistrer
        [RelayCommand]
        private void Enregistrement()
        {
            _enregistrement.Enregistrement(GcodeModif, CheminNomNouveauFichier);
            _dialogue.ShowMessage("Enregistrement", "Fichier modifié enregistré");
        }

        // Services
        IDialogue _dialogue;
        ILecture _Lecture;
        IConversion _conversion;
        IEnregistrement _enregistrement;
        IModificationCode _modificationCode;

        public MainViewModel(IDialogue dialogue, ILecture lecture, IConversion conversion, IEnregistrement enregistrement, IModificationCode modificationCode)
        {
            _dialogue = dialogue;
            _Lecture = lecture;
            _conversion = conversion;
            _enregistrement = enregistrement;
            GcodeOriginal = new ObservableCollection<string>();
            gcodeModif = new ObservableCollection<string>();
            _modificationCode = modificationCode;
        }

        /// <summary>
        /// Détermine la puissance et la vitesse par défaut
        /// </summary>
        public void Analyse()
        {
            string[] tab;
            tab = GcodeModif[1].Split(' ');
            PowerCurrent = tab[1];
            tab = GcodeModif[2].Split(' ');
            SpeedCurrent = tab[1];
        }
    }
}
