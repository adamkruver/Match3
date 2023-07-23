using System;

namespace Match3.PresentationInterfaces.Factories
{
    public interface IViewTypeFactory
    {
        Type Create(Type domainType);
        Type Create(Type domainType, string prefix);
    }
}