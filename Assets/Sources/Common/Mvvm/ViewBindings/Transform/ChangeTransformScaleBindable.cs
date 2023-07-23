using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kruver.Mvvm.Methods;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Transform
{
    public class ChangeTransformScaleBindable : BindableViewMethod<Vector3>
    {
        [SerializeField] private float _speed = 10;

        private CancellationTokenSource _cancellationTokenSource;

        public async UniTask SetScale(Vector3 targetScale)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            
            await StartScaleCoroutine(targetScale);
            
            BindingCallback?.Invoke(targetScale);
        }
        
        private IEnumerator StartScaleCoroutine(Vector3 targetScale)
        {
            while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * _speed);
                yield return null;
            }
        } 
    }
}