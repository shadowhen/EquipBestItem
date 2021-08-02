using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace EquipBestItem
{
    class CharacterData
    {
        private CharacterObject _characterObject;
        private CharacterSettings _characterSettings;

        public CharacterData(CharacterObject characterObject, CharacterSettings characterSettings)
        {
            _characterObject = characterObject;
            _characterSettings = characterSettings;

        }

        public FilterArmorSettings GetFilterArmor(EquipmentIndex index)
        {
            var tempArmorSettings = _characterSettings.FilterArmor;

            switch (index)
            {
                case EquipmentIndex.Head:
                    return tempArmorSettings[0];
                case EquipmentIndex.Cape:
                    return tempArmorSettings[1];
                case EquipmentIndex.Body:
                    return tempArmorSettings[2];
                case EquipmentIndex.Gloves:
                    return tempArmorSettings[3];
                case EquipmentIndex.Leg:
                    return tempArmorSettings[4];
                case EquipmentIndex.HorseHarness:
                    return tempArmorSettings[5];
                default:
                    return null;
            }
        }

        public FilterWeaponSettings GetFilterWeapon(EquipmentIndex index)
        {
            var tempWeaponFilters = _characterSettings.FilterWeapon;
            switch (index)
            {
                case EquipmentIndex.Weapon0:
                    return tempWeaponFilters[0];
                case EquipmentIndex.Weapon1:
                    return tempWeaponFilters[1];
                case EquipmentIndex.Weapon2:
                    return tempWeaponFilters[2];
                case EquipmentIndex.Weapon3:
                    return tempWeaponFilters[3];
                default:
                    return null;
            }
        }

        public FilterMountSettings GetFilterMount()
        {
            return _characterSettings.FilterMount;
        }

        public string GetCharacterName()
        {
            return _characterObject.GetName().ToString();
        }

        public CharacterObject GetCharacterObject()
        {
            return _characterObject;
        }

        public CharacterSettings GetCharacterSettings()
        {
            return _characterSettings;
        }

        public Equipment GetBattleEquipment()
        {
            return _characterObject.FirstBattleEquipment;
        }

        public Equipment GetCivilianEquipment()
        {
            return _characterObject.FirstCivilianEquipment;
        }
    }
}
