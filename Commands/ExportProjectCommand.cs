using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class ExportProjectCommand : Command
    {
        public override bool Execute(object parameter = null)
        {
            var project = parameter as Project;
            return FileSystemService.ExportAsJson(project, project.Name);
        }
    }
}
