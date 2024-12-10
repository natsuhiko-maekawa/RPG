using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.UseCase
{
    public class PlayerSelectSkillUseCase
    {
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly ITargetService _target;

        public PlayerSelectSkillUseCase(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            ITargetService target)
        {
            _skillFactory = skillFactory;
            _target = target;
        }

        public SkillValueObject GetSkill(SkillCode skillCode)
        {
            var skill = _skillFactory.Create(skillCode);
            return skill;
        }

        public IReadOnlyList<CharacterEntity> GetTarget(CharacterEntity actor, Range range)
        {
            var target = _target.Get(actor, range, true);
            return target;
        }
    }
}