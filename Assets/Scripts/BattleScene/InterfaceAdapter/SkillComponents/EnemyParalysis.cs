using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class EnemyParalysis : BaseAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.EnemyParalysis;
    }
}