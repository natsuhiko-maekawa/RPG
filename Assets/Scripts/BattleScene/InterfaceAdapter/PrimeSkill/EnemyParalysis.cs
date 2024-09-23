using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class EnemyParalysis : BaseAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.EnemyParalysis;
    }
}