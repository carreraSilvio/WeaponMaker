using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class ImportWeaponCommand : Command
    {
        public override bool Execute(object parameter = null)
        {
            var result = FileSystemService.ImportWeapon();
            if (result.success)
            {
                var session = ServiceLocator.Fetch<SessionService>();
                session.CurrentWeapon.Copy(result.weapon);
                return true;
            }
            return false;
        }
    }
}
