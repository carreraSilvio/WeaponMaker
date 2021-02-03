using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace WeaponMaker
{
    /// <summary>
    /// Represents the whole set of weappon data for a game
    /// </summary>
    public class Project
    {
        private string name;
        private List<Weapon> _weapons;
        private List<string> _weaponTypes;
    }
}
