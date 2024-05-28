using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace DrillToLight.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        // Version
        [ObservableProperty]
        private string version;

        // Vitesse courante
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ModificationCodeCommand))]
        private string speedCurrent = string.Empty;

        // Vitesse souhaitée
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

        // Affichage du tecte traitement en cours
        [ObservableProperty]
        private bool chargement;

        // Ligne selectionnée comme point d'entrée
        [ObservableProperty]

        private string selectedStartLine = string.Empty;
        // Index de la ligne selectionnée
        [ObservableProperty]
        private int indexStartLine;

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
                SelectedStartLine = "";
                IndexStartLine = -1;
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
                GcodeModif = _conversion.GetConvertir(GcodeOriginal, IndexStartLine);
                Chargement = false;
            });
            PowerCurrent = "100";
            SpeedCurrent = "150";

            CheminNomNouveauFichier = NewFile(CheminFichierOriginal);
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
            CheminNomNouveauFichier = NewFile(CheminFichierOriginal);
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
        private string NewFile(string file)
        {
            //CheminNomNouveauFichier = CheminFichierOriginal.Insert(CheminFichierOriginal.Length - 3, "-Laser - S" + PowerCurrent + "- F" + SpeedCurrent);
            return file.Insert(file.Length - 3, "-Laser - S" + PowerCurrent + "- F" + SpeedCurrent);
        }

        /// <summary>
        /// Bouton validation du point d'entrée
        /// </summary>
        [RelayCommand]
        private void PointEntree()
        {
            GcodeModif = _conversion.GetConvertir(GcodeOriginal, IndexStartLine);
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

            Version = "Convertisseur de Gcode - Version du 28/05/2024";
        }
    }
}
