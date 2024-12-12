using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class Utsusemi : BaseEnhance
    {
        public override EnhanceCode EnhanceCode { get; } = EnhanceCode.Utsusemi;
        public override int Turn { get; } = 1;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToEndTurn;
    }
}