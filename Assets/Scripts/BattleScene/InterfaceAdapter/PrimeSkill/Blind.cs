using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class Blind : BaseAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.Blind;
    }
}