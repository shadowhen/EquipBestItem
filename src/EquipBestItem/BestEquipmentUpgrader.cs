using System;
using System.Collections.Generic;
using Helpers;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem
{
    class BestEquipmentUpgrader
    {
        private CharacterData _characterData;

        private SPInventoryVM _inventory;
        private InventoryLogic _inventoryLogic;

        private Equipment _bestLeftEquipment;
        private Equipment _bestRightEquipment;

        public BestEquipmentUpgrader()
        {
            _characterData = null;
        }

        /// <summary>
        /// Refresh the equipment upgrader with new inventory and inventory logic
        /// </summary>
        public void RefreshValues()
        {
            _inventoryLogic = InventoryManager.InventoryLogic;
            _inventory = InventoryBehavior.Inventory;

            // Create a character data based on given character name and character settings
            var character = GetCharacterByName(_inventory.CurrentCharacterName);
            //var characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(character.Name.ToString());
            var characterSettings = InventoryBehavior.GetCharacterSettingsByName(character.Name.ToString());
            _characterData = new CharacterData(character, characterSettings);

            // Fetch the character's current equipment depending we are in 
            // a war set or civilian set
            Equipment equipment = _inventory.IsInWarSet ? _characterData.GetBattleEquipment() : _characterData.GetCivilianEquipment();

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
                    bestLeftEquipmentElement = GetBetterItemFromSide(_inventory.LeftItemListVM, equipment[equipmentIndex], equipmentIndex, !_inventory.IsInWarSet);

                }
                if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
                {
                    bestRightEquipmentElement = GetBetterItemFromSide(_inventory.RightItemListVM, equipment[equipmentIndex], equipmentIndex, !_inventory.IsInWarSet);

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
                    else if (ItemIndexCalculation(bestLeftEquipmentElement, equipmentIndex) > ItemIndexCalculation(bestRightEquipmentElement, equipmentIndex))
                    {
                        _bestLeftEquipment[equipmentIndex] = bestLeftEquipmentElement;
                    }
                    else
                    {
                        _bestRightEquipment[equipmentIndex] = bestRightEquipmentElement;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a character object with a given name
        /// </summary>
        /// <param name="name">character name</param>
        /// <returns>character object</returns>
        private CharacterObject GetCharacterByName(string name)
        {
            var rightMemberRoster = _inventoryLogic.RightMemberRoster.GetTroopRoster();
            if (rightMemberRoster != null)
            {
                foreach (TroopRosterElement rosterElement in rightMemberRoster)
                {
                    if (rosterElement.Character.IsHero && rosterElement.Character.Name.ToString() == name)
                        return rosterElement.Character;
                }
            }

            // Crash fix for the mod Party AI Overhaul and Commands
            if (_inventoryLogic.MerchantParty != null)
            {
                var merchantPartyMemberRoster = _inventoryLogic.MerchantParty.MemberRoster.GetTroopRoster();
                foreach (TroopRosterElement rosterElement in merchantPartyMemberRoster)
                {
                    if (rosterElement.Character.IsHero && rosterElement.Character.Name.ToString() == name)
                    {
                        return rosterElement.Character;
                    }
                }
            }

            // Returns only the initial equipment character 
            return _inventoryLogic.InitialEquipmentCharacter;
        }

        /// <summary>
        /// Get the best item from inventory side
        /// </summary>
        /// <param name="itemListVM">List of items on the inventory side</param>
        /// <param name="equipmentElement">Current item</param>
        /// <param name="slot">Type of item</param>
        /// <param name="isCivilian">Civilian Type</param>
        /// <returns></returns>
        private EquipmentElement GetBetterItemFromSide(MBBindingList<SPItemVM> itemListVM, EquipmentElement equipmentElement, EquipmentIndex slot, bool isCivilian)
        {
            EquipmentElement bestEquipmentElement;

            CharacterObject character = _characterData.GetCharacterObject();

            // Loops through the inventory list to find the best equipment item
            foreach (SPItemVM item in itemListVM)
            {
                // Skips through the process if the item is camel or camel harness
                if (IsCamel(item) || IsCamelHarness(item))
                    continue;

                // Skips only if the character cannot use the item or the item is not equipable
                if (!CharacterHelper.CanUseItem(character, item.ItemRosterElement.EquipmentElement) || !item.IsEquipableItem)
                    continue;

                // Skips only if we are looking for civilian equipment and item is not civilian equipment
                if (isCivilian && !item.IsCivilianItem)
                    continue;

                if (slot < EquipmentIndex.NonWeaponItemBeginSlot &&
                    item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon != null)
                {
                    bool sameWeaponClass = equipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.WeaponClass;
                    bool sameItemUsage = GetItemUsage(item) == equipmentElement.Item.PrimaryWeapon.ItemUsage;
                    
                    bool couchWeapon = IsCouchWeapon(equipmentElement);
                    bool couchUsage = !couchWeapon || IsCouchWeapon(item.ItemRosterElement.EquipmentElement);

                    if (sameWeaponClass && sameItemUsage && couchUsage)
                    {
                        bestEquipmentElement = GetBestEquipmentElement(slot, item.ItemRosterElement.EquipmentElement, equipmentElement, bestEquipmentElement);
                    }
                }
                else if (item.ItemType == slot)
                {
                    bestEquipmentElement = GetBestEquipmentElement(slot, item.ItemRosterElement.EquipmentElement, equipmentElement, bestEquipmentElement);
                }

            }

            return bestEquipmentElement;
        }

        /// <summary>
        /// Returns the best equipment element
        /// </summary>
        /// <param name="slot">Equipment slot</param>
        /// <param name="inventoryEquipmentElement">inventory equipment</param>
        /// <param name="currentEquipmentElement">current equipment</param>
        /// <param name="bestEquipmentElement">best equipment</param>
        /// <returns></returns>
        private EquipmentElement GetBestEquipmentElement(EquipmentIndex slot, EquipmentElement inventoryEquipmentElement, EquipmentElement currentEquipmentElement, EquipmentElement bestEquipmentElement)
        {
            float inventoryItemValue = ItemIndexCalculation(inventoryEquipmentElement, slot);
            float currentItemValue = bestEquipmentElement.IsEmpty ? ItemIndexCalculation(currentEquipmentElement, slot) : ItemIndexCalculation(bestEquipmentElement, slot);

            if (inventoryItemValue > currentItemValue && Math.Abs(inventoryItemValue) > 0.0001f)
            {
                return inventoryEquipmentElement;
            }
            return bestEquipmentElement;
        }

        /// <summary>
        /// Returns a boolean if the item is a camel
        /// </summary>
        /// <param name="item">inventory item</param>
        /// <returns></returns>
        private static bool IsCamel(SPItemVM item)
        {
            if (item != null)
                if (!item.ItemRosterElement.IsEmpty)
                    if (!item.ItemRosterElement.EquipmentElement.IsEmpty)
                        if (item.ItemRosterElement.EquipmentElement.Item.HasHorseComponent)
                            if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Monster.MonsterUsage == "camel")
                                return true;
            return false;
        }

        /// <summary>
        /// Returns a boolean if the is a camel harness
        /// </summary>
        /// <param name="item">inventory item</param>
        /// <returns></returns>
        private static bool IsCamelHarness(SPItemVM item)
        {
            return item != null && item.StringId.StartsWith("camel_sadd");
        }

        private static bool IsCouchWeapon(EquipmentElement weapon)
        {
            if (weapon.IsEmpty)
                return false;
            if (weapon.Item.Weapons == null || weapon.Item.Weapons.Count == 0)
                return false;

            foreach (var temp in weapon.Item.Weapons)
            {
                if (temp.ItemUsage == null)
                    continue;

                if (temp.ItemUsage.Contains("couch"))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns item usage in string
        /// </summary>
        /// <param name="item">inventory item</param>
        /// <returns></returns>
        private static string GetItemUsage(SPItemVM item)
        {
            if (item == null || item.ItemRosterElement.IsEmpty || item.ItemRosterElement.EquipmentElement.IsEmpty || item.ItemRosterElement.EquipmentElement.Item.WeaponComponent == null)
                return "";
            return item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.ItemUsage;
        }

        /// <summary>
        /// Equips every character with the best items
        /// </summary>
        public void EquipEveryCharacter()
        {
            RefreshValues();
            foreach (TroopRosterElement rosterElement in _inventoryLogic.RightMemberRoster.GetTroopRoster())
            {
                if (rosterElement.Character.IsHero)
                {
                    var character = rosterElement.Character;
                    //var characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(character.Name.ToString());
                    var characterSettings = InventoryBehavior.GetCharacterSettingsByName(character.Name.ToString());
                    _characterData = new CharacterData(character, characterSettings);
                    Equipment characterEquipment = _inventory.IsInWarSet ? _characterData.GetBattleEquipment() : _characterData.GetCivilianEquipment();
                    EquipCharacterEquipment(characterEquipment, !_inventory.IsInWarSet);
                }
            }
        }

        /// <summary>
        /// Equips the current character with the best equipment
        /// </summary>
        public void EquipCurrentCharacter()
        {
            bool civilian = !_inventory.IsInWarSet;
            Equipment characterEquipment = civilian ? _characterData.GetCivilianEquipment() : _characterData.GetBattleEquipment();
            EquipCharacterEquipment(characterEquipment, civilian);
        }

        /// <summary>
        /// Equips the characterwith the best equipment
        /// </summary>
        /// <param name="equipment">character's equipment</param>
        /// <param name="isCivilian">equipment civilian type</param>
        private void EquipCharacterEquipment(Equipment equipment, bool isCivilian)
        {
            // Loops through the character's equipment to equip the character with the best items
            for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
            {
                if (equipment[equipmentIndex].IsEmpty && equipmentIndex < EquipmentIndex.NonWeaponItemBeginSlot ||
                    equipment[EquipmentIndex.Horse].IsEmpty && equipmentIndex == EquipmentIndex.HorseHarness)
                    continue;
                
                EquipmentElement bestLeftEquipmentElement;
                EquipmentElement bestRightEquipmentElement;

                // Gets the best item from sides that are unlocked
                if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
                {
                    bestLeftEquipmentElement = GetBetterItemFromSide(_inventory.LeftItemListVM, equipment[equipmentIndex], equipmentIndex, isCivilian);
                }
                if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
                {
                    bestRightEquipmentElement = GetBetterItemFromSide(_inventory.RightItemListVM, equipment[equipmentIndex], equipmentIndex, isCivilian);
                }

                if (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null)
                {
                    EquipBestItem(equipmentIndex, bestLeftEquipmentElement, bestRightEquipmentElement);
                    EquipMessage(equipmentIndex);
                }

                if (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null)
                {
                    if (ItemIndexCalculation(bestLeftEquipmentElement, equipmentIndex) > ItemIndexCalculation(bestRightEquipmentElement, equipmentIndex))
                    {
                        TransferCommand equipCommand = TransferCommand.Transfer(
                            1,
                            InventoryLogic.InventorySide.OtherInventory,
                            InventoryLogic.InventorySide.Equipment,
                            new ItemRosterElement(bestLeftEquipmentElement, 1),
                            EquipmentIndex.None,
                            equipmentIndex,
                            _characterData.GetCharacterObject(),
                            isCivilian
                        );

                        EquipMessage(equipmentIndex);
                        _inventoryLogic.AddTransferCommand(equipCommand);
                    }
                    else
                    {
                        TransferCommand equipCommand = TransferCommand.Transfer(
                            1,
                            InventoryLogic.InventorySide.PlayerInventory,
                            InventoryLogic.InventorySide.Equipment,
                            new ItemRosterElement(bestRightEquipmentElement, 1),
                            EquipmentIndex.None,
                            equipmentIndex,
                            _characterData.GetCharacterObject(),
                            isCivilian
                        );

                        EquipMessage(equipmentIndex);
                        _inventoryLogic.AddTransferCommand(equipCommand);
                    }
                }
                _inventory.ExecuteRemoveZeroCounts();
            }
            _inventory.GetMethod("RefreshInformationValues");
        }

        /// <summary>
        /// Equips the current character with the best item
        /// </summary>
        /// <param name="equipmentIndex">Equipment Index</param>
        public void EquipBestItem(EquipmentIndex equipmentIndex)
        {
            EquipBestItem(equipmentIndex, _bestLeftEquipment[equipmentIndex], _bestRightEquipment[equipmentIndex]);
            _inventory.ExecuteRemoveZeroCounts();
            _inventory.GetMethod("RefreshInformationValues");
        }

        /// <summary>
        /// Equips the current character with the best item
        /// </summary>
        /// <param name="equipmentIndex">Equipment Index</param>
        /// <param name="bestLeftEquipmentElement">Best Left Equipment Item</param>
        /// <param name="bestRightEquipmentElement">Best Right Equipment Item</param>
        private void EquipBestItem(EquipmentIndex equipmentIndex, EquipmentElement bestLeftEquipmentElement, EquipmentElement bestRightEquipmentElement)
        {
            bool isCivilian = !_inventory.IsInWarSet;
            Equipment equipment = _inventory.IsInWarSet ? _characterData.GetBattleEquipment() : _characterData.GetCivilianEquipment();

            // Unequip character's current equipment item
            if (!equipment[equipmentIndex].IsEmpty)
            {
                _inventoryLogic.AddTransferCommand(TransferCommand.Transfer(
                    1,
                    InventoryLogic.InventorySide.Equipment,
                    InventoryLogic.InventorySide.PlayerInventory,
                    new ItemRosterElement(equipment[equipmentIndex], 1),
                    equipmentIndex,
                    EquipmentIndex.None,
                    _characterData.GetCharacterObject(),
                    isCivilian
                ));
            }

            bool leftGreaterThanRight = ItemIndexCalculation(bestLeftEquipmentElement, equipmentIndex) >
                                        ItemIndexCalculation(bestRightEquipmentElement, equipmentIndex);

            _inventoryLogic.AddTransferCommand(TransferCommand.Transfer(
                1,
                leftGreaterThanRight ? InventoryLogic.InventorySide.OtherInventory : InventoryLogic.InventorySide.PlayerInventory,
                InventoryLogic.InventorySide.Equipment,
                new ItemRosterElement(leftGreaterThanRight ? bestLeftEquipmentElement : bestRightEquipmentElement, 1),
                EquipmentIndex.None,
                equipmentIndex,
                _characterData.GetCharacterObject(),
                isCivilian
            ));
        }

        /// <summary>
        /// Outputs the message based on the equipment slot
        /// </summary>
        /// <param name="equipmentIndex">Equipment Index Slot</param>
        private void EquipMessage(EquipmentIndex equipmentIndex)
        {
            var name = _characterData.GetCharacterName();

            switch (equipmentIndex)
            {
                case EquipmentIndex.Weapon0:
                    InformationManager.DisplayMessage(new InformationMessage(name + " equips weapon in the first slot"));
                    break;
                case EquipmentIndex.Weapon1:
                    InformationManager.DisplayMessage(new InformationMessage(name + " equips weapon in the second slot"));
                    break;
                case EquipmentIndex.Weapon2:
                    InformationManager.DisplayMessage(new InformationMessage(name + " equips weapon in the third slot"));
                    break;
                case EquipmentIndex.Weapon3:
                    InformationManager.DisplayMessage(new InformationMessage(name + " equips weapon in the fourth slot"));
                    break;
                case EquipmentIndex.Head:
                    InformationManager.DisplayMessage(new InformationMessage(name + " equips helmet"));
                    break;
                case EquipmentIndex.Body:
                    InformationManager.DisplayMessage(new InformationMessage(name + " equips body armor"));
                    break;
                case EquipmentIndex.Leg:
                    InformationManager.DisplayMessage(new InformationMessage(name + " equips boots"));
                    break;
                case EquipmentIndex.Gloves:
                    InformationManager.DisplayMessage(new InformationMessage(name + " equips gloves"));
                    break;
                case EquipmentIndex.Cape:
                    InformationManager.DisplayMessage(new InformationMessage(name + " equips cape"));
                    break;
                case EquipmentIndex.Horse:
                    InformationManager.DisplayMessage(new InformationMessage(name + " equips horse"));
                    break;
                case EquipmentIndex.HorseHarness:
                    InformationManager.DisplayMessage(new InformationMessage(name + " equips horse harness"));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Returns item value based on the filter settings and item properties
        /// </summary>
        /// <param name="sourceItem">inventory item</param>
        /// <param name="slot">equipment index slot</param>
        /// <returns>item value</returns>
        private float ItemIndexCalculation(EquipmentElement sourceItem, EquipmentIndex slot)
        {
            // Given a item is "empty", return with big negative number
            if (sourceItem.IsEmpty)
                return -9999f;

            // Calculation for armor items
            if (sourceItem.Item.HasArmorComponent)
            {
                return BestEquipmentCalculator.CalculateArmorValue(sourceItem, _characterData.GetFilterArmor(slot));
            }

            // Calculation for weapon items
            if (sourceItem.Item.PrimaryWeapon != null)
            {
                return BestEquipmentCalculator.CalculateWeaponValue(sourceItem, _characterData.GetFilterWeapon(slot));
            }
            
            // Calculation for horse component
            if (sourceItem.Item.HasHorseComponent)
            {
                return BestEquipmentCalculator.CalculateHorseValue(sourceItem, _characterData.GetFilterMount());
            }

            return 0f;
        }

        /// <summary>
        /// Returns true if an item at this equipment index is available for upgrade
        /// </summary>
        /// <param name="index">equipment index</param>
        /// <returns>bool</returns>
        public bool IsItemUpgradable(EquipmentIndex index)
        {
            return !_bestLeftEquipment[index].IsEmpty ||
                   !_bestRightEquipment[index].IsEmpty;
        }

        /// <summary>
        /// Returns true if any equipment upgrades are available
        /// </summary>
        /// <returns>bool</returns>
        public bool IsUpgradeAvailable()
        {
            // Loops through the equipment slots to determine if the character should be able to upgrade their
            // equipment with the best items
            for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
            {
                if (!_bestLeftEquipment[equipmentIndex].IsEmpty || !_bestRightEquipment[equipmentIndex].IsEmpty)
                    return true;
            }

            return false;
        }
    }
}
