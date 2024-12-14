using BattleScene.Domain.Codes;

namespace BattleScene.UseCases.IServices
{
    public interface IPlayerSkillService
    {
        public SkillCode[] Get();
        public SkillCode[] GetFatality();
    }
}