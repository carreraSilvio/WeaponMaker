using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace WeaponMaker
{
    /// <summary>
    /// Represents a weapon maker project
    /// </summary>
    public class Project
    {
        private string name;
        private List<Weapon> _weapons;
        private List<string> _weaponTypes;

        public string Name { get => name; set => name = value; }
    }
}
