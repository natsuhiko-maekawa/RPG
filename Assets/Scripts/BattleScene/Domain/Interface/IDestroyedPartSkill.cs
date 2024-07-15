using BattleScene.Domain.Code;

namespace BattleScene.Domain.Interface
{
    public interface IDestroyedPartSkill : ISkillElement
    {
        public BodyPartCode GetDestroyedPart();
        public float GetLuckRate();
    }
}