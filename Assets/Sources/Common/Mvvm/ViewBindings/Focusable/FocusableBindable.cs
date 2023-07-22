using Kruver.Mvvm.Methods;

namespace Kruver.Mvvm.ViewBindings.Focusable
{
    public class FocusableBindable : BindableViewMethod<bool>
    {
        public void Focus() =>
            BindingCallback?.Invoke(true);

        public void Blur() =>
            BindingCallback?.Invoke(false);
    }
}