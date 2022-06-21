using EquipBestItem.Layers;
using SandBox.GauntletUI;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem
{
    class InventoryBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            Game.Current.EventManager.RegisterEvent(new Action<TutorialContextChangedEvent>(this.AddNewInventoryLayer));
        }

        public static SPInventoryVM Inventory;
        private GauntletInventoryScreen _inventoryScreen;
        private MainLayer _mainLayer;
        private FilterLayer _filterLayer;

        private void AddNewInventoryLayer(TutorialContextChangedEvent tutorialContextChangedEvent)
        {
            try
            {
                if (tutorialContextChangedEvent.NewContext == TutorialContexts.InventoryScreen)
                {
                    if (ScreenManager.TopScreen is GauntletInventoryScreen)
                    {
                        _inventoryScreen = ScreenManager.TopScreen as GauntletInventoryScreen;
                        Inventory = _inventoryScreen.GetField("_dataSource") as SPInventoryVM;

                        _mainLayer = new MainLayer(1000, "GauntletLayer");
                        _inventoryScreen.AddLayer(_mainLayer);
                        _mainLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);

                        _filterLayer = new FilterLayer(1001, "GauntletLayer");
                        _inventoryScreen.AddLayer(_filterLayer);
                        _filterLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
                    }

                    //Temporarily disabled clearing settings file for characters
                    //foreach (CharacterSettings charSettings in SettingsLoader.Instance.CharacterSettings.ToList())
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
                    //        SettingsLoader.Instance.CharacterSettings.Remove(charSettings);
                    //    }
                    //}
                }
                else if (tutorialContextChangedEvent.NewContext == TutorialContexts.None)
                {
                    if (_inventoryScreen != null && _mainLayer != null)
                    {

                        _inventoryScreen.RemoveLayer(this._mainLayer);
                        _mainLayer = null;
                        SettingsLoader.Instance.SaveSettings();
                        SettingsLoader.Instance.SaveCharacterSettings();
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

        }
    }
}
