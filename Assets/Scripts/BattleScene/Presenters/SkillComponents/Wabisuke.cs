using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class Wabisuke : BaseBuff
    {
        public override BuffCode BuffCode { get; } = BuffCode.Speed;
        public override float Rate { get; } = 0.5f;
        public override int Turn { get; } = 10;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToEndTurn;
    }
}