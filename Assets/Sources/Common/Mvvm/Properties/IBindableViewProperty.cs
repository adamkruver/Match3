namespace Kruver.Mvvm.Properties
{
    public interface IBindableViewProperty
    {
        object OnBind();
    }

    public interface IBindableViewProperty<T> : IBindableViewProperty
    {
        ObservableProperty<T> GetBinding();

        object IBindableViewProperty.OnBind() => GetBinding();
    }
}