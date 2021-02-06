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

        private WeaponEditPage _weaponEditPage;

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

            _weaponEditPage = new WeaponEditPage(_weapon);
            _mainFrame.Navigate(_weaponEditPage);
        }

        private void HandleExportJsonClicked(object sender, RoutedEventArgs e)
        {
            FileSystemService.ExportWeapon(_weaponEditPage.Weapon);
        }

        private void HandleImportJsonClicked(object sender, RoutedEventArgs e)
        {
            var result = FileSystemService.ImportWeapon();
            if(result.success)
            {
                _weaponEditPage.Weapon.Copy(result.weapon);
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
