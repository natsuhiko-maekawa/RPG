using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.UseCase.Skill.Interface
{
    internal interface IDestroyedPartSkill : ISkillElement
    {
        public BodyPartCode GetDestroyedPart();
        public float GetLuckRate();
    }
}