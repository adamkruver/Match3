using System.Collections;
using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.CanvasGroups
{
    public class CanvasGroupFadeInBindable : BindableViewProperty<bool>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime;

        private readonly float _maxAlpha = 1f;
        private readonly float _minAlpha = 0f;
        
        private Coroutine _fadeJob;

        public override bool BindableProperty
        {
            get => false;
            set
            {
                if (value)
                    FadeIn();
                else
                    Disable();
            }
        }

        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        private void Disable()
        {
            StopJob();
            _canvasGroup.alpha = _minAlpha;
        }

        private void FadeIn()
        {
            StartJob();
        }

        private void StartJob()
        {
            StopJob();
            _fadeJob = StartCoroutine(FadeInCoroutine(_fadeTime));
        }

        private void StopJob()
        {
            if(_fadeJob != null)
                StopCoroutine(_fadeJob);
        }

        private IEnumerator FadeInCoroutine(float fadeTime)
        {
            while (_canvasGroup.alpha < _maxAlpha)
            {
                _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, _maxAlpha, Time.deltaTime * fadeTime);
                
                yield return null;
            }

            _canvasGroup.alpha = _maxAlpha;
        }
    }
}