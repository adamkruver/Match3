using Kruver.Mvvm.ViewModels;

namespace Kruver.Mvvm.Views
{
    public interface IBindableView : IView
    {
        void Bind(IViewModel viewModel);
        void Unbind();
    }
}