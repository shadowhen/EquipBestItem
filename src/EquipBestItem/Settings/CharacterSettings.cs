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

        private Dictionary<FilterInventorySlot, IFilterSettings> _filterSettingDictionary;

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

        /*
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
        */

        public CharacterSettings()
        {

        }

        /// <summary>
        /// Constructor using name
        /// </summary>
        /// <param name="name">character name</param>
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

            CreateFilterDictionary();
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">character settings</param>
        public CharacterSettings(CharacterSettings other)
        {
            if (other == null) return;

            Name = other.Name;

            for (int i = 0; i < 4; i++)
            {
                FilterWeapon.Add(new FilterWeaponSettings(other.FilterWeapon[i]));
            }

            for (int i = 0; i < 6; i++)
            {
                FilterArmor.Add(new FilterArmorSettings(other.FilterArmor[i]));
            }

            FilterMount = new FilterMountSettings(other.FilterMount);
            
            CreateFilterDictionary();
        }

        public IFilterSettings GetFilter(FilterInventorySlot slot)
        {
            if (_filterSettingDictionary == null)
                CreateFilterDictionary();
            return _filterSettingDictionary[slot];
        }

        public void SetFilter(FilterInventorySlot slot, IFilterSettings filter)
        {
            _filterSettingDictionary[slot] = filter;
        }

        private void CreateFilterDictionary()
        {
            _filterSettingDictionary = new Dictionary<FilterInventorySlot, IFilterSettings>
            {
                {FilterInventorySlot.Weapon1, FilterWeapon[0]},
                {FilterInventorySlot.Weapon2, FilterWeapon[1]},
                {FilterInventorySlot.Weapon3, FilterWeapon[2]},
                {FilterInventorySlot.Weapon4, FilterWeapon[3]},
                {FilterInventorySlot.Helm, FilterArmor[0]},
                {FilterInventorySlot.Cloak, FilterArmor[1]},
                {FilterInventorySlot.Body, FilterArmor[2]},
                {FilterInventorySlot.Gloves, FilterArmor[3]},
                {FilterInventorySlot.Boot, FilterArmor[4]},
                {FilterInventorySlot.HorseHarness, FilterArmor[5]},
                {FilterInventorySlot.Horse, FilterMount}
            };
        }
    }
}
