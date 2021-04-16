using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    public class FilterCopyPasteVM : ViewModel
    {
        private FilterArmorSettings _armorSettings;
        private FilterMountSettings _mountSettings;
        private FilterWeaponSettings _weaponSettings;
        private CharacterSettings _characterSettings;

        private FilterViewModel _filterVM;

        private bool _itemCopied;
        private bool _characterCopied;

        public FilterCopyPasteVM(FilterViewModel filterVM)
        {
            _filterVM = filterVM ?? throw new NullReferenceException();
            _filterVM.UpdateActions.Add(Update);
        }

        private void Update(FilterInventorySlot state)
        {
            if (state >= FilterInventorySlot.Helm && state <= FilterInventorySlot.HorseHarness)
            {
                ItemCopied = _armorSettings != null;
            }
            else if (state == FilterInventorySlot.Horse)
            {
                ItemCopied = _mountSettings != null;
            }
            else if (state >= FilterInventorySlot.Weapon1 && state <= FilterInventorySlot.Weapon4)
            {
                ItemCopied = _weaponSettings != null;
            }

            CharacterCopied = _characterSettings != null;
        }

        private void CopyFilter()
        {
            var state = _filterVM.CurrentState;
            if (state >= FilterInventorySlot.Helm && state <= FilterInventorySlot.HorseHarness)
            {
                _armorSettings = new FilterArmorSettings((FilterArmorSettings) _filterVM.CurrentCharacterSettings.GetFilter(state));
            }
            else if (state == FilterInventorySlot.Horse)
            {
                _mountSettings = new FilterMountSettings((FilterMountSettings)_filterVM.CurrentCharacterSettings.GetFilter(state));
            }
            else if (state >= FilterInventorySlot.Weapon1 && state <= FilterInventorySlot.Weapon4)
            {
                _weaponSettings = new FilterWeaponSettings((FilterWeaponSettings)_filterVM.CurrentCharacterSettings.GetFilter(state));
            }
            DisplayMessage(true, false);
            Update(state);
        }

        private void CopyCharacter()
        {
            _characterSettings = new CharacterSettings(_filterVM.CurrentCharacterSettings);
            DisplayMessage(true, true);
            Update(_filterVM.CurrentState);
        }

        private void PasteFilter()
        {
            var state = _filterVM.CurrentState;
            if (state >= FilterInventorySlot.Helm && state <= FilterInventorySlot.HorseHarness)
            {
                _filterVM.CurrentCharacterSettings.SetFilter(state, new FilterArmorSettings(_armorSettings));
            }
            else if (state == FilterInventorySlot.Horse)
            {
                _filterVM.CurrentCharacterSettings.SetFilter(state, new FilterMountSettings(_mountSettings));
            }
            else if (state >= FilterInventorySlot.Weapon1 && state <= FilterInventorySlot.Weapon4)
            {
                _filterVM.CurrentCharacterSettings.SetFilter(state, new FilterWeaponSettings(_weaponSettings));
            }
            SaveProfile();
            DisplayMessage(false, false);
            _filterVM.UpdateValues();
        }

        private void PasteCharacter()
        {
            var tempName = _filterVM.CurrentCharacterSettings.Name;
            _filterVM.CurrentCharacterSettings = new CharacterSettings(_characterSettings) { Name = tempName };
            SaveProfile();
            DisplayMessage(false, true);
            _filterVM.UpdateValues();
        }

        private void DisplayMessage(bool copied, bool showCharacterMessage)
        {
            string word = copied ? "copied" : "pasted";
            FilterInventorySlot state = _filterVM.CurrentState;
            if (showCharacterMessage)
            {
                InformationManager.DisplayMessage(new InformationMessage($"Character settings {word}"));
            }
            else if (state >= FilterInventorySlot.Helm && state <= FilterInventorySlot.HorseHarness)
            {
                InformationManager.DisplayMessage(new InformationMessage($"Armor settings {word}"));
            }
            else if (state == FilterInventorySlot.Horse)
            {
                InformationManager.DisplayMessage(new InformationMessage($"Mount settings {word}"));
            }
            else if (state >= FilterInventorySlot.Weapon1 && state <= FilterInventorySlot.Weapon4)
            {
                InformationManager.DisplayMessage(new InformationMessage($"Weapon settings {word}"));
            }
        }

        private void SaveProfile()
        {
            SettingsLoader.Instance.SetCharacterSettingsByName(_filterVM.CurrentCharacterSettings,
                InventoryBehavior.Inventory.CurrentCharacterName);
        }

        [DataSourceProperty]
        public bool ItemCopied
        {
            get => _itemCopied;
            set
            {
                if (value == _itemCopied)
                    return;
                _itemCopied = value;
                OnPropertyChangedWithValue(value, nameof(ItemCopied));
            }
        }

        [DataSourceProperty]
        public bool CharacterCopied
        {
            get => _characterCopied;
            set
            {
                if (value == _characterCopied)
                    return;
                _characterCopied = true;
                OnPropertyChangedWithValue(value, nameof(CharacterCopied));
            }
        }
    }
}
