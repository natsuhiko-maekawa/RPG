using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class AbsoluteConfusion : AbstractAilment
    {
        public override float GetLuckRate()
        {
            return 1.0f;
        }

        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.Confusion;
        }
    }
}