using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.UseCase.Skill.Interface
{
    public interface ICureSkill : ISkillElement
    {
        public float GetCureRate();
        public int GetCureAmount(CharacterId targetId);
    }
}