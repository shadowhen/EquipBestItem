using System;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    public class FilterIconsVM : ViewModel
    {
        public Action<FilterInventorySlot> ExecuteAction { get; set; }

        private FilterIconVM _helmIconVM = new FilterIconVM();
        private FilterIconVM _cloakIconVM = new FilterIconVM();
        private FilterIconVM _bodyIconVM = new FilterIconVM();
        private FilterIconVM _gloveIconVM = new FilterIconVM();
        private FilterIconVM _bootIconVM = new FilterIconVM();

        private FilterIconVM _mountIconVM = new FilterIconVM();
        private FilterIconVM _harnessIconVM = new FilterIconVM();

        private FilterIconVM _weapon1IconVM = new FilterIconVM();
        private FilterIconVM _weapon2IconVM = new FilterIconVM();
        private FilterIconVM _weapon3IconVM = new FilterIconVM();
        private FilterIconVM _weapon4IconVM = new FilterIconVM();

        public FilterIconsVM(Action<FilterInventorySlot> executeAction)
        {
            ExecuteAction = executeAction;

            _helmIconVM.ExecuteAction = () => { ExecuteAction.Invoke(FilterInventorySlot.Helm); };
            _cloakIconVM.ExecuteAction = () => { ExecuteAction.Invoke(FilterInventorySlot.Cloak); };
            _bodyIconVM.ExecuteAction = () => { ExecuteAction.Invoke(FilterInventorySlot.Body); };
            _gloveIconVM.ExecuteAction = () => { ExecuteAction.Invoke(FilterInventorySlot.Gloves); };
            _bootIconVM.ExecuteAction = () => { ExecuteAction.Invoke(FilterInventorySlot.Boot); };

            _mountIconVM.ExecuteAction = () => { ExecuteAction.Invoke(FilterInventorySlot.Horse); };
            _harnessIconVM.ExecuteAction = () => { ExecuteAction.Invoke(FilterInventorySlot.HorseHarness); };

            _weapon1IconVM.ExecuteAction = () => { ExecuteAction.Invoke(FilterInventorySlot.Weapon1); };
            _weapon2IconVM.ExecuteAction = () => { ExecuteAction.Invoke(FilterInventorySlot.Weapon2); };
            _weapon3IconVM.ExecuteAction = () => { ExecuteAction.Invoke(FilterInventorySlot.Weapon3); };
            _weapon4IconVM.ExecuteAction = () => { ExecuteAction.Invoke(FilterInventorySlot.Weapon4); };
        }

        public void UpdateIcons(CharacterSettings characterSettings)
        {
            if (characterSettings == null)
                return;

            //Helmet icon state
            _helmIconVM.IsSelected = characterSettings.FilterArmor[0].IsNotDefault();
            _helmIconVM.IsLocked = characterSettings.FilterArmor[0].IsZero();

            //Cloak icon state
            _cloakIconVM.IsSelected = characterSettings.FilterArmor[1].IsNotDefault();
            _cloakIconVM.IsLocked = characterSettings.FilterArmor[1].IsZero();

            //Armor icon state
            _bodyIconVM.IsSelected = characterSettings.FilterArmor[2].IsNotDefault();
            _bodyIconVM.IsLocked = characterSettings.FilterArmor[2].IsZero();

            //Gloves icon state
            _gloveIconVM.IsSelected = characterSettings.FilterArmor[3].IsNotDefault();
            _gloveIconVM.IsLocked = characterSettings.FilterArmor[3].IsZero();

            //Boots icon state
            _bootIconVM.IsSelected = characterSettings.FilterArmor[4].IsNotDefault();
            _bootIconVM.IsLocked = characterSettings.FilterArmor[4].IsZero();

            //Mount icon state
            _mountIconVM.IsSelected = characterSettings.FilterMount.IsNotDefault();
            _mountIconVM.IsLocked = characterSettings.FilterMount.IsZero();

            //Harness icon state
            _harnessIconVM.IsSelected = characterSettings.FilterArmor[5].IsNotDefault();
            _harnessIconVM.IsLocked = characterSettings.FilterArmor[5].IsZero();

            //Weapon1 icon state
            _weapon1IconVM.IsSelected = characterSettings.FilterWeapon[0].IsNotDefault();
            _weapon1IconVM.IsLocked = characterSettings.FilterWeapon[0].IsZero();

            //Weapon2 icon state
            _weapon2IconVM.IsSelected = characterSettings.FilterWeapon[1].IsNotDefault();
            _weapon2IconVM.IsLocked = characterSettings.FilterWeapon[1].IsZero();

            //Weapon3 icon state
            _weapon3IconVM.IsSelected = characterSettings.FilterWeapon[2].IsNotDefault();
            _weapon3IconVM.IsLocked = characterSettings.FilterWeapon[2].IsZero();

            //Weapon4 icon state
            _weapon4IconVM.IsSelected = characterSettings.FilterWeapon[3].IsNotDefault();
            _weapon4IconVM.IsLocked = characterSettings.FilterWeapon[3].IsZero();
        }

        [DataSourceProperty]
        public FilterIconVM HelmIconVM
        {
            get => _helmIconVM;
            set
            {
                if (_helmIconVM == value)
                    return;
                _helmIconVM = value;
                OnPropertyChangedWithValue(value, nameof(HelmIconVM));
            }
        }

        [DataSourceProperty]
        public FilterIconVM CloakIconVM
        {
            get => _cloakIconVM;
            set
            {
                if (_cloakIconVM == value)
                    return;
                _cloakIconVM = value;
                OnPropertyChangedWithValue(value, nameof(CloakIconVM));
            }
        }

        [DataSourceProperty]
        public FilterIconVM BodyIconVM
        {
            get => _bodyIconVM;
            set
            {
                if (_bodyIconVM == value)
                    return;
                _bodyIconVM = value;
                OnPropertyChangedWithValue(value, nameof(BodyIconVM));
            }
        }

        [DataSourceProperty]
        public FilterIconVM GloveIconVM
        {
            get => _gloveIconVM;
            set
            {
                if (_gloveIconVM == value)
                    return;
                _gloveIconVM = value;
                OnPropertyChangedWithValue(value, nameof(GloveIconVM));
            }
        }

        [DataSourceProperty]
        public FilterIconVM BootIconVM
        {
            get => _bootIconVM;
            set
            {
                if (_bootIconVM == value)
                    return;
                _bootIconVM = value;
                OnPropertyChangedWithValue(value, nameof(BootIconVM));
            }
        }

        [DataSourceProperty]
        public FilterIconVM MountIconVM
        {
            get => _mountIconVM;
            set
            {
                if (_mountIconVM == value)
                    return;
                _mountIconVM = value;
                OnPropertyChangedWithValue(value, nameof(MountIconVM));
            }
        }

        [DataSourceProperty]
        public FilterIconVM HarnessIconVM
        {
            get => _harnessIconVM;
            set
            {
                if (_harnessIconVM == value)
                    return;
                _harnessIconVM = value;
                OnPropertyChangedWithValue(value, nameof(HarnessIconVM));
            }
        }

        [DataSourceProperty]
        public FilterIconVM Weapon1IconVM
        {
            get => _weapon1IconVM;
            set
            {
                if (_weapon1IconVM == value)
                    return;
                _weapon1IconVM = value;
                OnPropertyChangedWithValue(value, nameof(Weapon1IconVM));
            }
        }

        [DataSourceProperty]
        public FilterIconVM Weapon2IconVM
        {
            get => _weapon2IconVM;
            set
            {
                if (_weapon2IconVM == value)
                    return;
                _weapon2IconVM = value;
                OnPropertyChangedWithValue(value, nameof(Weapon2IconVM));
            }
        }

        [DataSourceProperty]
        public FilterIconVM Weapon3IconVM
        {
            get => _weapon3IconVM;
            set
            {
                if (_weapon3IconVM == value)
                    return;
                _weapon3IconVM = value;
                OnPropertyChangedWithValue(value, nameof(Weapon3IconVM));
            }
        }

        [DataSourceProperty]
        public FilterIconVM Weapon4IconVM
        {
            get => _weapon4IconVM;
            set
            {
                if (_weapon4IconVM == value)
                    return;
                _weapon4IconVM = value;
                OnPropertyChangedWithValue(value, nameof(Weapon4IconVM));
            }
        }

        private bool _iconsHidden;

        [DataSourceProperty]
        public bool IconsHidden
        {
            get => _iconsHidden;
            set
            {
                if (_iconsHidden == value)
                    return;
                _iconsHidden = value;
                OnPropertyChangedWithValue(value, nameof(IconsHidden));
            }
        }
    }
}
