using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace WeaponMaker
{
    public sealed class ShutdownCommand : Command
    {
        private readonly SessionService _sessionService;
        private readonly SaveProjectCommand _saveProjectCommand;

        public ShutdownCommand(SessionService sessionService)
        {
            _sessionService = sessionService;
            _saveProjectCommand = new SaveProjectCommand();
        }

        public override bool Execute(object parameter = null)
        {
            if (_sessionService.IsProjectModified)
            {
                var messageBoxResult = MessageBox.Show(
                    "Do you want to save the changes you made? \nChanges will be lost if you don't save them.",
                    "Project has been modified", 
                    System.Windows.MessageBoxButton.YesNoCancel);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _saveProjectCommand.Execute();
                }
                else if (messageBoxResult == MessageBoxResult.Cancel)
                {
                    return false;
                }
            }
            Application.Current.Shutdown();
            
            return true;
        }
    }
}
