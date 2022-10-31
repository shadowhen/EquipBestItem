using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem
{
    class FilterViewModel : ViewModel
    {
        public CharacterSettings CharacterSettings;

        public static int CurrentSlot = 0;

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

        // Holds filter settings for clipboard usage
        private FilterArmorSettings _clipboardFilterArmorSettings;
        private FilterWeaponSettings _clipboardFilterWeaponSettings;
        private FilterMountSettings _clipboardFilterMountSettings;
        private CharacterSettings _clipboardCharacterSettings;

        private bool _pastedCharacterSettings = false;

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

        private string _headArmor;
        [DataSourceProperty]
        public string HeadArmor
        {
            get => _headArmor;
            set
            {
                if (_headArmor != value)
                {
                    _headArmor = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _armorBodyArmor;
        [DataSourceProperty]
        public string ArmorBodyArmor
        {
            get => _armorBodyArmor;
            set
            {
                if (_armorBodyArmor != value)
                {
                    _armorBodyArmor = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _legArmor;
        [DataSourceProperty]
        public string LegArmor
        {
            get => _legArmor;
            set
            {
                if (_legArmor != value)
                {
                    _legArmor = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _armArmor;
        [DataSourceProperty]
        public string ArmArmor
        {
            get => _armArmor;
            set
            {
                if (_armArmor != value)
                {
                    _armArmor = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maneuverBonus;
        [DataSourceProperty]
        public string ManeuverBonus
        {
            get => _maneuverBonus;
            set
            {
                if (_maneuverBonus != value)
                {
                    _maneuverBonus = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _speedBonus;
        [DataSourceProperty]
        public string SpeedBonus
        {
            get => _speedBonus;
            set
            {
                if (_speedBonus != value)
                {
                    _speedBonus = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _chargeBonus;
        [DataSourceProperty]
        public string ChargeBonus
        {
            get => _chargeBonus;
            set
            {
                if (_chargeBonus != value)
                {
                    _chargeBonus = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _armorWeight;
        [DataSourceProperty]
        public string ArmorWeight
        {
            get => _armorWeight;
            set
            {
                if (_armorWeight != value)
                {
                    _armorWeight = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _chargeDamage;
        [DataSourceProperty]
        public string ChargeDamage
        {
            get => _chargeDamage;
            set
            {
                if (_chargeDamage != value)
                {
                    _chargeDamage = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _hitPoints;
        [DataSourceProperty]
        public string HitPoints
        {
            get => _hitPoints;
            set
            {
                if (_hitPoints != value)
                {
                    _hitPoints = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maneuver;
        [DataSourceProperty]
        public string Maneuver
        {
            get => _maneuver;
            set
            {
                if (_maneuver != value)
                {
                    _maneuver = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _speed;
        [DataSourceProperty]
        public string Speed
        {
            get => _speed;
            set
            {
                if (_speed != value)
                {
                    _speed = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion DataSourceProperties

        public FilterViewModel()
        {
            this.RefreshValues();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            try
            {
                // Should character settings be pasted from clipboard, set the new characters settings
                // Note: This process should not be executed here since it only occurs when the values refresh.
                // It would be wise to move this portion of code to the part where the settings are pasted.
                if (!_pastedCharacterSettings)
                {
                    this.CharacterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(InventoryBehavior.Inventory.CurrentCharacterName);
                }
                else
                {
                    SettingsLoader.Instance.SetCharacterSettingsByName(CharacterSettings, InventoryBehavior.Inventory.CurrentCharacterName);
                    _pastedCharacterSettings = false;
                }
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
                throw;
            }

            // Updates the title whether we are looking at weapon, armor, or mount filter settings window
            if (!IsWeaponSlotHidden)
                this.Title = "Weapon " + (CurrentSlot + 1) + " filter";
            if (!IsArmorSlotHidden)
                switch (CurrentSlot)
                {
                    case 0:
                        this.Title = "Helm filter";
                        break;
                    case 1:
                        this.Title = "Cloak filter";
                        break;
                    case 2:
                        this.Title = "Armor filter";
                        break;
                    case 3:
                        this.Title = "Glove filter";
                        break;
                    case 4:
                        this.Title = "Boot filter";
                        break;
                    case 5:
                        this.Title = "Harness filter";
                        break;
                    default:
                        this.Title = "Default";
                        break;
                }
            if (!IsMountSlotHidden)
                this.Title = "Mount " + "filter";

            if (!IsWeaponSlotHidden)
            {
                this.Accuracy = this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy.ToString();
                this.WeaponBodyArmor = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor.ToString();
                this.Handling = this.CharacterSettings.FilterWeapon[CurrentSlot].Handling.ToString();
                this.MaxDataValue = this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue.ToString();
                this.MissileSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed.ToString();
                this.SwingDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage.ToString();
                this.SwingSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed.ToString();
                this.ThrustDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage.ToString();
                this.ThrustSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed.ToString();
                this.WeaponLength = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength.ToString();
                this.WeaponWeight = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight.ToString();
            }

            if (!IsArmorSlotHidden)
            {
                this.HeadArmor = this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor.ToString();
                this.ArmorBodyArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor.ToString();
                this.LegArmor = this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor.ToString();
                this.ArmArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor.ToString();
                this.ManeuverBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus.ToString();
                this.SpeedBonus = this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus.ToString();
                this.ChargeBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus.ToString();
                this.ArmorWeight = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight.ToString();
            }

            if (!IsMountSlotHidden)
            {
                this.ChargeDamage = this.CharacterSettings.FilterMount.ChargeDamage.ToString();
                this.HitPoints = this.CharacterSettings.FilterMount.HitPoints.ToString();
                this.Maneuver = this.CharacterSettings.FilterMount.Maneuver.ToString();
                this.Speed = this.CharacterSettings.FilterMount.Speed.ToString();
            }

            //Helmet icon state
            if (this.CharacterSettings.FilterArmor[0].ThisFilterNotDefault())
                this.IsHelmFilterSelected = true;
            else
                this.IsHelmFilterSelected = false;

            if (this.CharacterSettings.FilterArmor[0].ThisFilterLocked())
                this.IsHelmFilterLocked = true;
            else
                this.IsHelmFilterLocked = false;

            //Cloak icon state
            if (this.CharacterSettings.FilterArmor[1].ThisFilterNotDefault())
                this.IsCloakFilterSelected = true;
            else
                this.IsCloakFilterSelected = false;

            if (this.CharacterSettings.FilterArmor[1].ThisFilterLocked())
                this.IsCloakFilterLocked = true;
            else
                this.IsCloakFilterLocked = false;

            //Armor icon state
            if (this.CharacterSettings.FilterArmor[2].ThisFilterNotDefault())
                this.IsArmorFilterSelected = true;
            else
                this.IsArmorFilterSelected = false;

            if (this.CharacterSettings.FilterArmor[2].ThisFilterLocked())
                this.IsArmorFilterLocked = true;
            else
                this.IsArmorFilterLocked = false;

            //Gloves icon state
            if (this.CharacterSettings.FilterArmor[3].ThisFilterNotDefault())
                this.IsGloveFilterSelected = true;
            else
                this.IsGloveFilterSelected = false;

            if (this.CharacterSettings.FilterArmor[3].ThisFilterLocked())
                this.IsGloveFilterLocked = true;
            else
                this.IsGloveFilterLocked = false;

            //Boots icon state
            if (this.CharacterSettings.FilterArmor[4].ThisFilterNotDefault())
                this.IsBootFilterSelected = true;
            else
                this.IsBootFilterSelected = false;

            if (this.CharacterSettings.FilterArmor[4].ThisFilterLocked())
                this.IsBootFilterLocked = true;
            else
                this.IsBootFilterLocked = false;

            //Mount icon state
            if (this.CharacterSettings.FilterMount.ThisFilterNotDefault())
                this.IsMountFilterSelected = true;
            else
                this.IsMountFilterSelected = false;

            if (this.CharacterSettings.FilterMount.ThisFilterLocked())
                this.IsMountFilterLocked = true;
            else
                this.IsMountFilterLocked = false;

            //Harness icon state
            if (this.CharacterSettings.FilterArmor[5].ThisFilterNotDefault())
                this.IsHarnessFilterSelected = true;
            else
                this.IsHarnessFilterSelected = false;

            if (this.CharacterSettings.FilterArmor[5].ThisFilterLocked())
                this.IsHarnessFilterLocked = true;
            else
                this.IsHarnessFilterLocked = false;

            //Weapon1 icon state
            if (this.CharacterSettings.FilterWeapon[0].ThisFilterNotDefault())
                this.IsWeapon1FilterSelected = true;
            else
                this.IsWeapon1FilterSelected = false;

            if (this.CharacterSettings.FilterWeapon[0].ThisFilterLocked())
                this.IsWeapon1FilterLocked = true;
            else
                this.IsWeapon1FilterLocked = false;

            //Weapon2 icon state
            if (this.CharacterSettings.FilterWeapon[1].ThisFilterNotDefault())
                this.IsWeapon2FilterSelected = true;
            else
                this.IsWeapon2FilterSelected = false;

            if (this.CharacterSettings.FilterWeapon[1].ThisFilterLocked())
                this.IsWeapon2FilterLocked = true;
            else
                this.IsWeapon2FilterLocked = false;

            //Weapon3 icon state
            if (this.CharacterSettings.FilterWeapon[2].ThisFilterNotDefault())
                this.IsWeapon3FilterSelected = true;
            else
                this.IsWeapon3FilterSelected = false;

            if (this.CharacterSettings.FilterWeapon[2].ThisFilterLocked())
                this.IsWeapon3FilterLocked = true;
            else
                this.IsWeapon3FilterLocked = false;

            //Weapon4 icon state
            if (this.CharacterSettings.FilterWeapon[3].ThisFilterNotDefault())
                this.IsWeapon4FilterSelected = true;
            else
                this.IsWeapon4FilterSelected = false;

            if (this.CharacterSettings.FilterWeapon[3].ThisFilterLocked())
                this.IsWeapon4FilterLocked = true;
            else
                this.IsWeapon4FilterLocked = false;
#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage("FilterVMRefreshValue")); 
#endif
        }

        #region ExecuteMethods
        public void ExecuteSwingDamagePrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage -= _incrementValue;
            SwingDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage.ToString();
            this.RefreshValues();
        }
        public void ExecuteSwingDamageNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage += _incrementValue;
            SwingDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage.ToString();
            this.RefreshValues();
        }

        public void ExecuteSwingSpeedPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed -= _incrementValue;
            SwingSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed.ToString();
            this.RefreshValues();
        }
        public void ExecuteSwingSpeedNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed += _incrementValue;
            SwingSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed.ToString();
            this.RefreshValues();
        }

        public void ExecuteThrustDamagePrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage -= _incrementValue;
            ThrustDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage.ToString();
            this.RefreshValues();
        }
        public void ExecuteThrustDamageNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage += _incrementValue;
            ThrustDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage.ToString();
            this.RefreshValues();
        }

        public void ExecuteThrustSpeedPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed -= _incrementValue;
            ThrustSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed.ToString();
            this.RefreshValues();
        }
        public void ExecuteThrustSpeedNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed += _incrementValue;
            ThrustSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed.ToString();
            this.RefreshValues();
        }

        public void ExecuteWeaponLengthPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength -= _incrementValue;
            WeaponLength = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength.ToString();
            this.RefreshValues();
        }
        public void ExecuteWeaponLengthNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength += _incrementValue;
            WeaponLength = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength.ToString();
            this.RefreshValues();
        }

        public void ExecuteHandlingPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Handling -= _incrementValue;
            Handling = this.CharacterSettings.FilterWeapon[CurrentSlot].Handling.ToString();
            this.RefreshValues();
        }
        public void ExecuteHandlingNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Handling += _incrementValue;
            Handling = this.CharacterSettings.FilterWeapon[CurrentSlot].Handling.ToString();
            this.RefreshValues();
        }

        public void ExecuteWeaponWeightPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight -= _incrementValue;
            WeaponWeight = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight.ToString();
            this.RefreshValues();
        }
        public void ExecuteWeaponWeightNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight += _incrementValue;
            WeaponWeight = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight.ToString();
            this.RefreshValues();
        }

        public void ExecuteMissileSpeedPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed -= _incrementValue;
            MissileSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed.ToString();
            this.RefreshValues();
        }
        public void ExecuteMissileSpeedNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed += _incrementValue;
            MissileSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed.ToString();
            this.RefreshValues();
        }

        public void ExecuteAccuracyPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy -= _incrementValue;
            Accuracy = this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy.ToString();
            this.RefreshValues();
        }
        public void ExecuteAccuracyNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy += _incrementValue;
            Accuracy = this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy.ToString();
            this.RefreshValues();
        }

        public void ExecuteWeaponBodyArmorPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor -= _incrementValue;
            WeaponBodyArmor = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor.ToString();
            this.RefreshValues();
        }
        public void ExecuteWeaponBodyArmorNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor += _incrementValue;
            WeaponBodyArmor = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor.ToString();
            this.RefreshValues();
        }

        public void ExecuteMaxDataValuePrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue -= _incrementValue;
            MaxDataValue = this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue.ToString();
            this.RefreshValues();
        }
        public void ExecuteMaxDataValueNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue += _incrementValue;
            MaxDataValue = this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue.ToString();
            this.RefreshValues();
        }


        public void ExecuteHeadArmorPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor -= _incrementValue;
            HeadArmor = this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor.ToString();
            this.RefreshValues();
        }
        public void ExecuteHeadArmorNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor += _incrementValue;
            HeadArmor = this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor.ToString();
            this.RefreshValues();
        }

        public void ExecuteArmorBodyArmorPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor -= _incrementValue;
            ArmorBodyArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor.ToString();
            this.RefreshValues();
        }
        public void ExecuteArmorBodyArmorNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor += _incrementValue;
            ArmorBodyArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor.ToString();
            this.RefreshValues();
        }

        public void ExecuteLegArmorPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor -= _incrementValue;
            LegArmor = this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor.ToString();
            this.RefreshValues();
        }
        public void ExecuteLegArmorNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor += _incrementValue;
            LegArmor = this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor.ToString();
            this.RefreshValues();
        }

        public void ExecuteArmArmorPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor -= _incrementValue;
            ArmArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor.ToString();
            this.RefreshValues();
        }
        public void ExecuteArmArmorNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor += _incrementValue;
            ArmArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor.ToString();
            this.RefreshValues();
        }

        public void ExecuteManeuverBonusPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus -= _incrementValue;
            ManeuverBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus.ToString();
            this.RefreshValues();
        }
        public void ExecuteManeuverBonusNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus += _incrementValue;
            ManeuverBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus.ToString();
            this.RefreshValues();
        }

        public void ExecuteSpeedBonusPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus -= _incrementValue;
            SpeedBonus = this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus.ToString();
            this.RefreshValues();
        }
        public void ExecuteSpeedBonusNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus += _incrementValue;
            SpeedBonus = this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus.ToString();
            this.RefreshValues();
        }

        public void ExecuteChargeBonusPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus -= _incrementValue;
            ChargeBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus.ToString();
            this.RefreshValues();
        }
        public void ExecuteChargeBonusNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus += _incrementValue;
            ChargeBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus.ToString();
            this.RefreshValues();
        }

        public void ExecuteArmorWeightPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight -= _incrementValue;
            ArmorWeight = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight.ToString();
            this.RefreshValues();
        }
        public void ExecuteArmorWeightNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight += _incrementValue;
            ArmorWeight = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight.ToString();
            this.RefreshValues();
        }

        public void ExecuteChargeDamagePrev()
        {
            this.CharacterSettings.FilterMount.ChargeDamage -= _incrementValue;
            ChargeDamage = this.CharacterSettings.FilterMount.ChargeDamage.ToString();
            this.RefreshValues();
        }
        public void ExecuteChargeDamageNext()
        {
            this.CharacterSettings.FilterMount.ChargeDamage += _incrementValue;
            ChargeDamage = this.CharacterSettings.FilterMount.ChargeDamage.ToString();
            this.RefreshValues();
        }

        public void ExecuteHitPointsPrev()
        {
            this.CharacterSettings.FilterMount.HitPoints -= _incrementValue;
            HitPoints = this.CharacterSettings.FilterMount.HitPoints.ToString();
            this.RefreshValues();
        }
        public void ExecuteHitPointsNext()
        {
            this.CharacterSettings.FilterMount.HitPoints += _incrementValue;
            HitPoints = this.CharacterSettings.FilterMount.HitPoints.ToString();
            this.RefreshValues();
        }

        public void ExecuteManeuverPrev()
        {
            this.CharacterSettings.FilterMount.Maneuver -= _incrementValue;
            Maneuver = this.CharacterSettings.FilterMount.Maneuver.ToString();
            this.RefreshValues();
        }
        public void ExecuteManeuverNext()
        {
            this.CharacterSettings.FilterMount.Maneuver += _incrementValue;
            Maneuver = this.CharacterSettings.FilterMount.Maneuver.ToString();
            this.RefreshValues();
        }

        public void ExecuteSpeedPrev()
        {
            this.CharacterSettings.FilterMount.Speed -= _incrementValue;
            Speed = this.CharacterSettings.FilterMount.Speed.ToString();
            this.RefreshValues();
        }
        public void ExecuteSpeedNext()
        {
            this.CharacterSettings.FilterMount.Speed += _incrementValue;
            Speed = this.CharacterSettings.FilterMount.Speed.ToString();
            this.RefreshValues();
        }


        public void ExecuteShowHideWeapon1Filter()
        {
            if (CurrentSlot != 0 || this.IsWeaponSlotHidden)
            {
                CurrentSlot = 0;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;
            this.IsWeaponSlotHidden = false;
            this.IsArmorSlotHidden = true;
            this.IsMountSlotHidden = true;
            _filterItemState = FilterItemState.Weapon;
            this.RefreshValues();
        }

        public void ExecuteShowHideWeapon2Filter()
        {
            if (CurrentSlot != 1 || this.IsWeaponSlotHidden)
            {
                CurrentSlot = 1;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;
            this.IsWeaponSlotHidden = false;
            this.IsArmorSlotHidden = true;
            this.IsMountSlotHidden = true;
            _filterItemState = FilterItemState.Weapon;
            this.RefreshValues();
        }

        public void ExecuteShowHideWeapon3Filter()
        {
            if (CurrentSlot != 2 || this.IsWeaponSlotHidden)
            {
                CurrentSlot = 2;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;
            this.IsWeaponSlotHidden = false;
            this.IsArmorSlotHidden = true;
            this.IsMountSlotHidden = true;
            _filterItemState = FilterItemState.Weapon;
            this.RefreshValues();
        }

        public void ExecuteShowHideWeapon4Filter()
        {
            if (CurrentSlot != 3 || this.IsWeaponSlotHidden)
            {
                CurrentSlot = 3;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;
            this.IsWeaponSlotHidden = false;
            this.IsArmorSlotHidden = true;
            this.IsMountSlotHidden = true;
            _filterItemState = FilterItemState.Weapon;
            this.RefreshValues();
        }

        public void ExecuteShowHideHelmFilter()
        {
            if (CurrentSlot != 0 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 0;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            _filterItemState = FilterItemState.Armor;
            this.RefreshValues();
        }

        public void ExecuteShowHideCloakFilter()
        {
            if (CurrentSlot != 1 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 1;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            _filterItemState = FilterItemState.Armor;
            this.RefreshValues();
        }

        public void ExecuteShowHideArmorFilter()
        {
            if (CurrentSlot != 2 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 2;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            _filterItemState = FilterItemState.Armor;
            this.RefreshValues();
        }

        public void ExecuteShowHideGloveFilter()
        {
            if (CurrentSlot != 3 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 3;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            _filterItemState = FilterItemState.Armor;
            this.RefreshValues();
        }

        public void ExecuteShowHideBootFilter()
        {
            if (CurrentSlot != 4 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 4;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            
            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            _filterItemState = FilterItemState.Armor;
            this.RefreshValues();
        }

        public void ExecuteShowHideMountFilter()
        {
            if (this.IsMountSlotHidden)
                this.IsHiddenFilterLayer = false;
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = true;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = false;
            _filterItemState = FilterItemState.Mount;
            this.RefreshValues();
        }

        public void ExecuteShowHideHarnessFilter()
        {
            if (CurrentSlot != 5 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 5;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            _filterItemState = FilterItemState.Armor;
            this.RefreshValues();
        }

        public void ExecuteWeaponClose()
        {
            IsHiddenFilterLayer = true;
            this.RefreshValues();
        }

        public void ExecuteMountClear()
        {
            this.CharacterSettings.FilterMount.ChargeDamage = 1f;
            this.CharacterSettings.FilterMount.HitPoints = 1f;
            this.CharacterSettings.FilterMount.Maneuver = 1f;
            this.CharacterSettings.FilterMount.Speed = 1f;
            this.RefreshValues();
        }

        public void ExecuteArmorClear()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight = 0;
            this.RefreshValues();
        }

        public void ExecuteWeaponClear()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].Handling = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight = 0;
            this.RefreshValues();
        }

        public void ExecuteItemClear()
        {
            switch (_filterItemState)
            {
                case FilterItemState.Armor:
                    ExecuteArmorClear();
                    break;
                case FilterItemState.Weapon:
                    ExecuteWeaponClear();
                    break;
                case FilterItemState.Mount:
                    ExecuteMountClear();
                    break;
            }
        }

        public void ExecuteMountLock()
        {
            this.CharacterSettings.FilterMount.ChargeDamage = 0;
            this.CharacterSettings.FilterMount.HitPoints = 0;
            this.CharacterSettings.FilterMount.Maneuver = 0;
            this.CharacterSettings.FilterMount.Speed = 0;
            this.RefreshValues();
        }

        public void ExecuteArmorLock()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight = 0;
            this.RefreshValues();
        }

        public void ExecuteWeaponLock()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].Handling = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight = 0;
            this.RefreshValues();
        }

        public void ExecuteItemLock()
        {
            switch (_filterItemState)
            {
                case FilterItemState.Armor:
                    ExecuteArmorLock();
                    break;
                case FilterItemState.Weapon:
                    ExecuteWeaponLock();
                    break;
                case FilterItemState.Mount:
                    ExecuteMountLock();
                    break;
            }
        }

        public void ExecuteCopyFilterSettings()
        {
            switch (_filterItemState)
            {
                case FilterItemState.Armor:
                    _clipboardFilterArmorSettings = new FilterArmorSettings(CharacterSettings.FilterArmor[CurrentSlot]);
                    IsFilterArmorSettingsCopied = true;
                    InformationManager.DisplayMessage(new InformationMessage("Armor settings copied"));
                    break;
                case FilterItemState.Weapon:
                    _clipboardFilterWeaponSettings = new FilterWeaponSettings(CharacterSettings.FilterWeapon[CurrentSlot]);
                    IsFilterWeaponSettingsCopied = true;
                    InformationManager.DisplayMessage(new InformationMessage("Weapon settings copied"));
                    break;
                case FilterItemState.Mount:
                    _clipboardFilterMountSettings = new FilterMountSettings(CharacterSettings.FilterMount);
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
                    CharacterSettings.FilterArmor[CurrentSlot] = new FilterArmorSettings(_clipboardFilterArmorSettings);
                    InformationManager.DisplayMessage(new InformationMessage("Armor settings pasted"));
                    break;
                case FilterItemState.Weapon:
                    CharacterSettings.FilterWeapon[CurrentSlot] = new FilterWeaponSettings(_clipboardFilterWeaponSettings);
                    InformationManager.DisplayMessage(new InformationMessage("Weapon settings pasted"));
                    break;
                case FilterItemState.Mount:
                    CharacterSettings.FilterMount = new FilterMountSettings(_clipboardFilterMountSettings);
                    InformationManager.DisplayMessage(new InformationMessage("Mount settings pasted"));
                    break;
            }
            RefreshValues();
        }

        public void ExecuteCopyCharacterSettings()
        {
            _clipboardCharacterSettings = new CharacterSettings(CharacterSettings);
            IsCharacterSettingsCopied = true;
            InformationManager.DisplayMessage(new InformationMessage("Character settings copied"));
        }

        public void ExecutePasteCharacterSettings()
        {
            var tempName = CharacterSettings.Name;
            CharacterSettings = new CharacterSettings(_clipboardCharacterSettings);
            CharacterSettings.Name = tempName;
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
        //    if (this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass == (WeaponClass)28)
        //        this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass = (WeaponClass)0;
        //    else
        //        this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass += 1;

        //    this.WeaponClass = this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass.ToString();
        //}

        //public void ExecuteWeaponUsageSelectPreviousItem()
        //{




        //    //////// НЕ УДАЛЯТЬ МОЖЕТ ПРИГОДИТЬСЯ В БУДУЩЕМ
        //    //_weaponFlagsCurrentIndex -= 1;
        //    //if (_weaponFlagsCurrentIndex < 0)
        //    //    _weaponFlagsCurrentIndex = _weaponFlagsList.Count - 1;


        //    //if (_weaponFlagsList[_weaponFlagsCurrentIndex] == "None")
        //    //    this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)0;
        //    //else
        //    //    this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)Enum.Parse(typeof(WeaponFlags), _weaponFlagsList[_weaponFlagsCurrentIndex]);

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
        //    //    this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)0;
        //    //else
        //    //    this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)Enum.Parse(typeof(WeaponFlags), _weaponFlagsList[_weaponFlagsCurrentIndex]);

        //    //this.WeaponUsage = _weaponFlagsList[_weaponFlagsCurrentIndex];
        //}
    }
}
