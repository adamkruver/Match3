using Match3.Domain.Components;
using System;

namespace Match3.Domain.Assets.Sources.Domain.Units.Components
{
    public class SelectableComponent : IComponent
    {
        public event Action Changed;

        public bool IsSelected { get; private set; }

        public void Select()
        {
            if (IsSelected)
                return;

            IsSelected = true;
            Changed?.Invoke();
        }

        public void Unselect()
        {
            if (IsSelected == false)
                return;

            IsSelected = false;
            Changed?.Invoke();
        }
    }
}
