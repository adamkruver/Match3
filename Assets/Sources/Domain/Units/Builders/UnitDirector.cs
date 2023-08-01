using Match3.Domain.Attack.Strategies.Continuous;

namespace Match3.Domain.Units.Builders
{
    public class UnitDirector
    {
        private UnitBuilder _builder = new();

        public IUnit BuildOgre() =>
            _builder
                .Clear()
                .SetMaxHitPoints(1000)
                .AddSpecial(new ThreeTimesAttackStrategy())
                .Build();
    }
}