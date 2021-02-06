using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeaponMaker
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Weapon _weapon;
        public Weapon Weapon
        {
            get => _weapon;
            set => _weapon = value;
        }

        private Project _project;
        public Project Project
        {
            get => _project;
            set => _project = value;
        }

        public EditWindow()
        {
            _project = new Project();
            _weapon = new Weapon();
            InitializeComponent();
        }

        public EditWindow(Project project)
        {
            _project = project;
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
                    try
                    {
                        string output = JsonConvert.SerializeObject(_weapon, Formatting.Indented);
                        using (StreamWriter sw = new StreamWriter(fileDialog.FileName))
                        {
                            sw.WriteLine(output);
                        }
                        System.Windows.MessageBox.Show($"Sucess exporting {_weapon.WeaponName}!", "Success");
                    }
                    catch (Exception exception)
                    {
                        System.Windows.MessageBox.Show($"{exception.Message}", "Error");
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

        private void HandleOpenProjectClicked(object sender, RoutedEventArgs e)
        {
            var result = FileSystemService.OpenProject();
            if(result.success)
            {
                _project = result.project;
            }
        }

        private void HandleSaveProjectClicked(object sender, RoutedEventArgs e)
        {
            FileSystemService.SaveProject(_project);
        }
    }

}
