using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem
{
    class BestEquipmentCalculator
    {
        /// <summary>
        /// Returns value for armor using its properties and filter settings
        /// </summary>
        /// <param name="sourceItem">Armor item</param>
        /// <param name="slot">Armor equipment slot</param>
        /// <returns>calculated value for armor</returns>
        public float CalculateArmorValue(EquipmentElement sourceItem, FilterArmorSettings filterArmor)
        {
            // Get armor component from the item
            ArmorComponent armorComponentItem = sourceItem.Item.ArmorComponent;

            // Add up all of the filter values
            float sum =
                Math.Abs(filterArmor.HeadArmor) +
                Math.Abs(filterArmor.ArmArmor) +
                Math.Abs(filterArmor.ArmorBodyArmor) +
                Math.Abs(filterArmor.ArmorWeight) +
                Math.Abs(filterArmor.LegArmor);

            // Get item modifer from the item
            ItemModifier mod = sourceItem.ItemModifier;

            // Get values from the item and the armor component
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

                // Make sure that the values are not in the negative
                // TODO: Use Math's max function instead of conditional assignments
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

        public float CalculateWeaponsValue(EquipmentElement sourceItem, FilterWeaponSettings filterWeapon)
        {
            float highestValue = 0;
            foreach (WeaponComponentData weaponItem in sourceItem.Item.Weapons)
            {
                float temp = CalculateWeaponValue(weaponItem, sourceItem, filterWeapon);
                if (temp > highestValue) highestValue = temp;
            }
            return highestValue;
        }

        /// <summary>
        /// Returns value for weapon using its properties and filter settings
        /// </summary>
        /// <param name="sourceItem">Weapon item</param>
        /// <param name="slot">Weapon equipment slot</param>
        /// <returns>calculated value for weapon</returns>
        public float CalculateWeaponValue(WeaponComponentData primaryWeaponItem, EquipmentElement sourceItem, FilterWeaponSettings filterWeapon)
        {
            FilterWeaponSettings weights = filterWeapon;

            // Fetch direct values from the weapon item
            int accuracy = primaryWeaponItem.Accuracy;
            int bodyArmor = primaryWeaponItem.BodyArmor;
            int handling = primaryWeaponItem.Handling;
            int maxDataValue = primaryWeaponItem.MaxDataValue;
            int missileSpeed = primaryWeaponItem.MissileSpeed;
            int swingDamage = primaryWeaponItem.SwingDamage;
            int swingSpeed = primaryWeaponItem.SwingSpeed;
            int thrustDamage = primaryWeaponItem.ThrustDamage;
            int thrustSpeed = primaryWeaponItem.ThrustSpeed;
            int weaponLength = primaryWeaponItem.WeaponLength;
            float weaponWeight = sourceItem.Weight;

            // Modification calculation
            ItemModifier mod = sourceItem.ItemModifier;
            if (mod != null)
            {
                if (bodyArmor > 0f)
                    bodyArmor = mod.ModifyArmor(bodyArmor);
                if (missileSpeed > 0f)
                    missileSpeed = mod.ModifyMissileSpeed(missileSpeed);
                if (swingDamage > 0f)
                    swingDamage = mod.ModifyDamage(swingDamage);
                if (swingSpeed > 0f)
                    swingSpeed = mod.ModifySpeed(swingSpeed);
                if (thrustDamage > 0f)
                    thrustDamage = mod.ModifyDamage(thrustDamage);
                if (thrustDamage > 0f)
                    thrustSpeed = mod.ModifySpeed(thrustSpeed);
                if (maxDataValue > 0f)
                    maxDataValue = mod.ModifyHitPoints((short)maxDataValue);
                //WeaponWeight *= mod.WeightMultiplier;

                // Ensure that the values always clamped between 0 to infinity
                // TODO: Might be better to use Math max function instead inline if conditions
                bodyArmor = bodyArmor < 0 ? 0 : bodyArmor;
                missileSpeed = missileSpeed < 0 ? 0 : missileSpeed;
                swingDamage = swingDamage < 0 ? 0 : swingDamage;
                swingSpeed = swingSpeed < 0 ? 0 : swingSpeed;
                thrustDamage = thrustDamage < 0 ? 0 : thrustDamage;
                thrustSpeed = thrustSpeed < 0 ? 0 : thrustSpeed;
                maxDataValue = maxDataValue < 0 ? 0 : maxDataValue;
            }

            // Weighted value
            float weightAccuracy = accuracy * weights.Accuracy;
            float weightBodyArmor = bodyArmor * weights.WeaponBodyArmor;
            float weightHandling = handling * weights.Handling;
            float weightMaxDataValue = maxDataValue * weights.MaxDataValue;
            float weightMissileSpeed = missileSpeed * weights.MissileSpeed;
            float weightSwingDamage = swingDamage * weights.SwingDamage;
            float weightSwingSpeed = swingSpeed * weights.SwingSpeed;
            float weightThrustDamage = thrustDamage * weights.ThrustDamage; // It is also missile damage for bows and crossbows
            float weightThrustSpeed = thrustSpeed * weights.ThrustSpeed;
            float weightWeaponLength = weaponLength * weights.WeaponLength;
            float weightWeaponWeight = weaponWeight * weights.WeaponWeight;

            float sum = 0.0f;
            float value = 0.0f;

            // Determine how the value is calculated based on the weapon class of the primary weapon
            switch (primaryWeaponItem.WeaponClass)
            {
                case WeaponClass.SmallShield:
                case WeaponClass.LargeShield:
                    // Weight, HitPoints
                    sum = Math.Abs(filterWeapon.MaxDataValue) + Math.Abs(filterWeapon.WeaponWeight);
                    value = weightMaxDataValue + weightWeaponWeight;

#if DEBUG
                    InformationManager.DisplayMessage(new InformationMessage("Shield"));
#endif
                    break;
                case WeaponClass.Crossbow:
                case WeaponClass.Bow:
                    // Weight, Thrust Damage, Accuracy, Missile Speed
                    sum = Math.Abs(filterWeapon.WeaponWeight) +
                          Math.Abs(filterWeapon.ThrustDamage) +
                          Math.Abs(filterWeapon.Accuracy) +
                          Math.Abs(filterWeapon.MissileSpeed);
                    value = weightWeaponWeight + weightThrustDamage + weightAccuracy + weightMissileSpeed;

#if DEBUG
                    InformationManager.DisplayMessage(new InformationMessage("Shield"));
#endif
                    break;
                case WeaponClass.Javelin:
                case WeaponClass.ThrowingAxe:
                case WeaponClass.ThrowingKnife:
                    // Weight, Length, Thrust Damage, Missile Speed, Accuracy, MaxDataValue (Stack Amount)
                    sum = Math.Abs(filterWeapon.WeaponWeight) +
                          Math.Abs(filterWeapon.WeaponLength) +
                          Math.Abs(filterWeapon.ThrustDamage) +
                          Math.Abs(filterWeapon.MissileSpeed) +
                          Math.Abs(filterWeapon.Accuracy) +
                          Math.Abs(filterWeapon.MaxDataValue);
                    value = weightWeaponWeight +
                            weightWeaponLength +
                            weightThrustDamage +
                            weightMissileSpeed +
                            weightAccuracy +
                            weightMaxDataValue;

#if DEBUG
                    InformationManager.DisplayMessage(new InformationMessage("Javelin/Throwing Axe/Throwing Knife"));
#endif
                    break;
                case WeaponClass.OneHandedSword:
                case WeaponClass.TwoHandedSword:
                case WeaponClass.LowGripPolearm:
                case WeaponClass.OneHandedPolearm:
                case WeaponClass.TwoHandedPolearm:
                    // Weight, Length, Handling, Swing Speed, Swing Damage, Thrust Speed, Thrust Damage
                    sum = Math.Abs(filterWeapon.WeaponWeight) +
                          Math.Abs(filterWeapon.WeaponLength) +
                          Math.Abs(filterWeapon.Handling) +
                          Math.Abs(filterWeapon.SwingSpeed) +
                          Math.Abs(filterWeapon.SwingDamage) +
                          Math.Abs(filterWeapon.ThrustSpeed) +
                          Math.Abs(filterWeapon.ThrustDamage);
                    value = weightWeaponWeight +
                            weightWeaponLength +
                            weightHandling +
                            weightSwingSpeed +
                            weightSwingDamage +
                            weightThrustSpeed +
                            weightThrustDamage;

#if DEBUG
                    if (primaryWeaponItem.WeaponClass >= WeaponClass.OneHandedPolearm &&
                        primaryWeaponItem.WeaponClass <= WeaponClass.LowGripPolearm)
                    {
                        InformationManager.DisplayMessage(new InformationMessage("Low Grip/One Handed/Two Handed Polearm"));
                    }
                    else
                    {
                        InformationManager.DisplayMessage(new InformationMessage("One Handed/Two Handed Sword"));
                    }
#endif
                    break;
                case WeaponClass.OneHandedAxe:
                case WeaponClass.TwoHandedAxe:
                case WeaponClass.Mace:
                case WeaponClass.TwoHandedMace:
                    // Weight, Swing Speed, Swing Damage, Length, Handling
                    sum = Math.Abs(filterWeapon.WeaponWeight) +
                          Math.Abs(filterWeapon.SwingSpeed) +
                          Math.Abs(filterWeapon.SwingDamage) +
                          Math.Abs(filterWeapon.WeaponLength) +
                          Math.Abs(filterWeapon.Handling);
                    value = weightWeaponWeight +
                            weightSwingSpeed +
                            weightSwingDamage +
                            weightWeaponLength +
                            weightHandling;
#if DEBUG
                    if (primaryWeaponItem.WeaponClass >= WeaponClass.OneHandedAxe &&
                        primaryWeaponItem.WeaponClass <= WeaponClass.TwoHandedAxe)
                    {
                        InformationManager.DisplayMessage(new InformationMessage("One Handed/Two Handed Axe"));
                    }
                    else
                    {
                        InformationManager.DisplayMessage(new InformationMessage("One Handed/Two Handed Mace"));
                    }
#endif
                    break;

                case WeaponClass.Arrow:
                case WeaponClass.Bolt:
                    sum = Math.Abs(filterWeapon.WeaponWeight) +
                          Math.Abs(filterWeapon.Accuracy) +
                          Math.Abs(filterWeapon.ThrustDamage) +
                          Math.Abs(filterWeapon.MaxDataValue);
                    value = weightWeaponWeight +
                            weightAccuracy +
                            weightThrustDamage +
                            weightMaxDataValue;
#if DEBUG
                    InformationManager.DisplayMessage(new InformationMessage("Arrows/Bolts"));
#endif
                    break;

                default:
                    sum = 
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
                    value = weightAccuracy +
                            weightBodyArmor +
                            weightHandling +
                            weightMaxDataValue +
                            weightMissileSpeed +
                            weightSwingDamage +
                            weightSwingSpeed +
                            weightThrustDamage +
                            weightThrustSpeed +
                            weightWeaponLength +
                            weightWeaponWeight;
#if DEBUG
                    InformationManager.DisplayMessage(new InformationMessage("Other"));
#endif
                    break;
            }

            float finalValue = value / sum;

#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: Acc {1}, BA {2}, HL {3}, HP {4}, MS {5}, SD {6}, SS {7}, TD {8}, TS {9}, WL {10}, W {11}",
                            sourceItem.Item.Name, accuracy, bodyArmor, handling, maxDataValue, missileSpeed, swingDamage, swingSpeed, thrustDamage, thrustSpeed, weaponLength, weaponWeight)));

            InformationManager.DisplayMessage(new InformationMessage("Total score: " + finalValue));
#endif

            return finalValue;
        }

        /// <summary>
        /// Returns value for horse using its properties and filter settings
        /// </summary>
        /// <param name="sourceItem">Horse item</param>
        /// <param name="filterMount">Filter settings</param>
        /// <returns>calculated value for horse</returns>
        public float CalculateHorseValue(EquipmentElement sourceItem, FilterMountSettings filterMount)
        {
            // Get horse component from the item
            HorseComponent horseComponentItem = sourceItem.Item.HorseComponent;

            // Add together filter values in absolute sum
            float sum =
                Math.Abs(filterMount.ChargeDamage) +
                Math.Abs(filterMount.HitPoints) +
                Math.Abs(filterMount.Maneuver) +
                Math.Abs(filterMount.Speed);

            // Get values from the horse component
            int ChargeDamage = horseComponentItem.ChargeDamage,
                HitPoints = horseComponentItem.HitPoints,
                Maneuver = horseComponentItem.Maneuver,
                Speed = horseComponentItem.Speed;

            // Get item modifier from the item and modifiy the values
            ItemModifier mod = sourceItem.ItemModifier;
            if (mod != null)
            {
                ChargeDamage = mod.ModifyMountCharge(ChargeDamage);
                Maneuver = mod.ModifyMountManeuver(Maneuver);
                Speed = mod.ModifyMountSpeed(Speed);
            }

            var weights = filterMount;
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
    }
}
