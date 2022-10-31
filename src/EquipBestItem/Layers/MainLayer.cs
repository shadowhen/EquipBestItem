using TaleWorlds.Engine.GauntletUI;

namespace EquipBestItem
{
    class MainLayer : GauntletLayer
    {
        private MainViewModel _viewModel;

        private bool _firstRefresh = false;
        private bool _leftMouseButtonWasReleased = false;

        public MainLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _viewModel = new MainViewModel();

            // Loads movie for the view model
            this.LoadMovie("EBIInventory", _viewModel);
        }

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);

            // Since we are refreshing for the first time, we need to check for first refresh
            // so we are not refreshing every single late update
            if (!_firstRefresh)
            {
                _firstRefresh = true;
                _viewModel.RefreshValues();
                return;
            }

            // Refresh if the left mouse button is pressed and the left mouse button was not released yet
            //
            // Note: Checking input keys to refresh the view model is not good idea. There is another solution
            // where the view model refreshes based on events only.
            if (TaleWorlds.InputSystem.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftMouseButton) && !_leftMouseButtonWasReleased)
            {
                _viewModel.RefreshValues();

                // This would keep the view model from refreshing multiple times while holding
                // the left mouse button down
                _leftMouseButtonWasReleased = true;
            }

            if (TaleWorlds.InputSystem.Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.LeftMouseButton) && _leftMouseButtonWasReleased)
            {
                _leftMouseButtonWasReleased = false;
            }

        }
    }
}
