using EquipBestItem.ViewModels;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;

namespace EquipBestItem.Layers
{
    internal class FilterLayer : GauntletLayer
    {
        private FilterViewModel _viewModel;
        private DummyVM _dummyViewModel;

        public FilterLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _viewModel = new FilterViewModel();
            _dummyViewModel = new DummyVM();

            //this.LoadMovie("FiltersLayer", this._viewModel);
            LoadMovie("FilterTuple", _dummyViewModel);
            _dummyViewModel.RefreshValues();
        }

        private bool _altPressed;
        private bool _leftMouseButtonWasReleased;

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);

            // Refresh the filter view model every time when the mouse left button is pressed
            if (TaleWorlds.InputSystem.Input.IsKeyReleased(InputKey.LeftMouseButton) && !_leftMouseButtonWasReleased)
            {
                _dummyViewModel.RefreshValues();
                _leftMouseButtonWasReleased = true;
            }
            if (TaleWorlds.InputSystem.Input.IsKeyPressed(InputKey.LeftMouseButton) && _leftMouseButtonWasReleased)
            {
                _leftMouseButtonWasReleased = false;
            }
        }
    }
}
