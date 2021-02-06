using System;
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
        }

        /// <summary>
        /// Get the best item from inventory side
        /// </summary>
        /// <param name="itemListVM">List of items on the inventory side</param>
        /// <param name="equipmentElement">Current item</param>
        /// <param name="slot">Type of item</param>
        /// <param name="isCivilian">Civilian Type</param>
        /// <returns></returns>
        public EquipmentElement GetBetterItemFromSide(MBBindingList<SPItemVM> itemListVM, EquipmentElement equipmentElement, EquipmentIndex slot, bool isCivilian)
        {
            EquipmentElement bestEquipmentElement;

            CharacterObject character = _characterData.GetCharacterObject();

            // Loops through the inventory list to find the best equipment item
            foreach (SPItemVM item in itemListVM)
            {
                if (IsCamel(item) || IsCamelHarness(item))
                    continue;
                
                if (isCivilian)
                {
                    if (slot < EquipmentIndex.NonWeaponItemBeginSlot &&
                        item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon != null &&
                        item.IsEquipableItem &&
                        item.IsCivilianItem &&
                        CharacterHelper.CanUseItem(character, item.ItemRosterElement.EquipmentElement)
                        )
                    {
                        if (equipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.WeaponClass &&
                            GetItemUsage(item) == equipmentElement.Item.PrimaryWeapon.ItemUsage)
                        {
                            bestEquipmentElement = GetBestEquipmentElement(slot, item.ItemRosterElement.EquipmentElement, equipmentElement, bestEquipmentElement);
                        }
                    }
                    else if (item.ItemType == slot && 
                        item.IsEquipableItem && 
                        item.IsCivilianItem && 
                        CharacterHelper.CanUseItem(character, item.ItemRosterElement.EquipmentElement))
                    {
                        bestEquipmentElement = GetBestEquipmentElement(slot, item.ItemRosterElement.EquipmentElement, equipmentElement, bestEquipmentElement);
                    }
                }
                else
                {
                    if (slot < EquipmentIndex.NonWeaponItemBeginSlot && 
                        item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon != null && 
                        item.IsEquipableItem &&
                        CharacterHelper.CanUseItem(character, item.ItemRosterElement.EquipmentElement))
                    {
                        if (equipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.WeaponClass &&
                            GetItemUsage(item) == equipmentElement.Item.PrimaryWeapon.ItemUsage)
                        {
                            bestEquipmentElement = GetBestEquipmentElement(slot, item.ItemRosterElement.EquipmentElement, equipmentElement, bestEquipmentElement);
                        }
                    }
                    else if (item.ItemType == slot && 
                        item.IsEquipableItem &&
                        CharacterHelper.CanUseItem(character, item.ItemRosterElement.EquipmentElement))
                    {
                        bestEquipmentElement = GetBestEquipmentElement(slot, item.ItemRosterElement.EquipmentElement, equipmentElement, bestEquipmentElement);
                    }
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

            if (inventoryItemValue > currentItemValue && inventoryItemValue != 0f)
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
            if (item != null && item.StringId.StartsWith("camel_sadd"))
                return true;
            return false;
        }

        /// <summary>
        /// Returns item usage in string
        /// </summary>
        /// <param name="item">inventory item</param>
        /// <returns></returns>
        public static string GetItemUsage(SPItemVM item)
        {
            if (item == null || item.ItemRosterElement.IsEmpty || item.ItemRosterElement.EquipmentElement.IsEmpty || item.ItemRosterElement.EquipmentElement.Item.WeaponComponent == null)
                return "";
            string value = item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.ItemUsage;
            return value;
        }

        /// <summary>
        /// Equips the character with the best items.
        ///
        /// With a given character, a character data is created using the character and character settings loaded from
        /// settings loader using the character's name. Character's equipment is obtained either from the
        /// battle equipment or civilian equipment depending if the inventory is in war set or civilian set.
        /// After getting the character data and equipment, the character equipment can be upgraded
        /// with the best equipment.
        /// 
        /// </summary>
        /// <param name="character">hero</param>
        public void EquipCharacter(CharacterObject character)
        {
            _characterData = new CharacterData(character, SettingsLoader.Instance.GetCharacterSettingsByName(character.Name.ToString()));
            Equipment characterEquipment = _inventory.IsInWarSet ? character.FirstBattleEquipment : character.FirstCivilianEquipment;
            EquipCharacterEquipment(characterEquipment, !_inventory.IsInWarSet);
        }

        /// <summary>
        /// Equips the character's equipment with the best equipment
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

                // Unequip character's current equipment item
                if (!equipment[equipmentIndex].IsEmpty && (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null))
                {
                    TransferCommand transferCommand = TransferCommand.Transfer(
                        1,
                        InventoryLogic.InventorySide.Equipment,
                        InventoryLogic.InventorySide.PlayerInventory,
                        new ItemRosterElement(equipment[equipmentIndex], 1),
                        equipmentIndex,
                        EquipmentIndex.None,
                        _characterData.GetCharacterObject(),
                        isCivilian
                    );
                    _inventoryLogic.AddTransferCommand(transferCommand);
                }

                if (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null)
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
                _inventory.GetMethod("ExecuteRemoveZeroCounts");
            }
            _inventory.GetMethod("RefreshInformationValues");
        }

        /// <summary>
        /// Outputs the message based on the equipment slot
        /// </summary>
        /// <param name="equipmentIndex">Equipment Index Slot</param>
        private void EquipMessage(EquipmentIndex equipmentIndex)
        {
            var name = _characterData.GetCharacterObject().Name.ToString();

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
        public float ItemIndexCalculation(EquipmentElement sourceItem, EquipmentIndex slot)
        {
            
            // Given a item is "empty", return with big negative number
            if (sourceItem.IsEmpty)
                return -9999f;

            float value = 0f;
            CharacterSettings characterSettings = _characterData.GetCharacterSettings();

            
            // Calculation for armor items
            if (sourceItem.Item.HasArmorComponent)
            {
                value = CalculateArmorValue(sourceItem, slot);

                return value;
            }

            // Calculation for weapon items
            if (sourceItem.Item.PrimaryWeapon != null)
            {
                value = CalculateWeaponValue(sourceItem, slot);

                return value;
            }

            // Calculation for horse component
            if (sourceItem.Item.HasHorseComponent)
            {
                value = CalculateHorseValue(sourceItem);

                return value;
            }

            return value;
        }

        /// <summary>
        /// Calculate the value for armor using its properties and filter settings
        /// </summary>
        /// <param name="sourceItem">Armor item</param>
        /// <param name="slot">Armor equipment slot</param>
        /// <returns>calculated value for armor</returns>
        private float CalculateArmorValue(EquipmentElement sourceItem, EquipmentIndex slot)
        {
            ArmorComponent armorComponentItem = sourceItem.Item.ArmorComponent;
            FilterArmorSettings filterArmor = _characterData.GetCharacterSettings().FilterArmor[GetEquipmentSlot(slot)];

            float sum =
                Math.Abs(filterArmor.HeadArmor) +
                Math.Abs(filterArmor.ArmArmor) +
                Math.Abs(filterArmor.ArmorBodyArmor) +
                Math.Abs(filterArmor.ArmorWeight) +
                Math.Abs(filterArmor.LegArmor);

            ItemModifier mod = sourceItem.ItemModifier;

            int HeadArmor = armorComponentItem.HeadArmor,
                BodyArmor = armorComponentItem.BodyArmor,
                LegArmor = armorComponentItem.LegArmor,
                ArmArmor = armorComponentItem.ArmArmor;
            float Weight = sourceItem.Weight;

            if (mod != null)
            {
                // Since armor values are positive numbers, we need to check 
                // if the given values have positive number before we apply
                // any modifiers to it.
                if (HeadArmor > 0f)
                    HeadArmor = mod.ModifyArmor(HeadArmor);
                if (BodyArmor > 0f)
                    BodyArmor = mod.ModifyArmor(BodyArmor);
                if (LegArmor > 0f)
                    LegArmor = mod.ModifyArmor(LegArmor);
                if (ArmArmor > 0f)
                    ArmArmor = mod.ModifyArmor(ArmArmor);
                //Weight *= mod.WeightMultiplier;

                HeadArmor = HeadArmor < 0 ? 0 : HeadArmor;
                BodyArmor = BodyArmor < 0 ? 0 : BodyArmor;
                LegArmor = LegArmor < 0 ? 0 : LegArmor;
                ArmArmor = ArmArmor < 0 ? 0 : ArmArmor;

            }

            float value = (
                HeadArmor * filterArmor.HeadArmor +
                BodyArmor * filterArmor.ArmorBodyArmor +
                LegArmor * filterArmor.LegArmor +
                ArmArmor * filterArmor.ArmArmor +
                Weight * filterArmor.ArmorWeight
            ) / sum;

#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: HA {1}, BA {2}, LA {3}, AA {4}, W {5}",
                            sourceItem.Item.Name, HeadArmor, BodyArmor, LegArmor, ArmArmor, Weight)));

            InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
#endif
            return value;
        }

        /// <summary>
        /// Calculate the value for weapon using its properties and filter settings
        /// </summary>
        /// <param name="sourceItem">Weapon item</param>
        /// <param name="slot">Weapon equipment slot</param>
        /// <returns>calculated value for weapon</returns>
        private float CalculateWeaponValue(EquipmentElement sourceItem, EquipmentIndex slot)
        {
            WeaponComponentData primaryWeaponItem = sourceItem.Item.PrimaryWeapon;
            FilterWeaponSettings filterWeapon = _characterData.GetCharacterSettings().FilterWeapon[GetEquipmentSlot(slot)];
            float sum =
                Math.Abs(filterWeapon.Accuracy) +
                Math.Abs(filterWeapon.WeaponBodyArmor) +
                Math.Abs(filterWeapon.Handling) +
                Math.Abs(filterWeapon.MaxDataValue) +
                Math.Abs(filterWeapon.MissileSpeed) +
                Math.Abs(filterWeapon.SwingDamage) +
                Math.Abs(filterWeapon.SwingSpeed) +
                Math.Abs(filterWeapon.ThrustDamage) +
                Math.Abs(filterWeapon.ThrustSpeed) +
                Math.Abs(filterWeapon.WeaponLength) +
                Math.Abs(filterWeapon.WeaponWeight);

            int Accuracy = primaryWeaponItem.Accuracy,
                BodyArmor = primaryWeaponItem.BodyArmor,
                Handling = primaryWeaponItem.Handling,
                MaxDataValue = primaryWeaponItem.MaxDataValue,
                MissileSpeed = primaryWeaponItem.MissileSpeed,
                SwingDamage = primaryWeaponItem.SwingDamage,
                SwingSpeed = primaryWeaponItem.SwingSpeed,
                ThrustDamage = primaryWeaponItem.ThrustDamage,
                ThrustSpeed = primaryWeaponItem.ThrustSpeed,
                WeaponLength = primaryWeaponItem.WeaponLength;
            float WeaponWeight = sourceItem.Weight;

            ItemModifier mod = sourceItem.ItemModifier;
            if (mod != null)
            {
                if (BodyArmor > 0f)
                    BodyArmor = mod.ModifyArmor(BodyArmor);
                if (MissileSpeed > 0f)
                    MissileSpeed = mod.ModifyMissileSpeed(MissileSpeed);
                if (SwingDamage > 0f)
                    SwingDamage = mod.ModifyDamage(SwingDamage);
                if (SwingSpeed > 0f)
                    SwingSpeed = mod.ModifySpeed(SwingSpeed);
                if (ThrustDamage > 0f)
                    ThrustDamage = mod.ModifyDamage(ThrustDamage);
                if (ThrustSpeed > 0f)
                    ThrustSpeed = mod.ModifySpeed(ThrustSpeed);
                if (MaxDataValue > 0f)
                    MaxDataValue = mod.ModifyHitPoints((short)MaxDataValue);
                //WeaponWeight *= mod.WeightMultiplier;

                BodyArmor = BodyArmor < 0 ? 0 : BodyArmor;
                MissileSpeed = MissileSpeed < 0 ? 0 : MissileSpeed;
                SwingDamage = SwingDamage < 0 ? 0 : SwingDamage;
                SwingSpeed = SwingSpeed < 0 ? 0 : SwingSpeed;
                ThrustDamage = ThrustDamage < 0 ? 0 : ThrustDamage;
                ThrustSpeed = ThrustSpeed < 0 ? 0 : ThrustSpeed;
                MaxDataValue = MaxDataValue < 0 ? 0 : MaxDataValue;
            }

            var weights = _characterData.GetCharacterSettings().FilterWeapon[GetEquipmentSlot(slot)];
            float value = (
                Accuracy * weights.Accuracy +
                BodyArmor * weights.WeaponBodyArmor +
                Handling * weights.Handling +
                MaxDataValue * weights.MaxDataValue +
                MissileSpeed * weights.MissileSpeed +
                SwingDamage * weights.SwingDamage +
                SwingSpeed * weights.SwingSpeed +
                ThrustDamage * weights.ThrustDamage +
                ThrustSpeed * weights.ThrustSpeed +
                WeaponLength * weights.WeaponLength +
                WeaponWeight * weights.WeaponWeight
            ) / sum;


#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: Acc {1}, BA {2}, HL {3}, HP {4}, MS {5}, SD {6}, SS {7}, TD {8}, TS {9}, WL {10}, W {11}",
                            sourceItem.Item.Name, Accuracy, BodyArmor, Handling, MaxDataValue, MissileSpeed, SwingDamage, SwingSpeed, ThrustDamage, ThrustSpeed, WeaponLength, WeaponWeight)));

            InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
#endif

            return value;
        }

        /// <summary>
        /// Calculate the value for horse using its properties and filter settings
        /// </summary>
        /// <param name="sourceItem">Horse item</param>
        /// <returns>calculated value for horse</returns>
        private float CalculateHorseValue(EquipmentElement sourceItem)
        {
            HorseComponent horseComponentItem = sourceItem.Item.HorseComponent;
            FilterMountSettings filterMount = _characterData.GetCharacterSettings().FilterMount;

            float sum =
                Math.Abs(filterMount.ChargeDamage) +
                Math.Abs(filterMount.HitPoints) +
                Math.Abs(filterMount.Maneuver) +
                Math.Abs(filterMount.Speed);

            int ChargeDamage = horseComponentItem.ChargeDamage,
                HitPoints = horseComponentItem.HitPoints,
                Maneuver = horseComponentItem.Maneuver,
                Speed = horseComponentItem.Speed;

            ItemModifier mod = sourceItem.ItemModifier;
            if (mod != null)
            {
                ChargeDamage = mod.ModifyMountCharge(ChargeDamage);
                Maneuver = mod.ModifyMountManeuver(Maneuver);
                Speed = mod.ModifyMountSpeed(Speed);
            }

            var weights = _characterData.GetCharacterSettings().FilterMount;
            float value = (
                ChargeDamage * weights.ChargeDamage +
                HitPoints * weights.HitPoints +
                Maneuver * weights.Maneuver +
                Speed * weights.Speed
            ) / sum;

#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: CD {1}, HP {2}, MR {3}, SD {4}",
                            sourceItem.Item.Name, ChargeDamage, HitPoints, Maneuver, Speed)));

            InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
#endif

            return value;
        }

        /// <summary>
        /// Returns integer based on the equipment slot
        /// </summary>
        /// <param name="slot">equipment slot</param>
        /// <returns>int</returns>
        public static int GetEquipmentSlot(EquipmentIndex slot)
        {
            switch (slot)
            {
                case EquipmentIndex.Weapon0:
                    return 0;
                case EquipmentIndex.Weapon1:
                    return 1;
                case EquipmentIndex.Weapon2:
                    return 2;
                case EquipmentIndex.Weapon3:
                    return 3;
                case EquipmentIndex.Head:
                    return 0;
                case EquipmentIndex.Cape:
                    return 1;
                case EquipmentIndex.Body:
                    return 2;
                case EquipmentIndex.Gloves:
                    return 3;
                case EquipmentIndex.Leg:
                    return 4;
                case EquipmentIndex.Horse:
                    return 0;
                case EquipmentIndex.HorseHarness:
                    return 5;
                default:
                    return 0;
            }
        }

        public void SetCharacterData(CharacterData characterData)
        {
            _characterData = characterData;
        }

        public CharacterData GetCharacterData()
        {
            return _characterData;
        }
    }
}
