using Kruver.Mvvm.Views;
using Match3.Domain;

namespace Match3.PresentationInterfaces.Sources.PresentationInterfaces.Builders
{
    public interface ICellViewBuilder
    {
        public IBindableView Build(ICellType cellType);
    }
}