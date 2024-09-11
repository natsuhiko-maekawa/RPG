using BattleScene.Domain.Code;

namespace BattleScene.UseCases.IService
{
    public interface IEnemySkillSelectorService
    {
        public SkillCode Select();
    }
}