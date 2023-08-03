using Match3.Domain.Assets.Sources.Domain.Components;
using System.Collections.Generic;
using System.Linq;

namespace Match3.Domain.Components
{
    public class Composite : IComposite
    {
        private readonly List<IComponent> _components = new();

        public void AddComponent(IComponent component)
        {
            if (_components.Contains(component))
                return;

            _components.Add(component);
        }

        public T Get<T>() where T : IComponent
        {
            return (T)_components.First(component => component is T);
        }
    }
}
