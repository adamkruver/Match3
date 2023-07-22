using Kruver.Mvvm.Methods;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Click
{
    public class ClickBindable : BindableViewMethod<Vector3>
    {
        private void OnMouseDown()
        {
            BindingCallback.Invoke(transform.position);
        }
    }
}