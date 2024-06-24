using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class FirstAidSkillElement : ResetSkillElement
    {
        public override ImmutableList<BodyPartCode> GetAidBodyPart()
        {
            return ImmutableList.Create(BodyPartCode.Arm, BodyPartCode.Leg, BodyPartCode.Stomach);
        }
    }
}