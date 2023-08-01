using System.Collections.Generic;
using Match3.Domain.Attack;
using Match3.Domain.Attack.Strategies;
using Match3.Domain.Components;

namespace Match3.Domain.Units.Components
{
    public class TakeAttackLoopComponent : IComponent
    {
        private readonly TakeAttackComponent _takeAttackComponent;
        private List<IAttackStrategy> _attackStrategies = new();

        public TakeAttackLoopComponent(TakeAttackComponent takeAttackComponent)
        {
            _takeAttackComponent = takeAttackComponent;
        }

        public void Update()
        {
            for (int i = _attackStrategies.Count - 1; i >= 0; i--)
            {
                if (_attackStrategies[i].TryGetNext(out AttackInfo attackInfo) == false)
                {
                    _attackStrategies.RemoveAt(i);

                    continue;
                }

                _takeAttackComponent.Take(attackInfo);
            }
        }

        public void Add(IAttackStrategy attackStrategy) =>
            _attackStrategies.Add(attackStrategy);
    }
}