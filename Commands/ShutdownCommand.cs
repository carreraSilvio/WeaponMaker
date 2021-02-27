using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class ShutdownCommand : Command
    {
        public ShutdownCommand()
        {
        }

        public override bool Execute(object parameter = null)
        {
            Application.Current.Shutdown();
            return true;
        }
    }
}
