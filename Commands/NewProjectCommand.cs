using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var newProject = new Project() 
            {
                Name = "Project Name",
                Weapons = new ObservableCollection<Weapon>
                {
                    new Weapon()
                    {
                        Name = "Pistol"
                    },
                    new Weapon()
                    {
                        Name = "Shotgun"
                    }
                }
            };
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
