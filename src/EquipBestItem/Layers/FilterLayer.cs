using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;

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

        private bool _altPressed;
        private bool _leftMouseButtonWasReleased;

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);

            // Refresh the filter view model every time when the mouse left button is pressed
            if (TaleWorlds.InputSystem.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftMouseButton) && !_leftMouseButtonWasReleased)
            {
                _viewModel.RefreshValues();
                _leftMouseButtonWasReleased = true;
            }
            if (TaleWorlds.InputSystem.Input.IsKeyPressed(InputKey.LeftMouseButton) && _leftMouseButtonWasReleased)
            {
                _leftMouseButtonWasReleased = false;
            }

            // Hides the filter buttons in the inventory slots when pressing down a certain key
            if (TaleWorlds.InputSystem.Input.IsKeyDown(InputKey.LeftAlt) && !_altPressed)
            {
                _altPressed = true;
                if (this._viewModel.IsHiddenFilterLayer && (!this._viewModel.IsArmorSlotHidden || !this._viewModel.IsMountSlotHidden || !this._viewModel.IsWeaponSlotHidden))
                    this._viewModel.IsArmorSlotHidden = this._viewModel.IsMountSlotHidden = this._viewModel.IsWeaponSlotHidden = true;
                if (!this._viewModel.IsHiddenFilterLayer)
                {
                    this._viewModel.IsHiddenFilterLayer = true;
                }

                this._viewModel.IsLayerHidden = true;
            }
            if (TaleWorlds.InputSystem.Input.IsKeyReleased(InputKey.LeftAlt) && _altPressed)
            {
                _altPressed = false;
                if (this._viewModel.IsHiddenFilterLayer && (!this._viewModel.IsArmorSlotHidden || !this._viewModel.IsMountSlotHidden || !this._viewModel.IsWeaponSlotHidden)) this._viewModel.IsHiddenFilterLayer = false;
                this._viewModel.IsLayerHidden = false;
            }

            // Increments the value depending the following keys
            _viewModel.IncrementBy5 = TaleWorlds.InputSystem.Input.IsKeyDown(InputKey.LeftShift);
            _viewModel.IncrementBy10 = TaleWorlds.InputSystem.Input.IsKeyDown(InputKey.LeftControl);
        }
    }
}
