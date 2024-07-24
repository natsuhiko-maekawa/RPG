using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class Paralysis : AbstractAilment
    {
        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.Paralysis;
        }
    }
}