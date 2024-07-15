using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class EnemyParalysis : AbstractAilment
    {
        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.EnemyParalysis;
        }
    }
}