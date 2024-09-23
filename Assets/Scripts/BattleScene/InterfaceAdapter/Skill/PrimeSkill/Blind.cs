using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class Blind : AbstractAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.Blind;
    }
}