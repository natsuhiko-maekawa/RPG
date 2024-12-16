using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class DestroyStomach : BaseDestroy
    {
        public override BodyPartCode BodyPartCode { get; } = BodyPartCode.Stomach;
    }
}