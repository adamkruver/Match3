using Match3.Domain.Assets.Sources.Domain.Units.Types;
using Match3.Domain.Attack.Strategies.Continuous;
using System;
using System.Collections.Generic;

namespace Match3.Domain.Units.Builders
{
    public class UnitDirector
    {
        private readonly UnitBuilder _builder = new();
        private readonly Dictionary<Type, Func<IUnitType, IUnit>> _builds;

        public UnitDirector()
        {
            _builds = new Dictionary<Type, Func<IUnitType, IUnit>>()
            {
                [typeof(Ogre)] = BuildOgre,
                [typeof(Beholder)] = BuildBeholder,
            };
        }

        public IUnit Build<T>(T type) where T : IUnitType
        {
            return _builds[type.GetType()].Invoke(type);
        }

        private IUnit BuildOgre(IUnitType type) =>
            _builder.Clear()
                .SetUnitType(type)
                .SetMaxHitPoints(1000)
                .AddSpecial(new ThreeTimesAttackStrategy())
                .Build();

        private IUnit BuildBeholder(IUnitType type) =>
            _builder.Clear()
                .SetUnitType(type)
                .SetMaxHitPoints(700)
                .AddSpecial(new ThreeTimesAttackStrategy())
                .Build();
    }
}
