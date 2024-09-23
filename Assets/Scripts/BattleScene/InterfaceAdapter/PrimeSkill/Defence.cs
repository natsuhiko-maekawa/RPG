using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class Defence : BaseBuff
    {
        public override BuffCode BuffCode { get; } = BuffCode.DefenceSkill;
        public override float Rate { get; } = 2.0f;
        public override int Turn { get; } = 1;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToNextAction;
    }
}