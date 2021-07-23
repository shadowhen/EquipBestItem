using System;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    public class FilterIconVM : ViewModel
    {
        private bool _isLocked;
        private bool _isSelected;

        public Action ExecuteAction { get; set; }

        private void ExecuteShowHide()
        {
            ExecuteAction?.Invoke();
        }

        [DataSourceProperty]
        public bool IsLocked
        {
            get => _isLocked;
            set
            {
                if (value == _isLocked)
                    return;
                _isLocked = value;
                OnPropertyChangedWithValue(value, nameof(IsLocked));
            }
        }

        [DataSourceProperty]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value == _isSelected)
                    return;
                _isSelected = value;
                OnPropertyChangedWithValue(value, nameof(IsSelected));
            }
        }
    }
}
