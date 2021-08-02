using TaleWorlds.SaveSystem;

namespace EquipBestItem
{
    [SaveableRootClass(3)]
    public class FilterWeaponSettings : IFilterSettings
    {
        [SaveableProperty(1)]
        public float MaxDataValue { get; set; } = 1f;
        [SaveableProperty(2)]
        public float ThrustSpeed { get; set; } = 1f;
        [SaveableProperty(3)]
        public float SwingSpeed { get; set; } = 1f;
        [SaveableProperty(4)]
        public float MissileSpeed { get; set; } = 1f;
        [SaveableProperty(5)]
        public float WeaponLength { get; set; } = 1f;
        [SaveableProperty(6)]
        public float ThrustDamage { get; set; } = 1f;
        [SaveableProperty(7)]
        public float SwingDamage { get; set; } = 1f;
        [SaveableProperty(8)]
        public float Accuracy { get; set; } = 1f;
        [SaveableProperty(9)]
        public float Handling { get; set; } = 1f;
        [SaveableProperty(10)]
        public float WeaponWeight { get; set; } = 0f;
        [SaveableProperty(11)]
        public float WeaponBodyArmor { get; set; } = 1f;

        //public DamageTypes SwingDamageType { get; set; } = 0;
        //public DamageTypes ThrustDamageType { get; set; } = 0;
        //public int MissileDamage { get; set; }
        //public float WeaponBalance { get; set; }


        //public WeaponClass? WeaponClass { get; set; }
        //public string ItemUsage { get; set; }


        //public WeaponFlags? WeaponFlags { get; set; }

        public FilterWeaponSettings()
        {

        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">weapon settings</param>
        public FilterWeaponSettings(FilterWeaponSettings other)
        {
            if (other == null) return;

            MaxDataValue = other.MaxDataValue;
            ThrustSpeed = other.ThrustSpeed;
            SwingSpeed = other.SwingSpeed;
            MissileSpeed = other.MissileSpeed;
            WeaponLength = other.WeaponLength;
            ThrustDamage = other.ThrustDamage;
            SwingDamage = other.SwingDamage;
            Accuracy = other.Accuracy;
            Handling = other.Handling;
            WeaponWeight = other.WeaponWeight;
            WeaponBodyArmor = other.WeaponBodyArmor;
        }

        public bool IsNotDefault()
        {
            if (this.MaxDataValue != 1f) return true;
            if (this.ThrustSpeed != 1f) return true;
            if (this.SwingSpeed != 1f) return true;
            if (this.MissileSpeed != 1f) return true;
            if (this.WeaponLength != 1f) return true;
            if (this.ThrustDamage != 1f) return true;
            if (this.SwingDamage != 1f) return true;
            if (this.Accuracy != 1f) return true;
            if (this.Handling != 1f) return true;
            if (this.WeaponWeight != 0f) return true;
            if (this.WeaponBodyArmor != 1f) return true;
            return false;
        }

        public bool IsZero()
        {
            if (this.MaxDataValue == 0f &&
                this.ThrustSpeed == 0f &&
                this.SwingSpeed == 0f &&
                this.MissileSpeed == 0f &&
                this.WeaponLength == 0f &&
                this.ThrustDamage == 0f &&
                this.SwingDamage == 0f &&
                this.Accuracy == 0f &&
                this.Handling == 0f &&
                this.WeaponWeight == 0f &&
                this.WeaponBodyArmor == 0f)
                return true;
            return false;
        }

        public void Clear()
        {
            Accuracy = 1f;
            WeaponBodyArmor = 1f;
            Handling = 1f;
            MaxDataValue = 1f;
            MissileSpeed = 1f;
            SwingDamage = 1f;
            SwingSpeed = 1f;
            ThrustDamage = 1f;
            ThrustSpeed = 1f;
            WeaponLength = 1f;
            WeaponWeight = 0;
        }

        public void ClearZero()
        {
            Accuracy = 0;
            WeaponBodyArmor = 0;
            Handling = 0;
            MaxDataValue = 0;
            MissileSpeed = 0;
            SwingDamage = 0;
            SwingSpeed = 0;
            ThrustDamage = 0;
            ThrustSpeed = 0;
            WeaponLength = 0;
            WeaponWeight = 0;
        }
    }
}
