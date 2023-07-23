using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kruver.Mvvm.Methods;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Transform
{
    public class ChangeTransformPositionBindable : BindableViewMethod<Vector3>
    {
        [SerializeField] private float _moveSpeed = 20f;

        private CancellationTokenSource _cancellationTokenSource;
        
        public async UniTask SetPosition(Vector3 position)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            
            await StartMoveCoroutine(position).WithCancellation(_cancellationTokenSource.Token);
            
            BindingCallback?.Invoke(position);
        }
        
        private IEnumerator StartMoveCoroutine(Vector3 target)
        {
            Vector3 direction = target - transform.position;

            float zOffset = direction.x + direction.y;

            Vector3 halfDistance = transform.position + direction / 2 + Vector3.forward * zOffset / 2;

            while (Vector3.Distance(transform.position, halfDistance) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, halfDistance, Time.deltaTime * _moveSpeed);
                yield return null;
            }

            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _moveSpeed);
                yield return null;
            }
        }
    }
}