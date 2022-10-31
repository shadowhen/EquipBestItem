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
            // Register event for tutorial context changed event
            // since the inventory screen is a tutorial context
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
                    // Add layers if the top screen is a inventory screen
                    if (ScreenManager.TopScreen is GauntletInventoryScreen)
                    {
                        _inventoryScreen = ScreenManager.TopScreen as GauntletInventoryScreen;
                        
                        // Get inventory from the inventory screen using reflection
                        Inventory = _inventoryScreen.GetField("_dataSource") as SPInventoryVM;

                        // Setup and add main layer to the inventory screen
                        _mainLayer = new MainLayer(1000, "GauntletLayer");
                        _inventoryScreen.AddLayer(_mainLayer);
                        _mainLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);

                        // Setup and add filter layer to the inventory screen
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
                    // Removes main layer and saves settings and character settings to the file
                    if (_inventoryScreen != null && _mainLayer != null)
                    {
                        _inventoryScreen.RemoveLayer(this._mainLayer);
                        _mainLayer = null;
                        SettingsLoader.Instance.SaveSettings();
                        SettingsLoader.Instance.SaveCharacterSettings();
                    }

                    // Removes the filter layer
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
            // TODO: Add saving and loading filter settings for individual saves
            // 
            // Note: Saving and loading filter settings eliminates the need for
            // a global file shared by all saves. However, adding data to the save
            // requires good understanding how characters are added and removed during
            // gameplay.
            //
            // On another note, players would have save their games in order to save
            // the filter settings.
        }
    }
}
