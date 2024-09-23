using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class AbsoluteConfusion : BaseAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.Confusion;
        public override float LuckRate { get; } = 1.0f;


    }
}