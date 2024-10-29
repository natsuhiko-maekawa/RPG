using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface ITechnicalPointService
    {
        public int Get();
        public void Reduce(SkillValueObject skill);
    }
}