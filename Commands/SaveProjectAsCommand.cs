using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class SaveProjectAsCommand : Command
    {
        public SaveProjectAsCommand()
        {
        }

        public override bool Execute(object parameter = null)
        {
            var project = ServiceLocator.Fetch<SessionService>().Project;
            return FileSystemService.SaveProjectAs(project);
        }
    }
}