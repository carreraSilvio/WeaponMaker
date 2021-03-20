using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponMaker
{
    public class Preferences : INotifyPropertyChanged
    {
        private bool _loadLastProjectOnStartUp;

        public bool LoadLastProjectOnStartUp
        {
            get => _loadLastProjectOnStartUp;
            set
            {
                _loadLastProjectOnStartUp = value;
                RaisePropertyChanged(nameof(LoadLastProjectOnStartUp));
            }
        }

        public string LastProjectName { get; set; }
        public string LastProjectPath { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
