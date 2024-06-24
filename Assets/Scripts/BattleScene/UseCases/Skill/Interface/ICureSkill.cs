using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.UseCases.Skill.Interface
{
    public interface ICureSkill : ISkillElement
    {
        public float GetCureRate();
        public int GetCureAmount(CharacterId targetId);
    }
}