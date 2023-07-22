using System.Reflection;

namespace Kruver.Mvvm.Properties.Factories
{
    public class ObservablePropertyFactory
    {
        private static readonly BindingFlags _bindingFlags =
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        public ObservableProperty<T> Create<T>(object target, string propertyName)
        {
            return new ObservableProperty<T>(target, target.GetType().GetProperty(propertyName, _bindingFlags));
        }
    }
}