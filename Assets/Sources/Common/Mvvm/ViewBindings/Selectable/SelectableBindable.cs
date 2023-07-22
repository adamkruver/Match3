using Kruver.Mvvm.Methods;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Selectable
{
    [RequireComponent(typeof(Collider))]
    public class SelectableBindable : BindableViewMethod<bool>
    {
        public void Select() => 
            BindingCallback?.Invoke(true);

        public void Unselect() => 
            BindingCallback?.Invoke(false);
    }
}