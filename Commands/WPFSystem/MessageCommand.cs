using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace WeaponMaker
{
    public class MessageCommand : ICommand
    {
        public void Execute(object parameter)
        {
            string msg = parameter != null ? parameter.ToString() : "Message";
            MessageBox.Show(msg);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
