using System;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCase.Skill;
using VContainer;
using static BattleScene.Domain.Code.SkillCode;

namespace BattleScene.UseCase.Service
{
    public class SkillCreatorService
    {
        private readonly IObjectResolver _container;

        public SkillEntity Create(CharacterId characterId, SkillCode skillCode)
        {
            return new SkillEntity(
                characterId,
                skillCode,
                CreateSkill(skillCode));
        }

        private AbstractSkill CreateSkill(SkillCode skillCode)
        {
            return skillCode switch
            {
                Attack => _container.Resolve<AttackSkill>(),
                _ => throw new ArgumentOutOfRangeException(nameof(skillCode), skillCode, null)
            };
        }
    }
}