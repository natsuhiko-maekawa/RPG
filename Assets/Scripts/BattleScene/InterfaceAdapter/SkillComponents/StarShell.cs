using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class StarShell : BaseBuff
    {
        public override BuffCode BuffCode { get; } = BuffCode.HitRate;
        public override float Rate { get; } = 2.0f;
        public override int Turn { get; } = 15;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToEndTurn;
    }
}