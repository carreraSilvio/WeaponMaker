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

        private WeaponEditPage _weaponEditPage;
        private ProjectEditPage _projectEditPage;

        private SessionService _session;
        private CommandService _commandService;

        public EditWindow()
        {
            _session = ServiceLocator.Fetch<SessionService>();
            InitializeComponent();

            _weaponEditPage = new WeaponEditPage();
            _projectEditPage = new ProjectEditPage();
            _mainFrame.Navigate(_weaponEditPage);

            _commandService = ServiceLocator.Fetch<CommandService>();
        }

        #region New/Open/Save
        private void HandleNewProjectClicked(object sender, RoutedEventArgs e)
        {
            if (_commandService.Get<NewProjectCommand>().Execute())
            {
                var args = new NavigateToCommand.Args()
                {
                    caller = this,
                    target = typeof(EditWindow)
                };
                _commandService.Get<NavigateToCommand>().Execute(args);
            }
        }

        private void HandleOpenProjectClicked(object sender, RoutedEventArgs e)
        {
            if (_commandService.Get<OpenProjectCommand>().Execute())
            {
                var args = new NavigateToCommand.Args()
                {
                    caller = this,
                    target = typeof(EditWindow)
                };
                _commandService.Get<NavigateToCommand>().Execute(args);
            }
        }

        private void HandleSaveProjectClicked(object sender, RoutedEventArgs e)
        {
            _commandService.Get<SaveProjectCommand>().Execute();
        }

        private void HandleSaveProjectAsClicked(object sender, RoutedEventArgs e)
        {
            _commandService.Get<SaveProjectAsCommand>().Execute();
        }
        #endregion

        #region Export/Import
        private void HandleExportJsonClicked(object sender, RoutedEventArgs e)
        {
            FileSystemService.ExportWeapon(_weaponEditPage.Weapon);
        }

        private void HandleImportJsonClicked(object sender, RoutedEventArgs e)
        {
            var result = FileSystemService.ImportWeapon();
            if (result.success)
            {
                _weaponEditPage.Weapon.Copy(result.weapon);
            }
        } 
        #endregion

        #region View Clicked
        private void HandleWeaponsEditViewClicked(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(_weaponEditPage);
        }

        private void HandleProjectEditViewClicked(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(_projectEditPage);
        } 
        #endregion
    }

}
