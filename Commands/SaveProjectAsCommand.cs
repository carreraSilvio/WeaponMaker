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
        public class Args
        {
            public Window caller;
            public Type target;
        }

        public SaveProjectAsCommand()
        {
        }

        public override bool Execute(object parameter)
        {
            var args = parameter as Args;
            var window = args.caller;
            var result = FileSystemService.OpenProject();
            if (result.success)
            {
                var session = ServiceLocator.Fetch<SessionService>();
                session.Project = result.project;
                return true;
            }
            return false;
        }
    }
}
