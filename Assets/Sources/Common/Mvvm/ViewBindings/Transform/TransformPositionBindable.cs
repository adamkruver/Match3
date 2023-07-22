using System.Collections;
using Kruver.Mvvm.Properties;
using UnityEngine;

namespace Kruver.Mvvm.ViewBindings.Transform
{
    public class TransformPositionBindable : BindableViewProperty<Vector3>
    {
        [SerializeField] private float _moveSpeed = 2f;

        private Coroutine _moveJob;

        public override Vector3 BindableProperty
        {
            get => transform.position;
            set => StartMove(value);
        }

        private void StartMove(Vector3 target)
        {
            StopMove();

            _moveJob = StartCoroutine(StartMoveCoroutine(target));
        }

        private void StopMove()
        {
            if (_moveJob == null)
                return;

            StopCoroutine(_moveJob);
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

            StopMove();
        }
    }
}