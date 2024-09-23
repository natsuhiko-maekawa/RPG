using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class MusterStrength : BaseBuff
    {
        public override BuffCode BuffCode { get; } = BuffCode.Attack;
        public override float Rate { get; } = 2.0f;
        public override int Turn { get; } = 1;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToNextAction;
    }
}