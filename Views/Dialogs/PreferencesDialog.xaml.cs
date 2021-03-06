﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace WeaponMaker
{
    /// <summary>
    /// Interaction logic for PreferencesDialog.xaml
    /// </summary>
    public partial class PreferencesDialog : Window
    {
        private readonly PreferencesService _preferencesService;

        public bool LoadLastProjectOnStartUp
        {
            get => _preferencesService.Preferences.LoadLastProjectOnStartUp;
            set => _preferencesService.Preferences.LoadLastProjectOnStartUp = value;
        }

        public PreferencesDialog()
        {
            _preferencesService = ServiceLocator.Fetch<PreferencesService>();
            Closing += PreferencesDialog_Closing;

            InitializeComponent();
        }

        internal void PreferencesDialog_Closing(object sender, CancelEventArgs e)
        {
            FileSystemService.SaveToJson(_preferencesService.Preferences, _preferencesService.PreferencesFilePath);
        }
    }
}
