using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem
{
    class MainViewModel : ViewModel
    {
        private BestEquipmentUpgrader bestEquipmentUpgrader;

        #region DataSourcePropertys

        private bool _isHelmButtonEnabled;

        [DataSourceProperty]
        public bool IsHelmButtonEnabled
        {
            get => _isHelmButtonEnabled;
            set
            {
                if (_isHelmButtonEnabled != value)
                {
                    _isHelmButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isCloakButtonEnabled;

        [DataSourceProperty]
        public bool IsCloakButtonEnabled
        {
            get => _isCloakButtonEnabled;
            set
            {
                if (_isCloakButtonEnabled != value)
                {
                    _isCloakButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isArmorButtonEnabled;

        [DataSourceProperty]
        public bool IsArmorButtonEnabled
        {
            get => _isArmorButtonEnabled;
            set
            {
                if (_isArmorButtonEnabled != value)
                {
                    _isArmorButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isGloveButtonEnabled;

        [DataSourceProperty]
        public bool IsGloveButtonEnabled
        {
            get => _isGloveButtonEnabled;
            set
            {
                if (_isGloveButtonEnabled != value)
                {
                    _isGloveButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isBootButtonEnabled;

        [DataSourceProperty]
        public bool IsBootButtonEnabled
        {
            get => _isBootButtonEnabled;
            set
            {
                if (_isBootButtonEnabled != value)
                {
                    _isBootButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isMountButtonEnabled;

        [DataSourceProperty]
        public bool IsMountButtonEnabled
        {
            get => _isMountButtonEnabled;
            set
            {
                if (_isMountButtonEnabled != value)
                {
                    _isMountButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isHarnessButtonEnabled;

        [DataSourceProperty]
        public bool IsHarnessButtonEnabled
        {
            get => _isHarnessButtonEnabled;
            set
            {
                if (_isHarnessButtonEnabled != value)
                {
                    _isHarnessButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon1ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon1ButtonEnabled
        {
            get => _isWeapon1ButtonEnabled;
            set
            {
                if (_isWeapon1ButtonEnabled != value)
                {
                    _isWeapon1ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon2ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon2ButtonEnabled
        {
            get => _isWeapon2ButtonEnabled;
            set
            {
                if (_isWeapon2ButtonEnabled != value)
                {
                    _isWeapon2ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon3ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon3ButtonEnabled
        {
            get => _isWeapon3ButtonEnabled;
            set
            {
                if (_isWeapon3ButtonEnabled != value)
                {
                    _isWeapon3ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon4ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon4ButtonEnabled
        {
            get => _isWeapon4ButtonEnabled;
            set
            {
                if (_isWeapon4ButtonEnabled != value)
                {
                    _isWeapon4ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isEquipCurrentCharacterButtonEnabled;
        [DataSourceProperty]
        public bool IsEquipCurrentCharacterButtonEnabled
        {
            get => _isEquipCurrentCharacterButtonEnabled;
            set
            {
                if (_isEquipCurrentCharacterButtonEnabled != value)
                {
                    _isEquipCurrentCharacterButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isEnabledEquipAllButton;
        [DataSourceProperty]
        public bool IsEnabledEquipAllButton
        {
            get => _isEnabledEquipAllButton;
            set
            {
                if (_isEnabledEquipAllButton != value)
                {
                    _isEnabledEquipAllButton = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion DataSourcePropertys

        public MainViewModel()
        {
            bestEquipmentUpgrader = new BestEquipmentUpgrader();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            bestEquipmentUpgrader.RefreshValues();

            // Updates whether the character should be able to upgrade the item
            IsHelmButtonEnabled = bestEquipmentUpgrader.IsItemUpgradable(EquipmentIndex.Head);
            IsCloakButtonEnabled = bestEquipmentUpgrader.IsItemUpgradable(EquipmentIndex.Cape);
            IsArmorButtonEnabled = bestEquipmentUpgrader.IsItemUpgradable(EquipmentIndex.Body);
            IsGloveButtonEnabled = bestEquipmentUpgrader.IsItemUpgradable(EquipmentIndex.Gloves);
            IsBootButtonEnabled = bestEquipmentUpgrader.IsItemUpgradable(EquipmentIndex.Leg);
            IsMountButtonEnabled = bestEquipmentUpgrader.IsItemUpgradable(EquipmentIndex.Horse);
            IsHarnessButtonEnabled = bestEquipmentUpgrader.IsItemUpgradable(EquipmentIndex.HorseHarness);
            IsWeapon1ButtonEnabled = bestEquipmentUpgrader.IsItemUpgradable(EquipmentIndex.Weapon0);
            IsWeapon2ButtonEnabled = bestEquipmentUpgrader.IsItemUpgradable(EquipmentIndex.Weapon1);
            IsWeapon3ButtonEnabled = bestEquipmentUpgrader.IsItemUpgradable(EquipmentIndex.Weapon2);
            IsWeapon4ButtonEnabled = bestEquipmentUpgrader.IsItemUpgradable(EquipmentIndex.Weapon3);

            IsEquipCurrentCharacterButtonEnabled = bestEquipmentUpgrader.IsUpgradeAvailable();

#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage("MainViewModel RefreshValues()"));
#endif
        }

        public void ExecuteEquipEveryCharacter()
        {
            bestEquipmentUpgrader.EquipEveryCharacter();
        }

        public void ExecuteEquipCurrentCharacter()
        {
            bestEquipmentUpgrader.EquipCurrentCharacter();
            this.RefreshValues();

#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage("ExecuteEquipCurrentCharacter"));
#endif
        }

        public void ExecuteEquipBestHelm()
        {
            bestEquipmentUpgrader.EquipBestItem(EquipmentIndex.Head);
        }

        public void ExecuteEquipBestCloak()
        {
            bestEquipmentUpgrader.EquipBestItem(EquipmentIndex.Cape);
        }

        public void ExecuteEquipBestArmor()
        {
            bestEquipmentUpgrader.EquipBestItem(EquipmentIndex.Body);
        }

        public void ExecuteEquipBestGlove()
        {
            bestEquipmentUpgrader.EquipBestItem(EquipmentIndex.Gloves);
        }

        public void ExecuteEquipBestBoot()
        {
            bestEquipmentUpgrader.EquipBestItem(EquipmentIndex.Leg);
        }

        public void ExecuteEquipBestMount()
        {
            bestEquipmentUpgrader.EquipBestItem(EquipmentIndex.Horse);
        }

        public void ExecuteEquipBestHarness()
        {
            bestEquipmentUpgrader.EquipBestItem(EquipmentIndex.HorseHarness);
        }

        public void ExecuteEquipBestWeapon1()
        {
            bestEquipmentUpgrader.EquipBestItem(EquipmentIndex.Weapon0);
        }

        public void ExecuteEquipBestWeapon2()
        {
            bestEquipmentUpgrader.EquipBestItem(EquipmentIndex.Weapon1);
        }

        public void ExecuteEquipBestWeapon3()
        {
            bestEquipmentUpgrader.EquipBestItem(EquipmentIndex.Weapon2);
        }

        public void ExecuteEquipBestWeapon4()
        {
            bestEquipmentUpgrader.EquipBestItem(EquipmentIndex.Weapon3);
        }
    }
}
