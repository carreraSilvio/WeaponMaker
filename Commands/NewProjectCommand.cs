using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class NewProjectCommand : Command
    {
        public NewProjectCommand()
        {
        }

        public override bool Execute(object parameter = null)
        {
            var newProject = new Project();
            if (FileSystemService.SaveProjectAs(newProject))
            {
                var session = ServiceLocator.Fetch<SessionService>();
                session.Project = newProject;
                return true;
            }
            return false;
        }
    }
}
