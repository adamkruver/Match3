using Match3.Domain.Attack.Strategies;
using Match3.Domain.Units.Components;

namespace Match3.Domain.Units.Factories
{
    public class UnitFactory
    {
        public IUnit Create(int maxHitPoint,
            ICloneableAttackStrategy defaultAttack,
            ICloneableAttackStrategy[] specials)
        {
            Unit unit = new();
            TakeAttackComponent takeAttack = new();
            TakeAttackLoopComponent takeAttackLoop = new(takeAttack);

            unit.AddComponent(new HitPointsComponent(maxHitPoint));
            unit.AddComponent(new AttackComponent(defaultAttack, specials));
            unit.AddComponent(takeAttack);
            unit.AddComponent(takeAttackLoop);
            unit.AddComponent(new TakeAttackRouterComponent(takeAttackLoop, takeAttack));

            return unit;
        }
    }
}
