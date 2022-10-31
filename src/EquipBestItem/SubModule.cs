using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Library;

namespace EquipBestItem
{
    public class SubModule : MBSubModuleBase
    {
        public override void OnMissionBehaviorInitialize(Mission mission)
        {
            base.OnMissionBehaviorInitialize(mission);
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            try
            {
                base.OnGameStart(game, gameStarterObject);

                // Load mod settings and character settings
                SettingsLoader.Instance.LoadSettings();
                SettingsLoader.Instance.LoadCharacterSettings();

                // Add behavior to campaign game starter
                AddBehaviours(gameStarterObject as CampaignGameStarter);
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage("SubModule " + e.Message));
            }
        }

        private void AddBehaviours(CampaignGameStarter gameStarterObject)
        {
            try
            {
                if (gameStarterObject != null)
                {
                    gameStarterObject.AddBehavior(new InventoryBehavior());
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
