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

            if (TaleWorlds.InputSystem.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftMouseButton) && !_leftMouseButtonWasReleased)
            {
                _viewModel.RefreshValues();
                _leftMouseButtonWasReleased = true;
            }

            if (TaleWorlds.InputSystem.Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.LeftMouseButton) && _leftMouseButtonWasReleased)
            {
                _leftMouseButtonWasReleased = false;
            }

        }
    }
}
