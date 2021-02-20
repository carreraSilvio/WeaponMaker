﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class NewProjectCommand
    {
        public class Args
        {
            public Window caller;
            public Type target;
        }

        public NewProjectCommand()
        {
        }

        public void Execute(object parameter)
        {
            var args = parameter as Args;
            var window = args.caller;
            var newProject = new Project();
            if (FileSystemService.SaveProject(newProject))
            {
                var session = ServiceLocator.Fetch<SessionService>();
                session.Project = newProject;
                var editWindow = new EditWindow(); //use type of target
                editWindow.Show();
                window.Close();
            }
        }
    }
}