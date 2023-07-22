using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Animation
{
    public class UnitSpeedAnimationBindable : BindableViewProperty<float>
    {
        [SerializeField] private Animator _animator;

        private readonly int _speedHash = Animator.StringToHash("Speed");

        public override float BindableProperty
        {
            get => _animator.GetFloat(_speedHash);
            set => _animator.SetFloat(_speedHash, value);
        }
    }
}