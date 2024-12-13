using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class AfterImage : BaseBuff
    {
        public override BuffCode BuffCode { get; } = BuffCode.Speed;
        public override float Rate { get; } = 2.0f;
        public override int Turn { get; } = 5;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToEndTurn;
    }
}