using System;
using Kruver.Mvvm.ViewModels;
using UnityEngine;

namespace Kruver.Mvvm.Views
{
    public class BindableView : MonoBehaviour, IBindableView
    {
        protected Binder Binder;
        
        private IViewModel _viewModel;
        private bool _isEnabled;

        public Action AfterUnbindCallback { get; set; }

        private void Awake()
        {
            gameObject.SetActive(false);
            _isEnabled = false;
            
            OnAwake();
        }

        private void OnEnable()
        {
            OnEnabled();
        }

        private void OnDisable()
        {
            OnDisabled();
        }

        public void Bind(IViewModel viewModel)
        {
            _viewModel = viewModel;
            
            Binder.Bind(this, viewModel);

            viewModel.Enable();
        }

        public void Unbind()
        {
            if (_viewModel != null)
                Binder.Unbind(this, _viewModel);

            Hide();
            
            AfterUnbindCallback?.Invoke();
        }

        public void Show()
        {
            if (_isEnabled)
                return;

            if (gameObject == null)
                return;

            gameObject.SetActive(true);
            _isEnabled = true;
        }

        public void Hide()
        {
            if (_isEnabled == false)
                return;

            if (gameObject == null)
                return;

            gameObject.SetActive(false);
            _isEnabled = false;
        }

        public void Construct(Binder binder)
        {
            Binder = binder;
            OnConstruct();
        }

        protected virtual void OnConstruct()
        {
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnEnabled()
        {
        }

        protected virtual void OnDisabled()
        {
        }
    }
}