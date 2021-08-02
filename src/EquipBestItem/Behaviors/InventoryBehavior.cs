using EquipBestItem.Layers;
using SandBox.GauntletUI;
using System;
using System.Collections.Generic;
using Bannerlord.ButterLib.SaveSystem.Extensions;
using Newtonsoft.Json;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;

namespace EquipBestItem
{
    class InventoryBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            Game.Current.EventManager.RegisterEvent(new Action<TutorialContextChangedEvent>(this.AddNewInventoryLayer));
        }

        public static SPInventoryVM Inventory;

        private InventoryGauntletScreen _inventoryScreen;
        private MainLayer _mainLayer;
        private FilterLayer _filterLayer;

        private void AddNewInventoryLayer(TutorialContextChangedEvent tutorialContextChangedEvent)
        {
            try
            {
                if (tutorialContextChangedEvent.NewContext == TutorialContexts.InventoryScreen)
                {
                    if (ScreenManager.TopScreen is InventoryGauntletScreen)
                    {
                        _inventoryScreen = ScreenManager.TopScreen as InventoryGauntletScreen;
                        Inventory = _inventoryScreen.GetField("_dataSource") as SPInventoryVM;

                        _mainLayer = new MainLayer(1000, "GauntletLayer");
                        _inventoryScreen.AddLayer(_mainLayer);
                        _mainLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);

                        _filterLayer = new FilterLayer(1001, "GauntletLayer");
                        _inventoryScreen.AddLayer(_filterLayer);
                        _filterLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
                    }

                    //Temporarily disabled clearing settings file for characters
                    //foreach (_characterSettings charSettings in SettingsLoader.Instance._characterSettings.ToList())
                    //{
                    //    bool flag = false;
                    //    foreach (TroopRosterElement element in EquipBestItemViewModel._inventory.TroopRoster)
                    //    {
                    //        if (charSettings.Name == element.Character.Name.ToString())
                    //        {
                    //            flag = true;
                    //            break;
                    //        }
                    //    }
                    //    if (!flag)
                    //    {
                    //        SettingsLoader.Instance._characterSettings.Remove(charSettings);
                    //    }
                    //}
                }
                else if (tutorialContextChangedEvent.NewContext == TutorialContexts.None)
                {
                    if (_inventoryScreen != null && _mainLayer != null)
                    {

                        _inventoryScreen.RemoveLayer(this._mainLayer);
                        _mainLayer = null;
                        //SettingsLoader.Instance.SaveSettings();
                        //SettingsLoader.Instance.SaveCharacterSettings();
                    }

                    if (_inventoryScreen != null && _filterLayer != null)
                    {
                        _inventoryScreen.RemoveLayer(_filterLayer);
                        _filterLayer = null;
                    }
                }
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
        }

        public override void SyncData(IDataStore dataStore)
        {
            var characterSettingsDict = new Dictionary<string, CharacterSettings>();

            try
            {
                if (dataStore.IsSaving)
                {
                    characterSettingsDict = SettingsLoader.Instance.CharacterSettingsDict;
                }

                dataStore.SyncDataAsJson("EquipBestItem.CharacterSettings", ref characterSettingsDict);

                if (dataStore.IsLoading)
                {
                    SettingsLoader.Instance.SetCharacterSettingsDict(characterSettingsDict);
                }
            }
            catch (JsonSerializationException)
            {
                InformationManager.DisplayMessage(new InformationMessage("EquipBestItem: Loading data failed. Creating new data."));
                SettingsLoader.Instance.SetCharacterSettingsDict(new Dictionary<string, CharacterSettings>());
            }
            
        }
    }
}
