using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace EquipBestItem
{
    class CharacterData
    {
        private CharacterObject _characterObject;
        private CharacterSettings _characterSettings;

        public CharacterData(CharacterObject characterObject)
        {
            _characterObject = characterObject;
            _characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(characterObject.ToString());

        }

        public CharacterData(CharacterObject characterObject, CharacterSettings characterSettings)
        {
            _characterObject = characterObject;
            _characterSettings = characterSettings;

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
