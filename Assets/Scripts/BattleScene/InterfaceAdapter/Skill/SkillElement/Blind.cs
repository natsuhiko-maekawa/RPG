using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class Blind : AbstractAilment
    {
        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.Blind;
        }
    }
}