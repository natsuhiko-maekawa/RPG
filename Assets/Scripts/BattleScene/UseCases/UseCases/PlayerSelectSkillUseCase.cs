using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.UseCases
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