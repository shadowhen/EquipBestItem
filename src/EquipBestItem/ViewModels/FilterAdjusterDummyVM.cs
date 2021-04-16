using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    public class FilterAdjusterDummyVM : ViewModel
    {
        private enum IncrementState
        {
            One, Five, Ten
        }
        private static IncrementState _incrementState = IncrementState.One;

        private bool _hidden;
        private string _text;
        private string _title;

        private float _value;
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                Text = $"{value}";
            }
        }

        public Action<float> ExecuteAction;
        public Action ExecuteFilterAction;

        public FilterAdjusterDummyVM()
        {
            Value = 1f;
        }

        private static float GetIncrementScaleInput()
        {
            bool leftCtrlPressed = Input.IsKeyDown(InputKey.LeftControl);
            bool leftShiftPressed = Input.IsKeyDown(InputKey.LeftShift);

            switch (_incrementState)
            {
                case IncrementState.One:
                    if (leftCtrlPressed && !leftShiftPressed)
                        _incrementState = IncrementState.Ten;
                    if (!leftCtrlPressed && leftShiftPressed)
                        _incrementState = IncrementState.Five;
                    break;
                case IncrementState.Five:
                    if (!leftCtrlPressed && !leftShiftPressed)
                        _incrementState = IncrementState.One;
                    if (!leftCtrlPressed && leftShiftPressed)
                        _incrementState = IncrementState.Five;
                    break;
                case IncrementState.Ten:
                    if (!leftCtrlPressed && !leftShiftPressed)
                        _incrementState = IncrementState.One;
                    if (leftCtrlPressed && !leftShiftPressed)
                        _incrementState = IncrementState.Ten;
                    break;
            }

            switch (_incrementState)
            {
                case IncrementState.One:
                    return 1f;
                case IncrementState.Five:
                    return 5f;
                case IncrementState.Ten:
                    return 10f;
                default:
                    return 1f;
            }
        }

        private void ExecutePrev()
        {
            Value += -1f * GetIncrementScaleInput();
            ExecuteAction?.Invoke(Value);
            ExecuteFilterAction?.Invoke();
#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage("ExecutePrev()"));
#endif
        }

        private void ExecuteNext()
        {
            Value += 1f * GetIncrementScaleInput();
            ExecuteAction?.Invoke(Value);
            ExecuteFilterAction?.Invoke();
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

        [DataSourceProperty]
        public string Title
        {
            get => _title;
            set
            {
                if (value == Title)
                    return;
                _title = value;
                OnPropertyChangedWithValue(value, nameof(Title));
            }
        }
    }
}
