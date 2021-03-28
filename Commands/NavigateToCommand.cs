using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class NavigateToCommand : Command
    {
        public class Args
        {
            public Window currentWindow;
            public Type target;
        }

        public override bool Execute(object parameter)
        {
            var args = parameter as Args;
            var window = args.currentWindow;
            var nextWindow = new MainWindow();
            nextWindow.Show();
            window.Close();

            return true;
        }
    }
}
