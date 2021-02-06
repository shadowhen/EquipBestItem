using TaleWorlds.Engine.GauntletUI;

namespace EquipBestItem.Layers
{
    internal class FilterLayer : GauntletLayer
    {
        private FilterViewModel _viewModel;


        public FilterLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _viewModel = new FilterViewModel();

            this.LoadMovie("FiltersLayer", this._viewModel);
        }

        bool IsAltPressed = false;
        bool _leftMouseButtonWasReleased = false;

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);

            if (TaleWorlds.InputSystem.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftMouseButton) && !_leftMouseButtonWasReleased)
            {
                _viewModel.RefreshValues();
                _leftMouseButtonWasReleased = true;
            }

            if (TaleWorlds.InputSystem.Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.LeftMouseButton) && _leftMouseButtonWasReleased)
            {
                _leftMouseButtonWasReleased = false;
            }

            // Hides the filter buttons in the inventory slots when pressing down a certain key
            if (TaleWorlds.InputSystem.Input.IsKeyDown(TaleWorlds.InputSystem.InputKey.LeftAlt) && !IsAltPressed)
            {
                IsAltPressed = true;
                if (this._viewModel.IsHiddenFilterLayer && (!this._viewModel.IsArmorSlotHidden || !this._viewModel.IsMountSlotHidden || !this._viewModel.IsWeaponSlotHidden))
                    this._viewModel.IsArmorSlotHidden = this._viewModel.IsMountSlotHidden = this._viewModel.IsWeaponSlotHidden = true;
                if (!this._viewModel.IsHiddenFilterLayer)
                {
                    this._viewModel.IsHiddenFilterLayer = true;
                }

                this._viewModel.IsLayerHidden = true;
            }
            if (TaleWorlds.InputSystem.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftAlt) && IsAltPressed)
            {
                IsAltPressed = false;
                if (this._viewModel.IsHiddenFilterLayer && (!this._viewModel.IsArmorSlotHidden || !this._viewModel.IsMountSlotHidden || !this._viewModel.IsWeaponSlotHidden)) this._viewModel.IsHiddenFilterLayer = false;
                this._viewModel.IsLayerHidden = false;
            }
        }
    }
}
