using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class FirstAidSkillElement : ResetSkillElement
    {
        public override ImmutableList<BodyPartCode> GetAidBodyPart()
        {
            return ImmutableList.Create(BodyPartCode.Arm, BodyPartCode.Leg, BodyPartCode.Stomach);
        }
    }
}