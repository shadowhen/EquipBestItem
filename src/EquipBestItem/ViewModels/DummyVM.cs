
using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    public class DummyVM : ViewModel
    {
        private MBBindingList<FilterAdjusterDummyVM> _list;
        private readonly MBBindingList<FilterAdjusterDummyVM> _armorFilterAdjustList;
        private readonly MBBindingList<FilterAdjusterDummyVM> _mountFilterAdjustList;
        private readonly MBBindingList<FilterAdjusterDummyVM> _weaponFilterAdjustList;

        private CharacterSettings _currentCharacterSettings;
        public CharacterSettings CurrentCharacterSettings
        {
            get => _currentCharacterSettings;
            set => _currentCharacterSettings = value;
        }

        public FilterInventorySlot CurrentState { get; private set; } = FilterInventorySlot.None;
        public List<Action<FilterInventorySlot>> UpdateActions { get; } = new List<Action<FilterInventorySlot>>();

        private bool _civilianOutfit;
        private string _currentCharacterName;

        private string _title;
        private bool _settingsHidden;
        private FilterCopyPasteVM _copyPasteVM;
        private DummyIconVM _iconVM;

        public DummyVM()
        {
            _armorFilterAdjustList = new MBBindingList<FilterAdjusterDummyVM>
            {
                new FilterAdjusterDummyVM() {Title = "Head Armor"},
                new FilterAdjusterDummyVM() {Title = "Body Armor"},
                new FilterAdjusterDummyVM() {Title = "Leg Armor"},
                new FilterAdjusterDummyVM() {Title = "Glove Armor"},
                new FilterAdjusterDummyVM() {Title = "Maneuver Bonus"},
                new FilterAdjusterDummyVM() {Title = "Speed Bonus"},
                new FilterAdjusterDummyVM() {Title = "Charge Bonus"},
                new FilterAdjusterDummyVM() {Title = "Armor Weight"}
            };
            _mountFilterAdjustList = new MBBindingList<FilterAdjusterDummyVM>()
            {
                new FilterAdjusterDummyVM() {Title = "Charge Damage"},
                new FilterAdjusterDummyVM() {Title = "Hit Points"},
                new FilterAdjusterDummyVM() {Title = "Maneuver"},
                new FilterAdjusterDummyVM() {Title = "Speed"}
            };
            _weaponFilterAdjustList = new MBBindingList<FilterAdjusterDummyVM>()
            {
                new FilterAdjusterDummyVM() {Title = "MaxDataPoints"},
                new FilterAdjusterDummyVM() {Title = "Thrust Speed"},
                new FilterAdjusterDummyVM() {Title = "Swing Speed"},
                new FilterAdjusterDummyVM() {Title = "Missile Speed"},
                new FilterAdjusterDummyVM() {Title = "Weapon Length"},
                new FilterAdjusterDummyVM() {Title = "Thrust Damage"},
                new FilterAdjusterDummyVM() {Title = "Swing Damage"},
                new FilterAdjusterDummyVM() {Title = "Accuracy"},
                new FilterAdjusterDummyVM() {Title = "Handling"},
                new FilterAdjusterDummyVM() {Title = "Weapon Weight"},
                new FilterAdjusterDummyVM() {Title = "Weapon Body Armor"}
            };

            foreach (var filter in _armorFilterAdjustList)
            {
                filter.ExecuteFilterAction = UpdateValues;
            }

            foreach (var filter in _mountFilterAdjustList)
            {
                filter.ExecuteFilterAction = UpdateValues;
            }

            foreach (var filter in _weaponFilterAdjustList)
            {
                filter.ExecuteFilterAction = UpdateValues;
            }

            // Hides the filter settings upon opening the inventory
            SettingsHidden = true;
            CopyPasteVM = new FilterCopyPasteVM(this);
            IconVM = new DummyIconVM(UpdateState);

            CurrentCharacterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(InventoryBehavior.Inventory.CurrentCharacterName);
            IconVM.UpdateIcons(_currentCharacterSettings);
        }

        public override void RefreshValues()
        {
            base.RefreshValues();

            if (_currentCharacterSettings != null)
            {
                bool civilianSet = !InventoryBehavior.Inventory.IsInWarSet;
                string characterName = InventoryBehavior.Inventory.CurrentCharacterName;

                if (civilianSet != _civilianOutfit || characterName != _currentCharacterName)
                {
                    UpdateState(CurrentState, true, (civilianSet != _civilianOutfit), SettingsHidden);
                }

            }
        }

        public void UpdateState(FilterInventorySlot state)
        {
            UpdateState(state, false, false, false);
        }
        private void UpdateState(FilterInventorySlot state, bool doNotHide = false, bool overrideCharacterCheck = false, bool doNotReveal = false)
        {
            // Leaves the function if the filter settings are not hidden and the state is the current one
            if (!SettingsHidden && CurrentState == state && !doNotHide)
            {
                SettingsHidden = true;
                return;
            }
            CurrentState = state;
            SettingsHidden = doNotReveal;

            // Check if our character matches with the correct character settings
            string currentCharacterName = InventoryBehavior.Inventory.CurrentCharacterName;
            if (CurrentCharacterSettings == null || currentCharacterName != CurrentCharacterSettings.Name || overrideCharacterCheck)
                CurrentCharacterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(currentCharacterName);
            _civilianOutfit = !InventoryBehavior.Inventory.IsInWarSet;
            _currentCharacterName = InventoryBehavior.Inventory.CurrentCharacterName;

            if (state == FilterInventorySlot.Horse)
            {
                SetupMountFilters();
            }
            else if (state >= FilterInventorySlot.Helm && state <= FilterInventorySlot.Boot)
            {
                SetupArmorFilters(state);
            } 
            else if (state >= FilterInventorySlot.Weapon1 && state <= FilterInventorySlot.Weapon4)
            {
                SetupWeaponFilters(state);
            }

            UpdateTitle();
            UpdateValues();

            foreach (var action in UpdateActions)
            {
                action.Invoke(state);
            }
        }

        private void SetupArmorFilters(FilterInventorySlot state)
        {
            var armorFilters = (FilterArmorSettings)CurrentCharacterSettings.GetFilter(state);
            _armorFilterAdjustList[0].ExecuteAction = delegate (float value)
            {
                armorFilters.HeadArmor = value;
            };
            _armorFilterAdjustList[1].ExecuteAction = delegate (float value)
            {
                armorFilters.ArmorBodyArmor = value;
            };
            _armorFilterAdjustList[2].ExecuteAction = delegate (float value)
            {
                armorFilters.LegArmor = value;
            };
            _armorFilterAdjustList[3].ExecuteAction = delegate (float value)
            {
                armorFilters.ArmArmor = value;
            };
            _armorFilterAdjustList[4].ExecuteAction = delegate (float value)
            {
                armorFilters.ManeuverBonus = value;
            };
            _armorFilterAdjustList[5].ExecuteAction = delegate (float value)
            {
                armorFilters.SpeedBonus = value;
            };
            _armorFilterAdjustList[6].ExecuteAction = delegate (float value)
            {
                armorFilters.ChargeBonus = value;
            };
            _armorFilterAdjustList[7].ExecuteAction = delegate (float value)
            {
                armorFilters.ArmorWeight = value;
            };
            List = _armorFilterAdjustList;
        }

        private void SetupMountFilters()
        {
            var mountFilters = (FilterMountSettings)CurrentCharacterSettings.GetFilter(FilterInventorySlot.Horse);
            _mountFilterAdjustList[0].ExecuteAction = delegate (float value)
            {
                mountFilters.ChargeDamage = value;
            };
            _mountFilterAdjustList[1].ExecuteAction = delegate (float value)
            {
                mountFilters.HitPoints = value;
            };
            _mountFilterAdjustList[2].ExecuteAction = delegate (float value)
            {
                mountFilters.Maneuver = value;
            };
            _mountFilterAdjustList[3].ExecuteAction = delegate (float value)
            {
                mountFilters.Speed = value;
            };
            List = _mountFilterAdjustList;
        }

        private void SetupWeaponFilters(FilterInventorySlot state)
        {
            var weaponFilters = (FilterWeaponSettings) _currentCharacterSettings.GetFilter(state);
            _weaponFilterAdjustList[0].ExecuteAction = delegate(float value)
            {
                weaponFilters.MaxDataValue = value;
            };
            _weaponFilterAdjustList[1].ExecuteAction = delegate (float value)
            {
                weaponFilters.ThrustSpeed = value;
            };
            _weaponFilterAdjustList[2].ExecuteAction = delegate (float value)
            {
                weaponFilters.SwingDamage = value;
            };
            _weaponFilterAdjustList[3].ExecuteAction = delegate (float value)
            {
                weaponFilters.MissileSpeed = value;
            };
            _weaponFilterAdjustList[4].ExecuteAction = delegate (float value)
            {
                weaponFilters.WeaponLength = value;
            };
            _weaponFilterAdjustList[5].ExecuteAction = delegate (float value)
            {
                weaponFilters.ThrustDamage = value;
            };
            _weaponFilterAdjustList[6].ExecuteAction = delegate (float value)
            {
                weaponFilters.SwingDamage = value;
            };
            _weaponFilterAdjustList[7].ExecuteAction = delegate (float value)
            {
                weaponFilters.Accuracy = value;
            };
            _weaponFilterAdjustList[8].ExecuteAction = delegate (float value)
            {
                weaponFilters.Handling = value;
            };
            _weaponFilterAdjustList[9].ExecuteAction = delegate (float value)
            {
                weaponFilters.WeaponWeight = value;
            };
            _weaponFilterAdjustList[10].ExecuteAction = delegate (float value)
            {
                weaponFilters.WeaponBodyArmor = value;
            };
            List = _weaponFilterAdjustList;
        }

        private void UpdateTitle()
        {
            switch (CurrentState)
            {
                case FilterInventorySlot.Helm:
                    Title = "Helm Filter";
                    break;
                case FilterInventorySlot.Cloak:
                    Title = "Cloak Filter";
                    break;
                case FilterInventorySlot.Body:
                    Title = "Body Filter";
                    break;
                case FilterInventorySlot.Gloves:
                    Title = "Gloves Filter";
                    break;
                case FilterInventorySlot.Boot:
                    Title = "Boot Filter";
                    break;
                case FilterInventorySlot.HorseHarness:
                    Title = "Horse Harness Filter";
                    break;
                case FilterInventorySlot.Weapon1:
                    Title = "Weapon 1 Filter";
                    break;
                case FilterInventorySlot.Weapon2:
                    Title = "Weapon 2 Filter";
                    break;
                case FilterInventorySlot.Weapon3:
                    Title = "Weapon 3 Filter";
                    break;
                case FilterInventorySlot.Weapon4:
                    Title = "Weapon 4 Filter";
                    break;
                case FilterInventorySlot.Horse:
                    Title = "Horse Filter";
                    break;
                default:
                    Title = "None";
                    break;
            }
        }

        public void UpdateValues()
        {
            var values = new List<float>();
            var state = CurrentState;
            
            if (state == FilterInventorySlot.Horse)
            {
                var mountSettings = (FilterMountSettings) _currentCharacterSettings.GetFilter(state);
                values.AddRange(new List<float>
                {
                    mountSettings.ChargeDamage,
                    mountSettings.HitPoints,
                    mountSettings.Maneuver,
                    mountSettings.Speed
                });
            }
            else if (state >= FilterInventorySlot.Helm && state <= FilterInventorySlot.Boot)
            {
                var armorSettings = (FilterArmorSettings) _currentCharacterSettings.GetFilter(state);
                values.AddRange(new List<float>
                {
                    armorSettings.HeadArmor,
                    armorSettings.ArmorBodyArmor,
                    armorSettings.LegArmor,
                    armorSettings.ArmArmor,
                    armorSettings.ManeuverBonus,
                    armorSettings.SpeedBonus,
                    armorSettings.ChargeBonus,
                    armorSettings.ArmorWeight
                });
            }
            else if (state >= FilterInventorySlot.Weapon1 && state <= FilterInventorySlot.Weapon4)
            {
                var weaponSettings = (FilterWeaponSettings)_currentCharacterSettings.GetFilter(state);
                values.AddRange(new List<float>
                {
                    weaponSettings.MaxDataValue,
                    weaponSettings.ThrustSpeed,
                    weaponSettings.SwingSpeed,
                    weaponSettings.MissileSpeed,
                    weaponSettings.WeaponLength,
                    weaponSettings.ThrustDamage,
                    weaponSettings.SwingDamage,
                    weaponSettings.Accuracy,
                    weaponSettings.Handling,
                    weaponSettings.WeaponWeight,
                    weaponSettings.WeaponBodyArmor
                });

            }

            for (int i = 0; i < values.Count; i++)
            {
                List[i].Value = values[i];
            }
            IconVM.UpdateIcons(_currentCharacterSettings);
        }

        [DataSourceProperty]
        public MBBindingList<FilterAdjusterDummyVM> List
        {
            get => _list;
            set
            {
                if (value == _list)
                    return;
                _list = value;
                OnPropertyChangedWithValue(value, nameof(List));
            }
        }

        [DataSourceProperty]
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title)
                    return;
                _title = value;
                OnPropertyChangedWithValue(value, nameof(Title));
            }
        }

        [DataSourceProperty]
        public bool SettingsHidden
        {
            get => _settingsHidden;
            set
            {
                if (value == _settingsHidden)
                    return;
                _settingsHidden = value;
                OnPropertyChangedWithValue(value, nameof(SettingsHidden));
            }
        }

        [DataSourceProperty]
        public FilterCopyPasteVM CopyPasteVM
        {
            get => _copyPasteVM;
            set
            {
                if (value == _copyPasteVM)
                    return;
                _copyPasteVM = value;
                OnPropertyChangedWithValue(value, nameof(CopyPasteVM));
            }
        }

        [DataSourceProperty]
        public DummyIconVM IconVM
        {
            get => _iconVM;
            set
            {
                if (value == _iconVM)
                    return;
                _iconVM = value;
                OnPropertyChangedWithValue(value, nameof(IconVM));
            }
        }

        private void ExecuteHideSettings()
        {
            SettingsHidden = true;
        }

        private void ExecuteDefaultValues()
        {
            CurrentCharacterSettings.GetFilter(CurrentState).Clear();
            UpdateValues();
        }

        private void ExecuteLockValues()
        {
            CurrentCharacterSettings.GetFilter(CurrentState).ClearZero();
            UpdateValues();
        }
    }
}
