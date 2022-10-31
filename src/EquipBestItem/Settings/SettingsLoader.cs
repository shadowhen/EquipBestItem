using System.Collections.Generic;
using System.IO;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem
{
    public class SettingsLoader
    {
        private string _filePathSettings = Path.Combine(BasePath.Name, "Modules", "EquipBestItem", "ModuleData", "Settings.xml");
        private string _filePathCharacterSettings = Path.Combine(BasePath.Name, "Modules", "EquipBestItem", "ModuleData", "CharacterSettings.xml");

        private static SettingsLoader _instance = null;

        public Settings Settings { get; private set; }

        public List<CharacterSettings> CharacterSettings { get; private set; }

        private SettingsLoader()
        {
        }

        public static SettingsLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SettingsLoader();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Loads settings from a file
        /// </summary>
        public void LoadSettings()
        {
            try
            {
                this.Settings = Helper.Deserialize<Settings>(_filePathSettings);
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message + ". Trying to create a new..."));
            }
            finally
            {
                if (this.Settings == null)
                {
                    this.Settings = new Settings();
                    SaveSettings();
                }
            }
        }

        /// <summary>
        /// Saves settings to a file
        /// </summary>
        public void SaveSettings()
        {
            try
            {
                Helper.Serialize<Settings>(_filePathSettings, Settings);
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
        }

        /// <summary>
        /// Loads character settings from the file
        /// </summary>
        public void LoadCharacterSettings()
        {
            try
            {
                this.CharacterSettings = Helper.Deserialize<List<CharacterSettings>>(_filePathCharacterSettings);
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
                this.CharacterSettings = new List<CharacterSettings>();
            }

            finally
            {
                if (this.CharacterSettings == null)
                {
                    this.CharacterSettings = new List<CharacterSettings>();
                }
            }
        }

        /// <summary>
        /// Saves character settings to a file
        /// </summary>
        public void SaveCharacterSettings()
        {
            try
            {
                Helper.Serialize<List<CharacterSettings>>(_filePathCharacterSettings, this.CharacterSettings);
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
        }

        /// <summary>
        /// Returns character settings from t
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CharacterSettings GetCharacterSettingsByName(string name)
        {
            if (!InventoryBehavior.Inventory.IsInWarSet)
                name = name + "_civil";

            if (this.CharacterSettings != null)
            {
                foreach (CharacterSettings charSettings in this.CharacterSettings)
                {
                    if (charSettings.Name == name)
                    {
                        return charSettings;
                    }
                }
            }

            CharacterSettings characterSettings = new CharacterSettings(name);
            this.CharacterSettings.Add(characterSettings);

            return characterSettings;
        }

        public bool SetCharacterSettingsByName(CharacterSettings characterSettings, string name)
        {
            if (!InventoryBehavior.Inventory.IsInWarSet)
                name += "_civil";

            if (this.CharacterSettings != null)
            {
                for (int i = 0; i < CharacterSettings.Count; i++)
                {
                    if (CharacterSettings[i].Name == name)
                    {
                        CharacterSettings[i] = characterSettings;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
