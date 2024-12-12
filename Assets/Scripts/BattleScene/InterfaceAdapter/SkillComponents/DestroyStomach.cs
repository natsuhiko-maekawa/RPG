using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class DestroyStomach : BaseDestroy
    {
        public override BodyPartCode BodyPartCode { get; } = BodyPartCode.Stomach;
    }
}