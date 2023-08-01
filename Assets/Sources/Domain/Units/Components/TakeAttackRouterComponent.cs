using Match3.Domain.Attack.Strategies;
using Match3.Domain.Components;

namespace Match3.Domain.Units.Components
{
    public class TakeAttackRouterComponent : IComponent
    {
        private readonly TakeAttackLoopComponent _takeAttackLoop;
        private readonly TakeAttackComponent _takeAttack;

        public TakeAttackRouterComponent(TakeAttackLoopComponent takeAttackLoop, TakeAttackComponent takeAttack)
        {
            _takeAttackLoop = takeAttackLoop;
            _takeAttack = takeAttack;
        }

        public void ApplyAttack(IAttackStrategy attackStrategy)
        {
            _takeAttack.Take(attackStrategy.Permanent);
            _takeAttackLoop.Add(attackStrategy);
        }
    }
}
