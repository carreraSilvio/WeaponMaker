using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class SaveProjectCommand : Command
    {
        public SaveProjectCommand()
        {
        }

        public override bool Execute(object parameter = null)
        {
            var sessionService = ServiceLocator.Fetch<SessionService>();
            var project = sessionService.Project;
            sessionService.IsProjectModified = false;
            project.LastTimeSaved = DateTime.UtcNow.ToString("yyyy-MM-dd_hh:mm:ss");
            return FileSystemService.SaveProject(project);
        }
    }
}
