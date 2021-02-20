using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeaponMaker
{
    public class CommandService : IService
    {
        private static Dictionary<Type, Command> _commands = new Dictionary<Type, Command>();

        public void AddRange<T>(IEnumerable<T> commands)
        {
            foreach(var command in commands)
            {
                _commands.Add(command.GetType(), command as Command);
            }
        }

        public void Add(Command command)
        {
            _commands.Add(command.GetType(), command);
        }

        public T Get<T>() where T : Command
        {
            return (T)_commands[typeof(T)];
        }
    }
}
