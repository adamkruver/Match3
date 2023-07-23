using System;
using System.Linq;
using Match3.PresentationInterfaces.Factories;

namespace Match3.Presentation.Sources.Presentation.Factories
{
    public class ViewTypeFactory : IViewTypeFactory
    {
        private static readonly string _postfix = "View";

        public Type Create(Type domainType)
        {
            return Create(domainType, String.Empty);
        }

        public Type Create(Type domainType, string prefix)
        {
            return GetType().Assembly.ExportedTypes.First(type => type.Name == prefix + domainType.Name + _postfix);
        }
    }
}