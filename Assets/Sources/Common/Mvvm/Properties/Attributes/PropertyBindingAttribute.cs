using System;

namespace Kruver.Mvvm.Properties.Attributes
{
    public class PropertyBindingAttribute : Attribute
    {
        public PropertyBindingAttribute(Type componentType, string componentName = null)
        {
            ComponentType = componentType;
            ComponentName = componentName;
        }

        public Type ComponentType { get; }
        public string ComponentName { get; }
    }
}