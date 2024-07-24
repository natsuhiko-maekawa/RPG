using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class FirstAid : AbstractReset
    {
        public override ImmutableList<BodyPartCode> GetAidBodyPart()
        {
            return ImmutableList.Create(BodyPartCode.Arm, BodyPartCode.Leg, BodyPartCode.Stomach);
        }
    }
}