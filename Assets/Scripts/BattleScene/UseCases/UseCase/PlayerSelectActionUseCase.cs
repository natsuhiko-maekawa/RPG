using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.UseCase
{
    public class PlayerSelectActionUseCase
    {
        private readonly ITargetService _target;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;

        public PlayerSelectActionUseCase(
            ITargetService target,
            IFactory<SkillValueObject, SkillCode> skillFactory)
        {
            _target = target;
            _skillFactory = skillFactory;
        }

        public IReadOnlyList<CharacterEntity> GetOneself(CharacterEntity player)
        {
            var targetIdList = _target.Get(player, Range.Oneself);
            return targetIdList;
        }

        public SkillValueObject? GetSkill(BattleEventCode battleEventCode)
        {
            var skillCode = battleEventCode switch
            {
                BattleEventCode.Attack => SkillCode.Attack,
                BattleEventCode.Defence => SkillCode.Defence,
                _ => SkillCode.NoSkill
            };

            if (skillCode == SkillCode.NoSkill) return null;

            var skill = _skillFactory.Create(skillCode);
            return skill;
        }
    }
}