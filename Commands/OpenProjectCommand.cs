using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class OpenProjectCommand : Command
    {
        public OpenProjectCommand()
        {
        }

        public override bool Execute(object parameter = null)
        {
            var result = FileSystemService.OpenProject();
            if (result.success)
            {
                //TODO: Reuse LoadProjectCommand inside this here
                var session = ServiceLocator.Fetch<SessionService>();
                session.Clear();
                session.Project = result.project;
                session.WireEventHandlers();

                var prefService = ServiceLocator.Fetch<PreferencesService>();
                prefService.Preferences.LastProjectName = session.Project.Name;
                prefService.Preferences.LastProjectPath = session.Project.Path;
                return true;
            }
            return false;
        }
    }
}
