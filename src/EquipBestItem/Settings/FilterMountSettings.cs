using System;
using TaleWorlds.SaveSystem;

namespace EquipBestItem
{
    [Serializable]
    [SaveableRootClass(2)]
    public class FilterMountSettings : IFilterSettings
    {
        [SaveableProperty(1)]
        public float ChargeDamage { get; set; } = 1f;
        [SaveableProperty(2)]
        public float HitPoints { get; set; } = 1f;
        [SaveableProperty(3)]
        public float Maneuver { get; set; } = 1f;
        [SaveableProperty(4)]
        public float Speed { get; set; } = 1f;

        public FilterMountSettings()
        {

        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">mount settings</param>
        public FilterMountSettings(FilterMountSettings other)
        {
            if (other == null) return;

            ChargeDamage = other.ChargeDamage;
            HitPoints = other.HitPoints;
            Maneuver = other.Maneuver;
            Speed = other.Speed;
        }

        public bool IsNotDefault()
        {
            if (this.ChargeDamage != 1f) return true;
            if (this.HitPoints != 1f) return true;
            if (this.Maneuver != 1f) return true;
            if (this.Speed != 1f) return true;
            return false;
        }

        public bool IsZero()
        {
            if (this.ChargeDamage == 0f &&
                this.HitPoints == 0f &&
                this.Maneuver == 0f &&
                this.Speed == 0f)
                return true;
            return false;
        }

        public void Clear()
        {
            ChargeDamage = 1f;
            HitPoints = 1f;
            Maneuver = 1f;
            Speed = 1f;
        }

        public void ClearZero()
        {
            ChargeDamage = 0;
            HitPoints = 0;
            Maneuver = 0;
            Speed = 0;
        }
    }
}
