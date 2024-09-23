using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class FirstAid : BaseReset
    {
        public override ImmutableList<BodyPartCode> GetAidBodyPart()
        {
            return ImmutableList.Create(BodyPartCode.Arm, BodyPartCode.Leg, BodyPartCode.Stomach);
        }
    }
}