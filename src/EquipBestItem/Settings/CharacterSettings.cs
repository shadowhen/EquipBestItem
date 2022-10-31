using System;
using System.Collections.Generic;

namespace EquipBestItem
{
    [Serializable]
    public class CharacterSettings
    {
        public string Name { get; set; }

        private List<FilterWeaponSettings> _filterWeapon;

        public List<FilterWeaponSettings> FilterWeapon
        {
            get
            {
                if (_filterWeapon == null)
                {
                    _filterWeapon = new List<FilterWeaponSettings>();
                }
                return _filterWeapon;
            }
        }

        private List<FilterArmorSettings> _filterArmor;

        public List<FilterArmorSettings> FilterArmor
        {
            get
            {
                if (_filterArmor == null)
                {
                    _filterArmor = new List<FilterArmorSettings>();
                }
                return _filterArmor;
            }
        }

        private FilterMountSettings _filterMount;
        public FilterMountSettings FilterMount
        {
            get
            {
                if (_filterMount == null)
                {
                    _filterMount = new FilterMountSettings();
                }
                return _filterMount;
            }
            set => _filterMount = value;
        }

        public enum ArmorSlot
        {
            Helm = 0,
            Cloak = 1,
            Armor = 2,
            Glove = 3,
            Boot = 4,
            Harness = 5
        }

        public enum WeaponSlot
        {
            Weapon1 = 0,
            Weapon2 = 1,
            Weapon3 = 2,
            Weapon4 = 3
        }

        public CharacterSettings()
        {

        }

        /// <summary>
        /// Constructor using name
        /// </summary>
        /// <param name="name">Character name</param>
        public CharacterSettings(string name)
        {
            Name = name;

            for (int i = 0; i < 4; i++)
            {
                FilterWeapon.Add(new FilterWeaponSettings());
            }

            for (int i = 0; i < 6; i++)
            {
                FilterArmor.Add(new FilterArmorSettings());
            }

            _filterMount = new FilterMountSettings();
        }

        /// <summary>
        /// Creates a copy of character settings from the other character settings
        /// </summary>
        /// <param name="other">Character settings</param>
        public CharacterSettings(CharacterSettings other)
        {
            // Should be the other character settings, do not run the rest of constructor
            // TODO: It might be better to invoke NullException than returning since
            // the constructor requires the character settings to be not null
            if (other == null) return;

            // Copies the name
            Name = other.Name;

            // Copies filters for weapons, armors, and mount
            for (int i = 0; i < 4; i++)
            {
                FilterWeapon.Add(new FilterWeaponSettings(other.FilterWeapon[i]));
            }
            for (int i = 0; i < 6; i++)
            {
                FilterArmor.Add(new FilterArmorSettings(other.FilterArmor[i]));
            }
            FilterMount = new FilterMountSettings(other.FilterMount);
        }
    }
}
