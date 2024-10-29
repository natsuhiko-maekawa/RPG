using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.UseCase
{
    public class PlayerSelectSkillUseCase
    {
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;

        public PlayerSelectSkillUseCase(
            IFactory<SkillValueObject, SkillCode> skillFactory)
        {
            _skillFactory = skillFactory;
        }

        public SkillValueObject GetSkill(SkillCode skillCode)
        {
            var skill = _skillFactory.Create(skillCode);
            return skill;
        }
    }
}