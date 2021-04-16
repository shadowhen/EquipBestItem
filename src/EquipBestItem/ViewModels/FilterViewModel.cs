using System.Collections.Generic;
using EquipBestItem.ViewModels;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem
{
    class FilterViewModel : ViewModel
    {
        private CharacterSettings _characterSettings;

        private FilterInventorySlot _currentInventorySlot;

        private int _incrementValue = 1;
        private bool _incrementBy10;
        public bool IncrementBy10
        {
            get => _incrementBy10;
            set
            {
                _incrementBy10 = value;
                _incrementValue = _incrementBy10 && !_incrementBy5 ? 10 : (!_incrementBy10 && !_incrementBy5 ? 1 : _incrementValue);
            }
        }

        private bool _incrementBy5;

        public bool IncrementBy5
        {
            get => _incrementBy5;
            set
            {
                _incrementBy5 = value;
                _incrementValue = !_incrementBy10 && _incrementBy5 ? 5 : (!_incrementBy10 && !_incrementBy5 ? 1 : _incrementValue);
            }
        }

        private enum FilterItemState { None, Armor, Weapon, Mount }
        private FilterItemState _filterItemState = FilterItemState.None;

        private FilterArmorSettings _clipboardFilterArmorSettings;
        private FilterWeaponSettings _clipboardFilterWeaponSettings;
        private FilterMountSettings _clipboardFilterMountSettings;
        private CharacterSettings _clipboardCharacterSettings;

        private bool _pastedCharacterSettings = false;

        private FilterAdjusterVM _armorHelmAdjuster;
        private FilterAdjusterVM _armorBodyAdjuster;
        private FilterAdjusterVM _armorBootAdjuster;
        private FilterAdjusterVM _armorGlovesAdjuster;
        private FilterAdjusterVM _armorManeuverAdjuster;
        private FilterAdjusterVM _armorSpeedAdjuster;
        private FilterAdjusterVM _armorChargeAdjuster;
        private FilterAdjusterVM _armorWeightAdjuster;

        private FilterAdjusterVM _mountChargeDamageAdjuster;
        private FilterAdjusterVM _mountHitPointsAdjuster;
        private FilterAdjusterVM _mountManeuverAdjuster;
        private FilterAdjusterVM _mountSpeedAdjuster;

        private List<FilterAdjusterVM> _armorFilterAdjusterList;
        private List<FilterAdjusterVM> _mountFilterAdjusterList;

        [DataSourceProperty]
        public FilterAdjusterVM ArmorHelmAdjuster
        {
            get => _armorHelmAdjuster;
            set
            {
                if (value == ArmorHelmAdjuster)
                    return;
                _armorHelmAdjuster = value;
                OnPropertyChanged(nameof(ArmorHelmAdjuster));
            }
        }

        [DataSourceProperty]
        public FilterAdjusterVM ArmorBodyAdjuster
        {
            get => _armorBodyAdjuster;
            set
            {
                if (value == ArmorBodyAdjuster)
                    return;
                _armorBodyAdjuster = value;
                OnPropertyChanged(nameof(ArmorBodyAdjuster));
            }
        }

        [DataSourceProperty]
        public FilterAdjusterVM ArmorBootAdjuster
        {
            get => _armorBootAdjuster;
            set
            {
                if (value == ArmorBootAdjuster)
                    return;
                _armorBootAdjuster = value;
                OnPropertyChanged(nameof(ArmorBootAdjuster));
            }
        }

        [DataSourceProperty]
        public FilterAdjusterVM ArmorGlovesAdjuster
        {
            get => _armorGlovesAdjuster;
            set
            {
                if (value == ArmorGlovesAdjuster)
                    return;
                _armorGlovesAdjuster = value;
                OnPropertyChanged(nameof(ArmorGlovesAdjuster));
            }
        }

        [DataSourceProperty]
        public FilterAdjusterVM ArmorManeuverAdjuster
        {
            get => _armorManeuverAdjuster;
            set
            {
                if (value == ArmorManeuverAdjuster)
                    return;
                _armorManeuverAdjuster = value;
                OnPropertyChanged(nameof(ArmorManeuverAdjuster));
            }
        }

        [DataSourceProperty]
        public FilterAdjusterVM ArmorSpeedAdjuster
        {
            get => _armorSpeedAdjuster;
            set
            {
                if (value == ArmorSpeedAdjuster)
                    return;
                _armorSpeedAdjuster = value;
                OnPropertyChanged(nameof(ArmorSpeedAdjuster));
            }
        }

        [DataSourceProperty]
        public FilterAdjusterVM ArmorChargeAdjuster
        {
            get => _armorChargeAdjuster;
            set
            {
                if (value == ArmorChargeAdjuster)
                    return;
                _armorChargeAdjuster = value;
                OnPropertyChanged(nameof(ArmorChargeAdjuster));
            }
        }

        [DataSourceProperty]
        public FilterAdjusterVM ArmorWeightAdjuster
        {
            get => _armorWeightAdjuster;
            set
            {
                if (value == ArmorWeightAdjuster)
                    return;
                _armorWeightAdjuster = value;
                OnPropertyChanged(nameof(ArmorWeightAdjuster));
            }
        }

        #region DataSourcePropertys
        private bool _isHelmFilterSelected;

        [DataSourceProperty]
        public bool IsHelmFilterSelected
        {
            get => _isHelmFilterSelected;
            set
            {
                if (_isHelmFilterSelected != value)
                {
                    _isHelmFilterSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isHelmFilterLocked;

        [DataSourceProperty]
        public bool IsHelmFilterLocked
        {
            get => _isHelmFilterLocked;
            set
            {
                if (_isHelmFilterLocked != value)
                {
                    _isHelmFilterLocked = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isCloakFilterSelected;

        [DataSourceProperty]
        public bool IsCloakFilterSelected
        {
            get => _isCloakFilterSelected;
            set
            {
                if (_isCloakFilterSelected != value)
                {
                    _isCloakFilterSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isCloakFilterLocked;

        [DataSourceProperty]
        public bool IsCloakFilterLocked
        {
            get => _isCloakFilterLocked;
            set
            {
                if (_isCloakFilterLocked != value)
                {
                    _isCloakFilterLocked = value;
                    OnPropertyChanged();
                }
            }
        }



        private bool _isArmorFilterSelected;

        [DataSourceProperty]
        public bool IsArmorFilterSelected
        {
            get => _isArmorFilterSelected;
            set
            {
                if (_isArmorFilterSelected != value)
                {
                    _isArmorFilterSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isArmorFilterLocked;

        [DataSourceProperty]
        public bool IsArmorFilterLocked
        {
            get => _isArmorFilterLocked;
            set
            {
                if (_isArmorFilterLocked != value)
                {
                    _isArmorFilterLocked = value;
                    OnPropertyChanged();
                }
            }
        }



        private bool _isGloveFilterSelected;

        [DataSourceProperty]
        public bool IsGloveFilterSelected
        {
            get => _isGloveFilterSelected;
            set
            {
                if (_isGloveFilterSelected != value)
                {
                    _isGloveFilterSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isGloveFilterLocked;

        [DataSourceProperty]
        public bool IsGloveFilterLocked
        {
            get => _isGloveFilterLocked;
            set
            {
                if (_isGloveFilterLocked != value)
                {
                    _isGloveFilterLocked = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isBootFilterSelected;

        [DataSourceProperty]
        public bool IsBootFilterSelected
        {
            get => _isBootFilterSelected;
            set
            {
                if (_isBootFilterSelected != value)
                {
                    _isBootFilterSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isBootFilterLocked;

        [DataSourceProperty]
        public bool IsBootFilterLocked
        {
            get => _isBootFilterLocked;
            set
            {
                if (_isBootFilterLocked != value)
                {
                    _isBootFilterLocked = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isMountFilterSelected;

        [DataSourceProperty]
        public bool IsMountFilterSelected
        {
            get => _isMountFilterSelected;
            set
            {
                if (_isMountFilterSelected != value)
                {
                    _isMountFilterSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isMountFilterLocked;

        [DataSourceProperty]
        public bool IsMountFilterLocked
        {
            get => _isMountFilterLocked;
            set
            {
                if (_isMountFilterLocked != value)
                {
                    _isMountFilterLocked = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isHarnessFilterSelected;

        [DataSourceProperty]
        public bool IsHarnessFilterSelected
        {
            get => _isHarnessFilterSelected;
            set
            {
                if (_isHarnessFilterSelected != value)
                {
                    _isHarnessFilterSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isHarnessFilterLocked;

        [DataSourceProperty]
        public bool IsHarnessFilterLocked
        {
            get => _isHarnessFilterLocked;
            set
            {
                if (_isHarnessFilterLocked != value)
                {
                    _isHarnessFilterLocked = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon1FilterSelected;

        [DataSourceProperty]
        public bool IsWeapon1FilterSelected
        {
            get => _isWeapon1FilterSelected;
            set
            {
                if (_isWeapon1FilterSelected != value)
                {
                    _isWeapon1FilterSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon1FilterLocked;

        [DataSourceProperty]
        public bool IsWeapon1FilterLocked
        {
            get => _isWeapon1FilterLocked;
            set
            {
                if (_isWeapon1FilterLocked != value)
                {
                    _isWeapon1FilterLocked = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon2FilterSelected;

        [DataSourceProperty]
        public bool IsWeapon2FilterSelected
        {
            get => _isWeapon2FilterSelected;
            set
            {
                if (_isWeapon2FilterSelected != value)
                {
                    _isWeapon2FilterSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon2FilterLocked;

        [DataSourceProperty]
        public bool IsWeapon2FilterLocked
        {
            get => _isWeapon2FilterLocked;
            set
            {
                if (_isWeapon2FilterLocked != value)
                {
                    _isWeapon2FilterLocked = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon3FilterSelected;

        [DataSourceProperty]
        public bool IsWeapon3FilterSelected
        {
            get => _isWeapon3FilterSelected;
            set
            {
                if (_isWeapon3FilterSelected != value)
                {
                    _isWeapon3FilterSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon3FilterLocked;

        [DataSourceProperty]
        public bool IsWeapon3FilterLocked
        {
            get => _isWeapon3FilterLocked;
            set
            {
                if (_isWeapon3FilterLocked != value)
                {
                    _isWeapon3FilterLocked = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon4FilterSelected;

        [DataSourceProperty]
        public bool IsWeapon4FilterSelected
        {
            get => _isWeapon4FilterSelected;
            set
            {
                if (_isWeapon4FilterSelected != value)
                {
                    _isWeapon4FilterSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon4FilterLocked;

        [DataSourceProperty]
        public bool IsWeapon4FilterLocked
        {
            get => _isWeapon4FilterLocked;
            set
            {
                if (_isWeapon4FilterLocked != value)
                {
                    _isWeapon4FilterLocked = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isLayerHidden;

        [DataSourceProperty]
        public bool IsLayerHidden
        {
            get => _isLayerHidden;
            set
            {
                if (_isLayerHidden != value)
                {
                    _isLayerHidden = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isMountSlotHidden = true;

        [DataSourceProperty]
        public bool IsMountSlotHidden
        {
            get => _isMountSlotHidden;
            set
            {
                if (_isMountSlotHidden != value)
                {
                    _isMountSlotHidden = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isArmorSlotHidden = true;

        [DataSourceProperty]
        public bool IsArmorSlotHidden
        {
            get => _isArmorSlotHidden;
            set
            {
                if (_isArmorSlotHidden != value)
                {
                    _isArmorSlotHidden = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeaponSlotHidden = true;

        [DataSourceProperty]
        public bool IsWeaponSlotHidden
        {
            get => _isWeaponSlotHidden;
            set
            {
                if (_isWeaponSlotHidden != value)
                {
                    _isWeaponSlotHidden = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _title;

        [DataSourceProperty]
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isHiddenFilterLayer = true;

        [DataSourceProperty]
        public bool IsHiddenFilterLayer
        {
            get => _isHiddenFilterLayer;
            set
            {
                if (_isHiddenFilterLayer != value)
                {
                    _isHiddenFilterLayer = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _clipboardFilterArmorSettingsCopied;

        [DataSourceProperty]
        public bool IsFilterArmorSettingsCopied
        {
            get => _clipboardFilterArmorSettingsCopied;
            private set
            {
                _clipboardFilterArmorSettingsCopied = value;
                OnPropertyChanged();
            }
        }

        private bool _clipboardFilterWeaponSettingsCopied;
        [DataSourceProperty]
        public bool IsFilterWeaponSettingsCopied
        {
            get => _clipboardFilterWeaponSettingsCopied;
            private set
            {
                _clipboardFilterWeaponSettingsCopied = value;
                OnPropertyChanged();
            }
        }

        private bool _clipboardFilterMountSettingsCopied;
        [DataSourceProperty]
        public bool IsFilterMountSettingsCopied
        {
            get => _clipboardFilterMountSettingsCopied;
            private set
            {
                _clipboardFilterMountSettingsCopied = value;
                OnPropertyChanged();
            }
        }

        private bool _clipboardCharacterSettingsCopied;
        [DataSourceProperty]
        public bool IsCharacterSettingsCopied
        {
            get => _clipboardCharacterSettingsCopied;
            private set
            {
                _clipboardCharacterSettingsCopied = value;
                OnPropertyChanged();
            }
        }

        private string _swingDamage;
        [DataSourceProperty]
        public string SwingDamage
        {
            get => _swingDamage;
            set
            {
                if (_swingDamage != value)
                {
                    _swingDamage = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _swingSpeed;
        [DataSourceProperty]
        public string SwingSpeed
        {
            get => _swingSpeed;
            set
            {
                if (_swingSpeed != value)
                {
                    _swingSpeed = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _thrustDamage;
        [DataSourceProperty]
        public string ThrustDamage
        {
            get => _thrustDamage;
            set
            {
                if (_thrustDamage != value)
                {
                    _thrustDamage = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _thrustSpeed;
        [DataSourceProperty]
        public string ThrustSpeed
        {
            get => _thrustSpeed;
            set
            {
                if (_thrustSpeed != value)
                {
                    _thrustSpeed = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _weaponLength;
        [DataSourceProperty]
        public string WeaponLength
        {
            get => _weaponLength;
            set
            {
                if (_weaponLength != value)
                {
                    _weaponLength = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _handling;
        [DataSourceProperty]
        public string Handling
        {
            get => _handling;
            set
            {
                if (_handling != value)
                {
                    _handling = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _weaponWeight;
        [DataSourceProperty]
        public string WeaponWeight
        {
            get => _weaponWeight;
            set
            {
                if (_weaponWeight != value)
                {
                    _weaponWeight = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _accuracy;
        [DataSourceProperty]
        public string Accuracy
        {
            get => _accuracy;
            set
            {
                if (_accuracy != value)
                {
                    _accuracy = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _missileSpeed;
        [DataSourceProperty]
        public string MissileSpeed
        {
            get => _missileSpeed;
            set
            {
                if (_missileSpeed != value)
                {
                    _missileSpeed = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _weaponBodyArmor;
        [DataSourceProperty]
        public string WeaponBodyArmor
        {
            get => _weaponBodyArmor;
            set
            {
                if (_weaponBodyArmor != value)
                {
                    _weaponBodyArmor = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxDataValue;

        [DataSourceProperty]
        public string MaxDataValue
        {
            get => _maxDataValue;
            set
            {
                if (_maxDataValue != value)
                {
                    _maxDataValue = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataSourceProperty]
        public FilterAdjusterVM MountChargeDamageAdjuster
        {
            get => _mountChargeDamageAdjuster;
            set
            {
                if (value == MountChargeDamageAdjuster)
                    return;
                _mountChargeDamageAdjuster = value;
                OnPropertyChanged(nameof(MountChargeDamageAdjuster));
            }
        }

        [DataSourceProperty]
        public FilterAdjusterVM MountHitPointsAdjuster
        {
            get => _mountHitPointsAdjuster;
            set
            {
                if (value == MountHitPointsAdjuster)
                    return;
                _mountHitPointsAdjuster = value;
                OnPropertyChanged(nameof(MountHitPointsAdjuster));
            }
        }

        [DataSourceProperty]
        public FilterAdjusterVM MountManeuverAdjuster
        {
            get => _mountManeuverAdjuster;
            set
            {
                if (value == MountManeuverAdjuster)
                    return;
                _mountManeuverAdjuster = value;
                OnPropertyChanged(nameof(MountManeuverAdjuster));
            }
        }

        [DataSourceProperty]
        public FilterAdjusterVM MountSpeedAdjuster
        {
            get => _mountSpeedAdjuster;
            set
            {
                if (value == MountSpeedAdjuster)
                    return;
                _mountSpeedAdjuster = value;
                OnPropertyChanged(nameof(MountSpeedAdjuster));
            }
        }

        #endregion DataSourceProperties

        public FilterViewModel()
        {
            _armorHelmAdjuster = CreateAdjusterVM(ref _armorFilterAdjusterList);
            _armorBodyAdjuster = CreateAdjusterVM(ref _armorFilterAdjusterList);
            _armorBootAdjuster = CreateAdjusterVM(ref _armorFilterAdjusterList);
            _armorGlovesAdjuster = CreateAdjusterVM(ref _armorFilterAdjusterList);
            _armorSpeedAdjuster = CreateAdjusterVM(ref _armorFilterAdjusterList);
            _armorChargeAdjuster = CreateAdjusterVM(ref _armorFilterAdjusterList);
            _armorManeuverAdjuster = CreateAdjusterVM(ref _armorFilterAdjusterList);
            _armorWeightAdjuster = CreateAdjusterVM(ref _armorFilterAdjusterList);

            _mountChargeDamageAdjuster = CreateAdjusterVM(ref _mountFilterAdjusterList);
            _mountHitPointsAdjuster = CreateAdjusterVM(ref _mountFilterAdjusterList);
            _mountManeuverAdjuster = CreateAdjusterVM(ref _mountFilterAdjusterList);
            _mountSpeedAdjuster = CreateAdjusterVM(ref _mountFilterAdjusterList);

            this.RefreshValues();
        }

        private FilterAdjusterVM CreateAdjusterVM(ref List<FilterAdjusterVM> list)
        {
            var temp = new FilterAdjusterVM();
            if (list == null)
                list = new List<FilterAdjusterVM>();
            list.Add(temp);
            return temp;
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            try
            {
                SetupFilterProfile();
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
                throw;
            }

            // Updates the title whether we are looking at weapon, armor, or mount filter settings window
            UpdateInformationValues();
            UpdateIcons();

#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage("FilterVMRefreshValue")); 
#endif
        }

        private void SetupFilterProfile()
        {
            if (!_pastedCharacterSettings)
            {
                _characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(InventoryBehavior.Inventory.CurrentCharacterName);
            }
            else
            {
                SettingsLoader.Instance.SetCharacterSettingsByName(_characterSettings, InventoryBehavior.Inventory.CurrentCharacterName);
                _pastedCharacterSettings = false;
            }

            SetupArmorAdjusters();
            SetupMountAdjusters();
        }

        private void SetupArmorAdjusters()
        {
            if (_currentInventorySlot >= FilterInventorySlot.Weapon1)
                return;

            _armorHelmAdjuster.ExecuteAction = delegate(float value)
            {
                ((FilterArmorSettings)_characterSettings.GetFilter(_currentInventorySlot)).HeadArmor += value;
            };
            _armorBodyAdjuster.ExecuteAction = delegate(float value)
            {
                ((FilterArmorSettings)_characterSettings.GetFilter(_currentInventorySlot)).ArmorBodyArmor += value;
            };
            _armorBootAdjuster.ExecuteAction = delegate (float value)
            {
                ((FilterArmorSettings)_characterSettings.GetFilter(_currentInventorySlot)).LegArmor += value;
            };
            _armorGlovesAdjuster.ExecuteAction = delegate (float value)
            {
                ((FilterArmorSettings)_characterSettings.GetFilter(_currentInventorySlot)).ArmArmor += value;
            };
            _armorManeuverAdjuster.ExecuteAction = delegate (float value)
            {
                ((FilterArmorSettings)_characterSettings.GetFilter(_currentInventorySlot)).ManeuverBonus += value;
            };
            _armorSpeedAdjuster.ExecuteAction = delegate (float value)
            {
                ((FilterArmorSettings)_characterSettings.GetFilter(_currentInventorySlot)).SpeedBonus += value;
            };
            _armorChargeAdjuster.ExecuteAction = delegate (float value)
            {
                ((FilterArmorSettings)_characterSettings.GetFilter(_currentInventorySlot)).ChargeBonus += value;
            };
            _armorWeightAdjuster.ExecuteAction = delegate (float value)
            {
                ((FilterArmorSettings)_characterSettings.GetFilter(_currentInventorySlot)).ArmorWeight += value;
            };
        }

        private void SetupMountAdjusters()
        {
            _mountChargeDamageAdjuster.ExecuteAction = delegate (float value)
            {
                ((FilterMountSettings)_characterSettings.GetFilter(FilterInventorySlot.Horse)).ChargeDamage += value;
            };
            _mountHitPointsAdjuster.ExecuteAction = delegate (float value)
            {
                ((FilterMountSettings)_characterSettings.GetFilter(FilterInventorySlot.Horse)).HitPoints += value;
            };
            _mountManeuverAdjuster.ExecuteAction = delegate (float value)
            {
                ((FilterMountSettings)_characterSettings.GetFilter(FilterInventorySlot.Horse)).Maneuver += value;
            };
            _mountSpeedAdjuster.ExecuteAction = delegate (float value)
            {
                ((FilterMountSettings)_characterSettings.GetFilter(FilterInventorySlot.Horse)).Speed += value;
            };
        }

        private void UpdateInformationValues()
        {
            if (!IsWeaponSlotHidden)
            {
                int slotNumber;
                switch (_currentInventorySlot)
                {
                    case FilterInventorySlot.Weapon1:
                        slotNumber = 1;
                        break;
                    case FilterInventorySlot.Weapon2:
                        slotNumber = 2;
                        break;
                    case FilterInventorySlot.Weapon3:
                        slotNumber = 3;
                        break;
                    case FilterInventorySlot.Weapon4:
                        slotNumber = 4;
                        break;
                    default:
                        slotNumber = 0;
                        break;
                }
                this.Title = "Weapon " + slotNumber + " filter";
            }
                
            if (!IsArmorSlotHidden)
                switch (_currentInventorySlot)
                {
                    case FilterInventorySlot.Helm:
                        this.Title = "Helm filter";
                        break;
                    case FilterInventorySlot.Cloak:
                        this.Title = "Cloak filter";
                        break;
                    case FilterInventorySlot.Body:
                        this.Title = "Armor filter";
                        break;
                    case FilterInventorySlot.Gloves:
                        this.Title = "Glove filter";
                        break;
                    case FilterInventorySlot.Boot:
                        this.Title = "Boot filter";
                        break;
                    case FilterInventorySlot.HorseHarness:
                        this.Title = "Harness filter";
                        break;
                    default:
                        this.Title = "Default";
                        break;
                }
            if (!IsMountSlotHidden)
                this.Title = "Mount filter";

            if (!IsWeaponSlotHidden)
            {
                FilterWeaponSettings filterWeapon = (FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot);

                Accuracy = filterWeapon.Accuracy.ToString();
                WeaponBodyArmor = filterWeapon.WeaponBodyArmor.ToString();
                Handling = filterWeapon.Handling.ToString();
                MaxDataValue = filterWeapon.MaxDataValue.ToString();
                MissileSpeed = filterWeapon.MissileSpeed.ToString();
                SwingDamage = filterWeapon.SwingDamage.ToString();
                SwingSpeed = filterWeapon.SwingSpeed.ToString();
                ThrustDamage = filterWeapon.ThrustDamage.ToString();
                ThrustSpeed = filterWeapon.ThrustSpeed.ToString();
                WeaponLength = filterWeapon.WeaponLength.ToString();
                WeaponWeight = filterWeapon.WeaponWeight.ToString();
            }

            if (!IsArmorSlotHidden)
            {
                FilterArmorSettings filterArmor = (FilterArmorSettings) _characterSettings.GetFilter(_currentInventorySlot);

                _armorHelmAdjuster.Text = filterArmor.HeadArmor.ToString();
                _armorBodyAdjuster.Text = filterArmor.ArmorBodyArmor.ToString();
                _armorBootAdjuster.Text = filterArmor.LegArmor.ToString();
                _armorGlovesAdjuster.Text = filterArmor.ArmArmor.ToString();
                _armorManeuverAdjuster.Text = filterArmor.ManeuverBonus.ToString();
                _armorSpeedAdjuster.Text = filterArmor.SpeedBonus.ToString();
                _armorChargeAdjuster.Text = filterArmor.ChargeBonus.ToString();
                _armorWeightAdjuster.Text = filterArmor.ArmorWeight.ToString();
            }

            if (!IsMountSlotHidden)
            {
                var settings = (FilterMountSettings) _characterSettings.GetFilter(FilterInventorySlot.Horse);
                _mountChargeDamageAdjuster.Text = settings.ChargeDamage.ToString();
                _mountHitPointsAdjuster.Text = settings.HitPoints.ToString();
                _mountManeuverAdjuster.Text = settings.Maneuver.ToString();
                _mountSpeedAdjuster.Text = settings.Speed.ToString();
            }
        }

        private void UpdateIcons()
        {
            //Helmet icon state
            this.IsHelmFilterSelected = this._characterSettings.FilterArmor[0].IsNotDefault();
            this.IsHelmFilterLocked = this._characterSettings.FilterArmor[0].IsZero();

            //Cloak icon state
            this.IsCloakFilterSelected = this._characterSettings.FilterArmor[1].IsNotDefault();
            this.IsCloakFilterLocked = this._characterSettings.FilterArmor[1].IsZero();

            //Armor icon state
            this.IsArmorFilterSelected = this._characterSettings.FilterArmor[2].IsNotDefault();
            this.IsArmorFilterLocked = this._characterSettings.FilterArmor[2].IsZero();

            //Gloves icon state
            this.IsGloveFilterSelected = this._characterSettings.FilterArmor[3].IsNotDefault();
            this.IsGloveFilterLocked = this._characterSettings.FilterArmor[3].IsZero();

            //Boots icon state
            this.IsBootFilterSelected = this._characterSettings.FilterArmor[4].IsNotDefault();
            this.IsBootFilterLocked = this._characterSettings.FilterArmor[4].IsZero();

            //Mount icon state
            this.IsMountFilterSelected = this._characterSettings.FilterMount.IsNotDefault();
            this.IsMountFilterLocked = this._characterSettings.FilterMount.IsZero();

            //Harness icon state
            this.IsHarnessFilterSelected = this._characterSettings.FilterArmor[5].IsNotDefault();
            this.IsHarnessFilterLocked = this._characterSettings.FilterArmor[5].IsZero();

            //Weapon1 icon state
            this.IsWeapon1FilterSelected = this._characterSettings.FilterWeapon[0].IsNotDefault();
            this.IsWeapon1FilterLocked = this._characterSettings.FilterWeapon[0].IsZero();

            //Weapon2 icon state
            this.IsWeapon2FilterSelected = this._characterSettings.FilterWeapon[1].IsNotDefault();
            this.IsWeapon2FilterLocked = this._characterSettings.FilterWeapon[1].IsZero();

            //Weapon3 icon state
            this.IsWeapon3FilterSelected = this._characterSettings.FilterWeapon[2].IsNotDefault();
            this.IsWeapon3FilterLocked = this._characterSettings.FilterWeapon[2].IsZero();

            //Weapon4 icon state
            this.IsWeapon4FilterSelected = this._characterSettings.FilterWeapon[3].IsNotDefault();
            this.IsWeapon4FilterLocked = this._characterSettings.FilterWeapon[3].IsZero();
        }

        #region ExecuteMethods
        public void ExecuteSwingDamagePrev()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).SwingDamage -= _incrementValue;
            SwingDamage = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).SwingDamage.ToString();
            this.RefreshValues();
        }
        public void ExecuteSwingDamageNext()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).SwingDamage += _incrementValue;
            SwingDamage = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).SwingDamage.ToString();
            this.RefreshValues();
        }

        public void ExecuteSwingSpeedPrev()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).SwingSpeed -= _incrementValue;
            SwingSpeed = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).SwingSpeed.ToString();
            this.RefreshValues();
        }
        public void ExecuteSwingSpeedNext()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).SwingSpeed += _incrementValue;
            SwingSpeed = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).SwingSpeed.ToString();
            this.RefreshValues();
        }

        public void ExecuteThrustDamagePrev()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).ThrustDamage -= _incrementValue;
            ThrustDamage = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).ThrustDamage.ToString();
            this.RefreshValues();
        }
        public void ExecuteThrustDamageNext()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).ThrustDamage += _incrementValue;
            ThrustDamage = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).ThrustDamage.ToString();
            this.RefreshValues();
        }

        public void ExecuteThrustSpeedPrev()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).ThrustSpeed -= _incrementValue;
            ThrustSpeed = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).ThrustSpeed.ToString();
            this.RefreshValues();
        }
        public void ExecuteThrustSpeedNext()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).ThrustSpeed += _incrementValue;
            ThrustSpeed = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).ThrustSpeed.ToString();
            this.RefreshValues();
        }

        public void ExecuteWeaponLengthPrev()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponLength -= _incrementValue;
            WeaponLength = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponLength.ToString();
            this.RefreshValues();
        }
        public void ExecuteWeaponLengthNext()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponLength += _incrementValue;
            WeaponLength = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponLength.ToString();
            this.RefreshValues();
        }

        public void ExecuteHandlingPrev()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).Handling -= _incrementValue;
            Handling = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).Handling.ToString();
            this.RefreshValues();
        }
        public void ExecuteHandlingNext()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).Handling += _incrementValue;
            Handling = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).Handling.ToString();
            this.RefreshValues();
        }

        public void ExecuteWeaponWeightPrev()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponWeight -= _incrementValue;
            WeaponWeight = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponWeight.ToString();
            this.RefreshValues();
        }
        public void ExecuteWeaponWeightNext()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponWeight += _incrementValue;
            WeaponWeight = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponWeight.ToString();
            this.RefreshValues();
        }

        public void ExecuteMissileSpeedPrev()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).MissileSpeed -= _incrementValue;
            MissileSpeed = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).MissileSpeed.ToString();
            this.RefreshValues();
        }
        public void ExecuteMissileSpeedNext()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).MissileSpeed += _incrementValue;
            MissileSpeed = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).MissileSpeed.ToString();
            this.RefreshValues();
        }

        public void ExecuteAccuracyPrev()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).Accuracy -= _incrementValue;
            Accuracy = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).Accuracy.ToString();
            this.RefreshValues();
        }
        public void ExecuteAccuracyNext()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).Accuracy += _incrementValue;
            Accuracy = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).Accuracy.ToString();
            this.RefreshValues();
        }

        public void ExecuteWeaponBodyArmorPrev()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponBodyArmor -= _incrementValue;
            WeaponBodyArmor = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponBodyArmor.ToString();
            this.RefreshValues();
        }
        public void ExecuteWeaponBodyArmorNext()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponBodyArmor += _incrementValue;
            WeaponBodyArmor = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).WeaponBodyArmor.ToString();
            this.RefreshValues();
        }

        public void ExecuteMaxDataValuePrev()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).MaxDataValue -= _incrementValue;
            MaxDataValue = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).MaxDataValue.ToString();
            this.RefreshValues();
        }
        public void ExecuteMaxDataValueNext()
        {
            ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).MaxDataValue += _incrementValue;
            MaxDataValue = ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).MaxDataValue.ToString();
            this.RefreshValues();
        }

        public void ShowHideFilter(FilterInventorySlot inventorySlot)
        {
            if (inventorySlot >= FilterInventorySlot.Weapon1 && inventorySlot <= FilterInventorySlot.Weapon4)
            {
                if (_currentInventorySlot != inventorySlot || this.IsWeaponSlotHidden)
                    this.IsHiddenFilterLayer = false;
                else
                    this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

                this.IsWeaponSlotHidden = false;
                this.IsArmorSlotHidden = true;
                this.IsMountSlotHidden = true;

                _filterItemState = FilterItemState.Weapon;
            }
            else if (inventorySlot >= FilterInventorySlot.Helm && inventorySlot <= FilterInventorySlot.HorseHarness)
            {
                if (_currentInventorySlot != inventorySlot || this.IsArmorSlotHidden)
                    this.IsHiddenFilterLayer = false;
                else
                    this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

                this.IsArmorSlotHidden = false;
                this.IsWeaponSlotHidden = true;
                this.IsMountSlotHidden = true;
                _filterItemState = FilterItemState.Armor;
            }
            else if (inventorySlot == FilterInventorySlot.Horse)
            {
                if (this.IsMountSlotHidden)
                    this.IsHiddenFilterLayer = false;
                else
                    this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

                this.IsArmorSlotHidden = true;
                this.IsWeaponSlotHidden = true;
                this.IsMountSlotHidden = false;
                _filterItemState = FilterItemState.Mount;
            }

            foreach (var adjuster in _armorFilterAdjusterList)
            {
                adjuster.Hidden = IsArmorSlotHidden;
            }
            foreach (var adjuster in _mountFilterAdjusterList)
            {
                adjuster.Hidden = IsMountSlotHidden;
            }

            _currentInventorySlot = inventorySlot;
            SetupArmorAdjusters();
        }

        public void ExecuteShowHideWeapon1Filter()
        {
            ShowHideFilter(FilterInventorySlot.Weapon1);
            this.RefreshValues();
        }

        public void ExecuteShowHideWeapon2Filter()
        {
            ShowHideFilter(FilterInventorySlot.Weapon2);
            this.RefreshValues();
        }

        public void ExecuteShowHideWeapon3Filter()
        {
            ShowHideFilter(FilterInventorySlot.Weapon3);
            this.RefreshValues();
        }

        public void ExecuteShowHideWeapon4Filter()
        {
            ShowHideFilter(FilterInventorySlot.Weapon4);
            this.RefreshValues();
        }

        public void ExecuteShowHideHelmFilter()
        {
            ShowHideFilter(FilterInventorySlot.Helm);
            this.RefreshValues();
        }

        public void ExecuteShowHideCloakFilter()
        {
            ShowHideFilter(FilterInventorySlot.Cloak);
            this.RefreshValues();
        }

        public void ExecuteShowHideArmorFilter()
        {
            ShowHideFilter(FilterInventorySlot.Body);
            this.RefreshValues();
        }

        public void ExecuteShowHideGloveFilter()
        {
            ShowHideFilter(FilterInventorySlot.Gloves);
            this.RefreshValues();
        }

        public void ExecuteShowHideBootFilter()
        {
            ShowHideFilter(FilterInventorySlot.Boot);
            this.RefreshValues();
        }

        public void ExecuteShowHideMountFilter()
        {
            ShowHideFilter(FilterInventorySlot.Horse);
            this.RefreshValues();
        }

        public void ExecuteShowHideHarnessFilter()
        {
            ShowHideFilter(FilterInventorySlot.HorseHarness);
            this.RefreshValues();
        }

        public void ExecuteWeaponClose()
        {
            IsHiddenFilterLayer = true;
            this.RefreshValues();
        }

        public void ExecuteItemClear()
        {
            switch (_filterItemState)
            {
                case FilterItemState.Armor:
                    ((FilterArmorSettings) _characterSettings.GetFilter(_currentInventorySlot)).Clear();
                    break;
                case FilterItemState.Weapon:
                    ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).Clear(); ;
                    break;
                case FilterItemState.Mount:
                    ((FilterMountSettings) _characterSettings.GetFilter(_currentInventorySlot)).Clear();
                    break;
            }
            RefreshValues();
        }

        public void ExecuteItemLock()
        {
            switch (_filterItemState)
            {
                case FilterItemState.Armor:
                    ((FilterArmorSettings) _characterSettings.GetFilter(_currentInventorySlot)).ClearZero();
                    break;
                case FilterItemState.Weapon:
                    ((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot)).ClearZero(); ;
                    break;
                case FilterItemState.Mount:
                    ((FilterMountSettings) _characterSettings.GetFilter(_currentInventorySlot)).ClearZero();
                    break;
            }
            RefreshValues();
        }

        public void ExecuteCopyFilterSettings()
        {
            switch (_filterItemState)
            {
                case FilterItemState.Armor:
                    _clipboardFilterArmorSettings = new FilterArmorSettings((FilterArmorSettings) _characterSettings.GetFilter(_currentInventorySlot));
                    IsFilterArmorSettingsCopied = true;
                    InformationManager.DisplayMessage(new InformationMessage("Armor settings copied"));
                    break;
                case FilterItemState.Weapon:
                    _clipboardFilterWeaponSettings = new FilterWeaponSettings((FilterWeaponSettings) _characterSettings.GetFilter(_currentInventorySlot));
                    IsFilterWeaponSettingsCopied = true;
                    InformationManager.DisplayMessage(new InformationMessage("Weapon settings copied"));
                    break;
                case FilterItemState.Mount:
                    _clipboardFilterMountSettings = new FilterMountSettings(_characterSettings.FilterMount);
                    IsFilterMountSettingsCopied = true;
                    InformationManager.DisplayMessage(new InformationMessage("Mount settings copied"));
                    break;
            }
        }

        public void ExecutePasteFilterSettings()
        {
            switch (_filterItemState)
            {
                case FilterItemState.Armor:
                    _characterSettings.SetFilter(_currentInventorySlot, new FilterArmorSettings(_clipboardFilterArmorSettings));
                    InformationManager.DisplayMessage(new InformationMessage("Armor settings pasted"));
                    break;
                case FilterItemState.Weapon:
                    _characterSettings.SetFilter(_currentInventorySlot, new FilterWeaponSettings(_clipboardFilterWeaponSettings));
                    InformationManager.DisplayMessage(new InformationMessage("Weapon settings pasted"));
                    break;
                case FilterItemState.Mount:
                    _characterSettings.FilterMount = new FilterMountSettings(_clipboardFilterMountSettings);
                    InformationManager.DisplayMessage(new InformationMessage("Mount settings pasted"));
                    break;
            }
            RefreshValues();
        }

        public void ExecuteCopyCharacterSettings()
        {
            _clipboardCharacterSettings = new CharacterSettings(_characterSettings);
            IsCharacterSettingsCopied = true;
            InformationManager.DisplayMessage(new InformationMessage("Character settings copied"));
        }

        public void ExecutePasteCharacterSettings()
        {
            var tempName = _characterSettings.Name;
            _characterSettings = new CharacterSettings(_clipboardCharacterSettings) {Name = tempName};
            _pastedCharacterSettings = true;
            InformationManager.DisplayMessage(new InformationMessage("Character settings pasted"));
            RefreshValues();
        }

        #endregion ExecuteMethods

        //private string _weaponClass = "Choose weapon class";


        //[DataSourceProperty]
        //public string WeaponClass
        //{
        //    get => _weaponClass;
        //    set
        //    {
        //        if (_weaponClass != value)
        //        {
        //            _weaponClass = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}


        //private string _weaponUsage = "Choose weapon type item usage";
        //[DataSourceProperty]
        //public string WeaponUsage
        //{
        //    get => _weaponUsage;
        //    set
        //    {
        //        if (_weaponUsage != value)
        //        {
        //            _weaponUsage = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //private List<string> _ItemUsageList = new List<string>()
        //{
        //    "arrow_right",
        //    "arrow_top",
        //    "bow",
        //    "crossbow",
        //    "hand_shield",
        //    "long_bow",
        //    "shield"
        //};


        //private List<string> _weaponFlagsList;
        //private int _weaponFlagsCurrentIndex = 0;

        //public void ExecuteWeaponTypeSelectNextItem()
        //{
        //    if (this._characterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass == (WeaponClass)28)
        //        this._characterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass = (WeaponClass)0;
        //    else
        //        this._characterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass += 1;

        //    this.WeaponClass = this._characterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass.ToString();
        //}

        //public void ExecuteWeaponUsageSelectPreviousItem()
        //{




        //    //////// НЕ УДАЛЯТЬ МОЖЕТ ПРИГОДИТЬСЯ В БУДУЩЕМ
        //    //_weaponFlagsCurrentIndex -= 1;
        //    //if (_weaponFlagsCurrentIndex < 0)
        //    //    _weaponFlagsCurrentIndex = _weaponFlagsList.Count - 1;


        //    //if (_weaponFlagsList[_weaponFlagsCurrentIndex] == "None")
        //    //    this._characterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)0;
        //    //else
        //    //    this._characterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)Enum.Parse(typeof(WeaponFlags), _weaponFlagsList[_weaponFlagsCurrentIndex]);

        //    //this.WeaponUsage = _weaponFlagsList[_weaponFlagsCurrentIndex];
        //}

        //public void ExecuteWeaponUsageSelectNextItem()
        //{




        //    //////// НЕ УДАЛЯТЬ МОЖЕТ ПРИГОДИТЬСЯ В БУДУЩЕМ
        //    ///
        //    //_weaponFlagsCurrentIndex += 1;
        //    //if (_weaponFlagsCurrentIndex > _weaponFlagsList.Count - 1)
        //    //    _weaponFlagsCurrentIndex = 0;

        //    //if (_weaponFlagsList[_weaponFlagsCurrentIndex] == "None")
        //    //    this._characterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)0;
        //    //else
        //    //    this._characterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)Enum.Parse(typeof(WeaponFlags), _weaponFlagsList[_weaponFlagsCurrentIndex]);

        //    //this.WeaponUsage = _weaponFlagsList[_weaponFlagsCurrentIndex];
        //}
    }
}
