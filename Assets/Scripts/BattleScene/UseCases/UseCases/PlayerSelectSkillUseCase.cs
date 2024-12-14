using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.UseCases.UseCases
{
    public class PlayerSelectSkillUseCase
    {
        private readonly IPlayerSkillService _playerSkill;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly ITargetService _target;

        public PlayerSelectSkillUseCase(
            IPlayerSkillService playerSkill,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            ITargetService target)
        {
            _playerSkill = playerSkill;
            _skillFactory = skillFactory;
            _target = target;
        }

        public SkillCode[] GetSkillCodeArray(BattleEventCode battleEventCode)
        {
            switch (battleEventCode)
            {
                case BattleEventCode.Skill:
                    return _playerSkill.Get();
                case BattleEventCode.FatalitySkill:
                    return _playerSkill.GetFatality();
                default:
                    throw new ArgumentOutOfRangeException(nameof(battleEventCode), battleEventCode, null);
            }
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