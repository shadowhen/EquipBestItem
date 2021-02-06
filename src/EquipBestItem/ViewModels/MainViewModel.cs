using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem
{
    class MainViewModel : ViewModel
    {
        private InventoryLogic _inventoryLogic;
        private SPInventoryVM _inventory;
        private Equipment _bestLeftEquipment;
        private Equipment _bestRightEquipment;

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
            _inventoryLogic = InventoryManager.InventoryLogic;
            _inventory = InventoryBehavior.Inventory;

            bestEquipmentUpgrader = new BestEquipmentUpgrader();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            bestEquipmentUpgrader.RefreshValues();

            // Check for null for logic and inventory, so we won't crash in some cases beyond our control
            // when using other mods
            if (_inventoryLogic == null)
                _inventoryLogic = InventoryManager.InventoryLogic;
            if (_inventory == null)
                _inventory = InventoryBehavior.Inventory;

            // Create a character data based on given character name and character settings
            var character = GetCharacterByName(_inventory.CurrentCharacterName);
            var characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(character.Name.ToString());
            var characterData = new CharacterData(character, characterSettings);

            // Update the equipment upgrader with the newest character data
            bestEquipmentUpgrader.SetCharacterData(characterData);

            // Fetch the character's current equipment depending we are in 
            // a war set or civilian set
            Equipment equipment = _inventory.IsInWarSet ? characterData.GetBattleEquipment() : characterData.GetCivilianEquipment();
            
            // These two would be used to get the best equipment from both sides
            _bestLeftEquipment = new Equipment();
            _bestRightEquipment = new Equipment();

            // Loops through the character's equipment to find best items
            for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
            {
                if (equipment[equipmentIndex].IsEmpty && equipmentIndex < EquipmentIndex.NonWeaponItemBeginSlot ||
                    equipment[EquipmentIndex.Horse].IsEmpty && equipmentIndex == EquipmentIndex.HorseHarness)
                    continue;

                EquipmentElement bestLeftEquipmentElement;
                EquipmentElement bestRightEquipmentElement;

                // Depending on the panel locks, the best equipment would be fetched if the panel is not locked.
                if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
                {
                    bestLeftEquipmentElement = bestEquipmentUpgrader.GetBetterItemFromSide(_inventory.LeftItemListVM, equipment[equipmentIndex], equipmentIndex, !_inventory.IsInWarSet);

                }
                if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
                {
                    bestRightEquipmentElement = bestEquipmentUpgrader.GetBetterItemFromSide(_inventory.RightItemListVM, equipment[equipmentIndex], equipmentIndex, !_inventory.IsInWarSet);
                
                }

                // After getting the best items from both panels, we need to find the best item between
                // the two items
                if (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null)
                {
                    if (bestRightEquipmentElement.Item == null)
                    {
                        _bestLeftEquipment[equipmentIndex] = bestLeftEquipmentElement;
                    }
                    else if (bestLeftEquipmentElement.Item == null)
                    {
                        _bestRightEquipment[equipmentIndex] = bestRightEquipmentElement;
                    }
                    else if (bestEquipmentUpgrader.ItemIndexCalculation(bestLeftEquipmentElement, equipmentIndex) > bestEquipmentUpgrader.ItemIndexCalculation(bestRightEquipmentElement, equipmentIndex))
                    {
                        _bestLeftEquipment[equipmentIndex] = bestLeftEquipmentElement;
                    }
                    else
                    {
                        _bestRightEquipment[equipmentIndex] = bestRightEquipmentElement;
                    }
                }
            }

            // Updates whether the character should be able to upgrade the item
            IsHelmButtonEnabled = !_bestLeftEquipment[EquipmentIndex.Head].IsEmpty || !_bestRightEquipment[EquipmentIndex.Head].IsEmpty;
            IsCloakButtonEnabled = !_bestLeftEquipment[EquipmentIndex.Cape].IsEmpty || !_bestRightEquipment[EquipmentIndex.Cape].IsEmpty;
            IsArmorButtonEnabled = !_bestLeftEquipment[EquipmentIndex.Body].IsEmpty || !_bestRightEquipment[EquipmentIndex.Body].IsEmpty;
            IsGloveButtonEnabled = !_bestLeftEquipment[EquipmentIndex.Gloves].IsEmpty || !_bestRightEquipment[EquipmentIndex.Gloves].IsEmpty;
            IsBootButtonEnabled = !_bestLeftEquipment[EquipmentIndex.Leg].IsEmpty || !_bestRightEquipment[EquipmentIndex.Leg].IsEmpty;
            IsMountButtonEnabled = !_bestLeftEquipment[EquipmentIndex.Horse].IsEmpty || !_bestRightEquipment[EquipmentIndex.Horse].IsEmpty;
            IsHarnessButtonEnabled = !_bestLeftEquipment[EquipmentIndex.HorseHarness].IsEmpty || !_bestRightEquipment[EquipmentIndex.HorseHarness].IsEmpty;
            IsWeapon1ButtonEnabled = !_bestLeftEquipment[EquipmentIndex.Weapon0].IsEmpty || !_bestRightEquipment[EquipmentIndex.Weapon0].IsEmpty;
            IsWeapon2ButtonEnabled = !_bestLeftEquipment[EquipmentIndex.Weapon1].IsEmpty || !_bestRightEquipment[EquipmentIndex.Weapon1].IsEmpty;
            IsWeapon3ButtonEnabled = !_bestLeftEquipment[EquipmentIndex.Weapon2].IsEmpty || !_bestRightEquipment[EquipmentIndex.Weapon2].IsEmpty;
            IsWeapon4ButtonEnabled = !_bestLeftEquipment[EquipmentIndex.Weapon3].IsEmpty || !_bestRightEquipment[EquipmentIndex.Weapon3].IsEmpty;

            // Loops through the equipment slots to determine if the character should be able to upgrade their
            // equipment with the best items
            for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
            {
                if (_bestLeftEquipment[equipmentIndex].IsEmpty && _bestRightEquipment[equipmentIndex].IsEmpty)
                {
                    IsEquipCurrentCharacterButtonEnabled = false;
                }
                else
                {
                    IsEquipCurrentCharacterButtonEnabled = true;
                    break;
                }
            }

#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage("MainViewModel RefreshValues()"));
#endif
        }

        /// <summary>
        /// Equips every character with the best items
        /// </summary>
        public void EquipEveryCharacter()
        {
            bestEquipmentUpgrader.RefreshValues();
            foreach (TroopRosterElement rosterElement in _inventoryLogic.RightMemberRoster.GetTroopRoster())
            {
                if (rosterElement.Character.IsHero)
                    bestEquipmentUpgrader.EquipCharacter(rosterElement.Character);
            }
        }

        /// <summary>
        /// Returns a character object with a given name
        /// </summary>
        /// <param name="name">character name</param>
        /// <returns>character object</returns>
        public CharacterObject GetCharacterByName(string name)
        {
            foreach (TroopRosterElement rosterElement in _inventoryLogic.RightMemberRoster.GetTroopRoster())
            {
                if (rosterElement.Character.IsHero && rosterElement.Character.Name.ToString() == name)
                    return rosterElement.Character;
            }

            // Crash fix for the mod Party AI Overhaul and Commands
            foreach (TroopRosterElement rosterElement in _inventoryLogic.MerchantParty.MemberRoster.GetTroopRoster())
            {
                if (rosterElement.Character.IsHero && rosterElement.Character.Name.ToString() == name)
                {
                    return rosterElement.Character;
                }
            }

            return null;
        }

        /// <summary>
        /// Equips the character with the best equipment item in the equipment slot
        /// </summary>
        /// <param name="equipmentIndex">Equipment slot</param>
        public void EquipBestItem(EquipmentIndex equipmentIndex)
        {
            Equipment equipment = _inventory.IsInWarSet ? bestEquipmentUpgrader.GetCharacterData().GetBattleEquipment() : bestEquipmentUpgrader.GetCharacterData().GetCivilianEquipment();
            // Unequip current equipment element
            if (!equipment[equipmentIndex].IsEmpty)
            {
                TransferCommand transferCommand = TransferCommand.Transfer(
                    1,
                    InventoryLogic.InventorySide.Equipment,
                    InventoryLogic.InventorySide.PlayerInventory,
                    new ItemRosterElement(equipment[equipmentIndex], 1),
                    equipmentIndex,
                    EquipmentIndex.None,
                    bestEquipmentUpgrader.GetCharacterData().GetCharacterObject(),
                    !_inventory.IsInWarSet
                );
                _inventoryLogic.AddTransferCommand(transferCommand);
            }
            
            // Equip
            if (bestEquipmentUpgrader.ItemIndexCalculation(_bestLeftEquipment[equipmentIndex], equipmentIndex) > bestEquipmentUpgrader.ItemIndexCalculation(_bestRightEquipment[equipmentIndex], equipmentIndex))
            {
                TransferCommand equipCommand = TransferCommand.Transfer(
                    1,
                    InventoryLogic.InventorySide.OtherInventory,
                    InventoryLogic.InventorySide.Equipment,
                    new ItemRosterElement(_bestLeftEquipment[equipmentIndex], 1),
                    EquipmentIndex.None,
                    equipmentIndex,
                    bestEquipmentUpgrader.GetCharacterData().GetCharacterObject(),
                    !_inventory.IsInWarSet
                );

                _inventoryLogic.AddTransferCommand(equipCommand);
            }
            else
            {
                TransferCommand equipCommand = TransferCommand.Transfer(
                    1,
                    InventoryLogic.InventorySide.PlayerInventory,
                    InventoryLogic.InventorySide.Equipment,
                    new ItemRosterElement(_bestRightEquipment[equipmentIndex], 1),
                    EquipmentIndex.None,
                    equipmentIndex,
                    bestEquipmentUpgrader.GetCharacterData().GetCharacterObject(),
                    !_inventory.IsInWarSet
                );

                _inventoryLogic.AddTransferCommand(equipCommand);
            }
            _inventory.GetMethod("ExecuteRemoveZeroCounts");
            _inventory.GetMethod("RefreshInformationValues");
        }

        public void ExecuteEquipEveryCharacter()
        {
            EquipEveryCharacter();
        }

        public void ExecuteEquipCurrentCharacter()
        {
            bestEquipmentUpgrader.EquipCharacter(GetCharacterByName(_inventory.CurrentCharacterName));
            this.RefreshValues();

#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage("ExecuteEquipCurrentCharacter"));
#endif
        }

        public void ExecuteEquipBestHelm()
        {
            EquipBestItem(EquipmentIndex.Head);
        }

        public void ExecuteEquipBestCloak()
        {
            EquipBestItem(EquipmentIndex.Cape);
        }

        public void ExecuteEquipBestArmor()
        {
            EquipBestItem(EquipmentIndex.Body);
        }

        public void ExecuteEquipBestGlove()
        {
            EquipBestItem(EquipmentIndex.Gloves);
        }

        public void ExecuteEquipBestBoot()
        {
            EquipBestItem(EquipmentIndex.Leg);
        }

        public void ExecuteEquipBestMount()
        {
            EquipBestItem(EquipmentIndex.Horse);
        }

        public void ExecuteEquipBestHarness()
        {
            EquipBestItem(EquipmentIndex.HorseHarness);
        }

        public void ExecuteEquipBestWeapon1()
        {
            EquipBestItem(EquipmentIndex.Weapon0);
        }

        public void ExecuteEquipBestWeapon2()
        {
            EquipBestItem(EquipmentIndex.Weapon1);
        }

        public void ExecuteEquipBestWeapon3()
        {
            EquipBestItem(EquipmentIndex.Weapon2);
        }

        public void ExecuteEquipBestWeapon4()
        {
            EquipBestItem(EquipmentIndex.Weapon3);
        }
    }
}
