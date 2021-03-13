using System;
using TaleWorlds.Core;

namespace EquipBestItem
{
    [Serializable]
    public class FilterArmorSettings : IFilterSettings
    {
        public float HeadArmor { get; set; } = 1f;
        public float ArmorBodyArmor { get; set; } = 1f;
        public float LegArmor { get; set; } = 1f;
        public float ArmArmor { get; set; } = 1f;

        public float ManeuverBonus { get; set; } = 1f;
        public float SpeedBonus { get; set; } = 1f;
        public float ChargeBonus { get; set; } = 1f;
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
