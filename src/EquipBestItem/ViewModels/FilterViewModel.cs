
using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    public class FilterViewModel : ViewModel
    {
        private MBBindingList<FilterAdjusterVM> _list;
        private readonly MBBindingList<FilterAdjusterVM> _armorFilterAdjustList;
        private readonly MBBindingList<FilterAdjusterVM> _mountFilterAdjustList;
        private readonly MBBindingList<FilterAdjusterVM> _weaponFilterAdjustList;

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
        private FilterIconsVM _iconsVm;

        private static readonly string[] ArmorAdjustTitles =
        {
            "Head Armor",
            "Body Armor",
            "Leg Armor",
            "Glove Armor",
            "Maneuver Bonus",
            "Speed Bonus",
            "Charge Bonus",
            "Armor Weight",
        };

        private static readonly string[] MountAdjustTitles =
        {
            "Charge Damage",
            "Hit Points",
            "Maneuver",
            "Speed"
        };

        private static readonly string[] WeaponAdjustTitle =
        {
            "MaxDataPoints",
            "Thrust Speed",
            "Swing Speed",
            "Missile Speed",
            "Weapon Length",
            "Thrust Damage",
            "Swing Damage",
            "Accuracy",
            "Handling",
            "Weapon Weight",
            "Weapon Body Armor"
        };

        public FilterViewModel()
        {
            _armorFilterAdjustList = new MBBindingList<FilterAdjusterVM>();
            _mountFilterAdjustList = new MBBindingList<FilterAdjusterVM>();
            _weaponFilterAdjustList = new MBBindingList<FilterAdjusterVM>();

            foreach (var title in ArmorAdjustTitles)
            {
                var adjuster = new FilterAdjusterVM {Title = title, ExecuteFilterAction = UpdateValues};
                _armorFilterAdjustList.Add(adjuster);
            }
            foreach (var title in MountAdjustTitles)
            {
                var adjuster = new FilterAdjusterVM { Title = title, ExecuteFilterAction = UpdateValues };
                _mountFilterAdjustList.Add(adjuster);
            }
            foreach (var title in WeaponAdjustTitle)
            {
                var adjuster = new FilterAdjusterVM { Title = title, ExecuteFilterAction = UpdateValues };
                _weaponFilterAdjustList.Add(adjuster);
            }

            // Hides the filter settings upon opening the inventory
            SettingsHidden = true;
            CopyPasteVM = new FilterCopyPasteVM(this);
            IconsVM = new FilterIconsVM((FilterInventorySlot state) => { UpdateState(state); });

            CurrentCharacterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(InventoryBehavior.Inventory.CurrentCharacterName);
            //CurrentCharacterSettings = InventoryBehavior.GetCurrentCharacterSettings();

            IconsVM.UpdateIcons(_currentCharacterSettings);
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
            IconsVM.UpdateIcons(_currentCharacterSettings);
        }

        [DataSourceProperty]
        public MBBindingList<FilterAdjusterVM> List
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
        public FilterIconsVM IconsVM
        {
            get => _iconsVm;
            set
            {
                if (value == _iconsVm)
                    return;
                _iconsVm = value;
                OnPropertyChangedWithValue(value, nameof(IconsVM));
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
