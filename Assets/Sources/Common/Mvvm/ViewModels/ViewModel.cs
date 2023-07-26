using System;

namespace Kruver.Mvvm.ViewModels
{
    public abstract class ViewModel<T> : IViewModel
    {
        private bool _isEnabled;

        protected ViewModel(T model)
        {
            Model = model;
        }

        protected T Model { get; }

        public event Action Destroyed;

        public void Enable()
        {
            if (_isEnabled)
                return;
            
            _isEnabled = true;
            OnEnable();
        }
        
        public void Disable()
        {
            if (_isEnabled == false)
                return;
            
            _isEnabled = false;
            OnDisable();
        }

        public void Destroy()
        {
            Destroyed?.Invoke();
        }

        protected abstract void OnEnable();
        protected abstract void OnDisable();
    }
}