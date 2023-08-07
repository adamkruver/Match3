using Kruver.Mvvm.Properties;
using System;

namespace Match3.Common.Assets.Sources.Common.Mvvm.ViewBindings.Selectable
{
    public class SelectEventBindable : BindableViewProperty<bool>
    {
        private bool _isSelected;

        public event Action Changed;

        public override bool BindableProperty
        {
            get => _isSelected;
            set 
            { 
                _isSelected = value;
                Changed?.Invoke();
            }
        }
    }
}
