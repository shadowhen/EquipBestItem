
using System;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    public class DummyIconVM : ViewModel
    {
        public Action<FilterInventorySlot> ExecuteAction { get; set; }

        public DummyIconVM()
        {
            ExecuteAction = null;
        }

        public DummyIconVM(Action<FilterInventorySlot> executeAction)
        {
            ExecuteAction = executeAction;
        }

        public void UpdateIcons(CharacterSettings characterSettings)
        {
            if (characterSettings == null)
                return;

            //Helmet icon state
            IsHelmFilterSelected = characterSettings.FilterArmor[0].IsNotDefault();
            IsHelmFilterLocked = characterSettings.FilterArmor[0].IsZero();

            //Cloak icon state
            IsCloakFilterSelected = characterSettings.FilterArmor[1].IsNotDefault();
            IsCloakFilterLocked = characterSettings.FilterArmor[1].IsZero();

            //Armor icon state
            IsArmorFilterSelected = characterSettings.FilterArmor[2].IsNotDefault();
            IsArmorFilterLocked = characterSettings.FilterArmor[2].IsZero();

            //Gloves icon state
            IsGloveFilterSelected = characterSettings.FilterArmor[3].IsNotDefault();
            IsGloveFilterLocked = characterSettings.FilterArmor[3].IsZero();

            //Boots icon state
            IsBootFilterSelected = characterSettings.FilterArmor[4].IsNotDefault();
            IsBootFilterLocked = characterSettings.FilterArmor[4].IsZero();

            //Mount icon state
            IsMountFilterSelected = characterSettings.FilterMount.IsNotDefault();
            IsMountFilterLocked = characterSettings.FilterMount.IsZero();

            //Harness icon state
            IsHarnessFilterSelected = characterSettings.FilterArmor[5].IsNotDefault();
            IsHarnessFilterLocked = characterSettings.FilterArmor[5].IsZero();

            //Weapon1 icon state
            IsWeapon1FilterSelected = characterSettings.FilterWeapon[0].IsNotDefault();
            IsWeapon1FilterLocked = characterSettings.FilterWeapon[0].IsZero();

            //Weapon2 icon state
            IsWeapon2FilterSelected = characterSettings.FilterWeapon[1].IsNotDefault();
            IsWeapon2FilterLocked = characterSettings.FilterWeapon[1].IsZero();

            //Weapon3 icon state
            IsWeapon3FilterSelected = characterSettings.FilterWeapon[2].IsNotDefault();
            IsWeapon3FilterLocked = characterSettings.FilterWeapon[2].IsZero();

            //Weapon4 icon state
            IsWeapon4FilterSelected = characterSettings.FilterWeapon[3].IsNotDefault();
            IsWeapon4FilterLocked = characterSettings.FilterWeapon[3].IsZero();
        }

        private void ExecuteShowHideWeapon1Filter()
        {
            ExecuteAction?.Invoke(FilterInventorySlot.Weapon1);
        }

        private void ExecuteShowHideWeapon2Filter()
        {
            ExecuteAction?.Invoke(FilterInventorySlot.Weapon2);
        }

        private void ExecuteShowHideWeapon3Filter()
        {
            ExecuteAction?.Invoke(FilterInventorySlot.Weapon3);
        }

        private void ExecuteShowHideWeapon4Filter()
        {
            ExecuteAction?.Invoke(FilterInventorySlot.Weapon4);
        }

        private void ExecuteShowHideHelmFilter()
        {
            ExecuteAction?.Invoke(FilterInventorySlot.Helm);
        }

        private void ExecuteShowHideCloakFilter()
        {
            ExecuteAction?.Invoke(FilterInventorySlot.Cloak);
        }

        private void ExecuteShowHideArmorFilter()
        {
            ExecuteAction?.Invoke(FilterInventorySlot.Body);
        }

        private void ExecuteShowHideGloveFilter()
        {
            ExecuteAction?.Invoke(FilterInventorySlot.Gloves);
        }

        private void ExecuteShowHideBootFilter()
        {
            ExecuteAction?.Invoke(FilterInventorySlot.Boot);
        }

        private void ExecuteShowHideMountFilter()
        {
            ExecuteAction?.Invoke(FilterInventorySlot.Horse);
        }

        private void ExecuteShowHideHarnessFilter()
        {
            ExecuteAction?.Invoke(FilterInventorySlot.HorseHarness);
        }

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
                    OnPropertyChangedWithValue(value, nameof(IsHelmFilterSelected));
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
                    OnPropertyChangedWithValue(value, nameof(IsHelmFilterLocked));
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
                    OnPropertyChangedWithValue(value, nameof(IsCloakFilterSelected));
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
                    OnPropertyChangedWithValue(value, nameof(IsCloakFilterLocked));
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
                    OnPropertyChangedWithValue(value, nameof(IsArmorFilterSelected));
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
                    OnPropertyChangedWithValue(value, nameof(IsArmorFilterLocked));
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
                    OnPropertyChangedWithValue(value, nameof(IsGloveFilterSelected));
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
                    OnPropertyChangedWithValue(value, nameof(IsGloveFilterLocked));
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
                    OnPropertyChangedWithValue(value, nameof(IsBootFilterSelected));
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
                    OnPropertyChangedWithValue(value, nameof(IsBootFilterLocked));
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
                    OnPropertyChangedWithValue(value, nameof(IsMountFilterSelected)); ;
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
                    OnPropertyChangedWithValue(value, nameof(IsMountFilterLocked)); ;
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
                    OnPropertyChangedWithValue(value, nameof(IsHarnessFilterSelected)); ;
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
                    OnPropertyChangedWithValue(value, nameof(IsHarnessFilterLocked));
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
                    OnPropertyChangedWithValue(value, nameof(IsWeapon1FilterSelected));
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
                    OnPropertyChangedWithValue(value, nameof(IsWeapon1FilterLocked));
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
                    OnPropertyChangedWithValue(value, nameof(IsWeapon2FilterSelected));
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
                    OnPropertyChangedWithValue(value, nameof(IsWeapon2FilterLocked));
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
                    OnPropertyChangedWithValue(value, nameof(IsWeapon3FilterSelected));
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
                    OnPropertyChangedWithValue(value, nameof(IsWeapon3FilterLocked));
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
                    OnPropertyChangedWithValue(value, nameof(IsWeapon4FilterSelected));
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
                    OnPropertyChangedWithValue(value, nameof(IsWeapon4FilterLocked));
                }
            }
        }
    }
}
