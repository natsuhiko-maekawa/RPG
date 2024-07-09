using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.UseCases.Skill.Interface
{
    public interface IBuffSkill : ISkillElement
    {
        public BuffCode GetBuff();
        public float GetBuffRate();
        public int GetTurn();
        public LifetimeCode GetLifetimeCode();
    }
}