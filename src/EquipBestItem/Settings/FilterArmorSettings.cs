using System;
using TaleWorlds.Core;
using TaleWorlds.SaveSystem;

namespace EquipBestItem
{
    [Serializable]
    [SaveableRootClass(1)]
    public class FilterArmorSettings : IFilterSettings
    {
        [SaveableProperty(1)]
        public float HeadArmor { get; set; } = 1f;
        [SaveableProperty(2)]
        public float ArmorBodyArmor { get; set; } = 1f;
        [SaveableProperty(3)]
        public float LegArmor { get; set; } = 1f;
        [SaveableProperty(4)]
        public float ArmArmor { get; set; } = 1f;

        [SaveableProperty(5)]
        public float ManeuverBonus { get; set; } = 1f;
        [SaveableProperty(6)]
        public float SpeedBonus { get; set; } = 1f;
        [SaveableProperty(7)]
        public float ChargeBonus { get; set; } = 1f;
        [SaveableProperty(8)]
        public float ArmorWeight { get; set; } = 0;

        public FilterArmorSettings()
        {

        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">armor settings</param>
        public FilterArmorSettings(FilterArmorSettings other)
        {
            if (other == null) return;

            HeadArmor = other.HeadArmor;
            ArmorBodyArmor = other.ArmorBodyArmor;
            LegArmor = other.LegArmor;
            ArmArmor = other.ArmArmor;
            ManeuverBonus = other.ManeuverBonus;
            SpeedBonus = other.SpeedBonus;
            ChargeBonus = other.ChargeBonus;
            ArmorWeight = other.ArmorWeight;
        }

        public bool IsNotDefault()
        {
            if (this.HeadArmor != 1f) return true;
            if (this.ArmorBodyArmor != 1f) return true;
            if (this.LegArmor != 1f) return true;
            if (this.ArmArmor != 1f) return true;
            if (this.ManeuverBonus != 1f) return true;
            if (this.SpeedBonus != 1f) return true;
            if (this.ChargeBonus != 1f) return true;
            if (this.ArmorWeight != 0f) return true;
            return false;
        }

        public bool IsZero()
        {
            if (this.HeadArmor == 0f &&
                this.ArmorBodyArmor == 0f &&
                this.LegArmor == 0f &&
                this.ArmArmor == 0f &&
                this.ManeuverBonus == 0f &&
                this.SpeedBonus == 0f &&
                this.ChargeBonus == 0f &&
                this.ArmorWeight == 0f)
                return true;
            return false;
        }

        public void Clear()
        {
            HeadArmor = 1f;
            ArmorBodyArmor = 1f;
            LegArmor = 1f;
            ArmArmor = 1f;
            ManeuverBonus = 1f;
            SpeedBonus = 1f;
            ChargeBonus = 1f;
            ArmorWeight = 0;
        }

        public void ClearZero()
        {
            HeadArmor = 0;
            ArmorBodyArmor = 0;
            LegArmor = 0;
            ArmArmor = 0;
            ManeuverBonus = 0;
            SpeedBonus = 0;
            ChargeBonus = 0;
            ArmorWeight = 0;
        }
    }
}
