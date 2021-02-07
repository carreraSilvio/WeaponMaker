using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class NewProjectCommand : ICommand
    {
        private Window _targetWindow;
        public Window TargetWindow => _targetWindow;

        public NewProjectCommand()
        {
        }

        public NewProjectCommand(Window targetWindow)
        {
            _targetWindow = targetWindow;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var window = parameter as Window;
            var newProject = new Project();
            if (FileSystemService.SaveProject(newProject))
            {
                window.Close();
                _targetWindow.Show();
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
