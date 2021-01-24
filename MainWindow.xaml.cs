using System.IO;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WeaponMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Weapon _weapon;
        public Weapon Weapon
        {
            get => _weapon;
            set => _weapon = value;
        }

        public MainWindow()
        {
            _weapon = new Weapon();
            InitializeComponent();
        }

        private void HandleExportJsonClicked(object sender, RoutedEventArgs e)
        {
            var fileDialog = new SaveFileDialog
            {
                FileName = _weapon.WeaponName,
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.json"
            };
            var result = fileDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    string output = JsonConvert.SerializeObject(_weapon, Formatting.Indented);
                    using (StreamWriter sw = new StreamWriter(fileDialog.FileName))
                    {
                        sw.WriteLine(output);
                    }
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    break;
            }
        }

        private void HandleImportJsonClicked(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                FileName = _weapon.WeaponName,
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.json"
            };
            var result = fileDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    string input = "";
                    using (StreamReader sw = new StreamReader(fileDialog.FileName))
                    {
                        input = sw.ReadToEnd();
                    }
                    var weapon = JsonConvert.DeserializeObject<Weapon>(input);
                    _weapon.Copy(weapon);
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    break;
            }
        }

    }
}
