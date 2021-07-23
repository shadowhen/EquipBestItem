using EquipBestItem.ViewModels;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;

namespace EquipBestItem.Layers
{
    internal class FilterLayer : GauntletLayer
    {
        private FilterViewModel _filterViewModel;

        public FilterLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _filterViewModel = new FilterViewModel();

            LoadMovie("FiltersLayer", _filterViewModel);
            _filterViewModel.RefreshValues();
        }

        private bool _altPressed;
        private bool _leftMouseButtonWasReleased;

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);

            // Refresh the filter view model every time when the mouse left button is pressed
            if (TaleWorlds.InputSystem.Input.IsKeyReleased(InputKey.LeftMouseButton) && !_leftMouseButtonWasReleased)
            {
                _filterViewModel.RefreshValues();
                _leftMouseButtonWasReleased = true;
            }
            if (TaleWorlds.InputSystem.Input.IsKeyPressed(InputKey.LeftMouseButton) && _leftMouseButtonWasReleased)
            {
                _leftMouseButtonWasReleased = false;
            }

            if (TaleWorlds.InputSystem.Input.IsKeyPressed(InputKey.LeftAlt) && !_altPressed)
            {
                _altPressed = true;
                _filterViewModel.IconsVM.IconsHidden = true;
            }

            if (TaleWorlds.InputSystem.Input.IsKeyReleased(InputKey.LeftAlt) && _altPressed)
            {
                _altPressed = false;
                _filterViewModel.IconsVM.IconsHidden = false;
            }
        }
    }
}
