using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class EnemyParalysis : AbstractAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.EnemyParalysis;

        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.EnemyParalysis;
        }
    }
}