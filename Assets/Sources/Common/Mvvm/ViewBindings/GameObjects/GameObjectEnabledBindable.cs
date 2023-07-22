using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.GameObjects
{
    public class GameObjectEnabledBindable : BindableViewProperty<bool>
    {
        [SerializeField] private UnityEngine.GameObject _gameObject;
        [SerializeField] private bool _enabledOnStart;

        private void Awake() => 
            BindableProperty = _enabledOnStart;

        public override bool BindableProperty
        {
            get => _gameObject.activeSelf;
            set => _gameObject.SetActive(value);
        }
    }
}