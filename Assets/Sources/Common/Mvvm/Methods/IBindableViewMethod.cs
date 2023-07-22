namespace Kruver.Mvvm.Methods
{
    public interface IBindableViewMethod
    {
        void OnBind(object callback);
        void Unbind();
    }

    public interface IBindableViewMethod<T> : IBindableViewMethod
    {
        void BindCallback(BindableMethod<T> callback);

        void IBindableViewMethod.OnBind(object callback) => BindCallback((BindableMethod<T>)callback);
    }
}