using Match3.Domain.Attack;
using Match3.Domain.Attack.Strategies;
using Match3.Domain.Components;

namespace Match3.Domain.Units.Components
{
    public class AttackComponent : IComponent
    {
        private readonly ICloneableAttackStrategy _defaultAttack;
        private readonly ICloneableAttackStrategy[] _specials;

        public AttackComponent(ICloneableAttackStrategy defaultAttack, ICloneableAttackStrategy[] specials)
        {
            _defaultAttack = defaultAttack;
            _specials = specials;
        }

       public IAttackStrategy DefaultAttack => _defaultAttack.Clone(GetAttackInfo());

        public IAttackStrategy GetSpecialAttack(int index)
        {
            return _specials[index].Clone(GetAttackInfo());
        }

        private AttackInfo GetAttackInfo()
        {
            throw new System.Exception();
        }
    }
}