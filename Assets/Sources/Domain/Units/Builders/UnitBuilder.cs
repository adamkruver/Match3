using Match3.Domain.Assets.Sources.Domain.Units.Types;
using Match3.Domain.Attack.Strategies;
using Match3.Domain.Units.Factories;
using System.Collections.Generic;

namespace Match3.Domain.Units.Builders
{
    public class UnitBuilder
    {
        private static readonly int _defaultMaxHitPoints = 100;

        private readonly UnitFactory _unitFactory = new UnitFactory();
        private IUnitType _unitType;
        private List<ICloneableAttackStrategy> _specials;
        private ICloneableAttackStrategy _defaultAttack;
        private int _maxHitPoints;

        public UnitBuilder()
        {
            Clear();
        }

        public IUnit Build()
        {
            return _unitFactory.Create(_unitType, _maxHitPoints, _defaultAttack, _specials.ToArray());
        }

        public UnitBuilder Clear()
        {
            _specials = new();
            _defaultAttack = new AttackStrategy();
            _maxHitPoints = _defaultMaxHitPoints;

            return this;
        }

        public UnitBuilder SetUnitType(IUnitType unitType)
        {
            _unitType = unitType;

            return this;
        }

        public UnitBuilder SetDefaultAttack(ICloneableAttackStrategy defaultAttack)
        {
            _defaultAttack = defaultAttack;

            return this;
        }

        public UnitBuilder AddSpecial(ICloneableAttackStrategy special)
        {
            _specials.Add(special);

            return this;
        }

        public UnitBuilder SetMaxHitPoints(int maxHitPoints)
        {
            _maxHitPoints = maxHitPoints;

            return this;
        }
    }
}
