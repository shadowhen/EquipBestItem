using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    public class FilterAdjusterVM : ViewModel
    {
        public static float IncrementScale = 1f;
        
        private bool _hidden;
        private string _text;
        public Action<float> ExecuteAction;

        private void ExecutePrev()
        {
            ExecuteAction?.Invoke(-1f * IncrementScale);
#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage("ExecutePrev()"));
#endif
        }

        private void ExecuteNext()
        {
            ExecuteAction?.Invoke(1f * IncrementScale);
#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage("ExecuteNext()"));
#endif
        }

        [DataSourceProperty]
        public bool Hidden
        {
            get => _hidden;
            set
            {
                if (value == Hidden)
                    return;
                _hidden = value;
                OnPropertyChangedWithValue(value, nameof(Hidden));
            }
        }

        [DataSourceProperty]
        public string Text
        {
            get => _text;
            set
            {
                if (value == Text)
                    return;
                _text = value;
                OnPropertyChangedWithValue(value, nameof(Text));
            }
        }
    }
}
