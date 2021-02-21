using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class ExportWeaponCommand : Command
    {
        public ExportWeaponCommand()
        {

        }

        public override bool Execute(object parameter = null)
        {
            var weapon = parameter as Weapon;
            return FileSystemService.ExportAsJson(weapon, weapon.Name);
        }
    }
}
