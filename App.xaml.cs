using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WeaponMaker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var session = new SessionService();
            ServiceLocator.Add(session);
            var commander = new CommandService();
            commander.AddRange(
                new Command[]
                {
                    new NewProjectCommand(),
                    new OpenProjectCommand(),
                    new SaveProjectAsCommand(),
                    new SaveProjectCommand(),
                    new NavigateToCommand(),
                    new ExportProjectCommand(),
                    new ExportWeaponCommand(),
                    new LoadProjectCommand(),
                    new ShutdownCommand(session)
                });
            ServiceLocator.Add(commander);

            var prefService = new PreferencesService();
            ServiceLocator.Add(prefService);

            //TODO: Move this somewhere else, inject or use interfaces idk
            session.Project.PropertyChanged += prefService.Project_PropertyChanged;
        }
    }
}
