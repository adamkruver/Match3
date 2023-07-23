using Kruver.Mvvm.Methods;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.ParticleSystems
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemAfterPlayBindable : BindableViewMethod<bool>
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            ParticleSystem.MainModule mainModule = _particleSystem.main;
            mainModule.stopAction = ParticleSystemStopAction.Callback;
        }

        public void OnParticleSystemStopped() => 
            BindingCallback?.Invoke(false);
    }
}