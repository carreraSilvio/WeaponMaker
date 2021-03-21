using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class LoadProjectCommand : Command
    {
        public LoadProjectCommand()
        {
        }

        public override bool Execute(object parameter = null)
        {
            var projectFilePath = parameter as string;
            var result = FileSystemService.LoadProject(projectFilePath);
            if (result.success)
            {
                var session = ServiceLocator.Fetch<SessionService>();
                session.Clear();
                session.Project = result.project;
                session.WireEventHandlers();
                

                return true;
            }
            return false;
        }
    }
}