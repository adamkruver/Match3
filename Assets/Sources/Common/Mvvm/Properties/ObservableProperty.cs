using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kruver.Mvvm.Properties
{
    public class ObservableProperty<T> : IDisposable
    {
        private readonly object _target;
        private readonly PropertyInfo _propertyInfo;
        private readonly List<Action> _actions = new List<Action>();

        public ObservableProperty(object target, PropertyInfo propertyInfo)
        {
            _target = target;
            _propertyInfo = propertyInfo;
        }

        public event Action Changed
        {
            add => _actions.Add(value);
            remove => _actions.Remove(value);
        }

        public void Set(T value)
        {
            _propertyInfo.SetValue(_target, value);
            InvokeChanged();
        }

        public T Value => (T)_propertyInfo.GetValue(_target);

        public void Dispose()
        {
            _actions.Clear();
        }

        private void InvokeChanged()
        {
            foreach (Action action in _actions)
            {
                action.Invoke();
            }
        }
    }
}