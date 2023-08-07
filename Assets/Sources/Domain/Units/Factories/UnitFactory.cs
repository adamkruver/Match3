using Match3.Domain.Assets.Sources.Domain.Units.Components;
using Match3.Domain.Assets.Sources.Domain.Units.Types;
using Match3.Domain.Attack.Strategies;
using Match3.Domain.Units.Components;

namespace Match3.Domain.Units.Factories
{
    public class UnitFactory
    {
        public IUnit Create(
            IUnitType unitType,
            int maxHitPoint,
            ICloneableAttackStrategy defaultAttack,
            ICloneableAttackStrategy[] specials)
        {
            Unit unit = new Unit(unitType);
            TakeAttackComponent takeAttack = new();
            TakeAttackLoopComponent takeAttackLoop = new(takeAttack);

            unit.AddComponent(new HitPointsComponent(maxHitPoint));
            unit.AddComponent(new AttackComponent(defaultAttack, specials));
            unit.AddComponent(takeAttack);
            unit.AddComponent(takeAttackLoop);
            unit.AddComponent(new TakeAttackRouterComponent(takeAttackLoop, takeAttack));
            unit.AddComponent(new SelectableComponent());
            unit.AddComponent(new BarPositionComponent());

            return unit;
        }
    }
}
