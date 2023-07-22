using UnityEngine;

namespace Kruver.Mvvm.Methods
{
    public abstract class BindableViewMethod<T> : MonoBehaviour, IBindableViewMethod<T>
    {
        protected BindableMethod<T> BindingCallback { get; private set; }

        public void BindCallback(BindableMethod<T> callback)
        {
            BindingCallback = callback;
        }

        public void Unbind()
        {
            BindingCallback = new BindableMethod<T>(null, null);
        }
    }
}