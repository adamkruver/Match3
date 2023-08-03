using Match3.Domain.Components;

namespace Match3.Domain.Assets.Sources.Domain.Components
{
    public interface IComposite
    {
        T Get<T>() where T : IComponent;
    }
}
