using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.UseCases.Skill.Interface
{
    internal interface IDestroyedPartSkill : ISkillElement
    {
        public BodyPartCode GetDestroyedPart();
        public float GetLuckRate();
    }
}