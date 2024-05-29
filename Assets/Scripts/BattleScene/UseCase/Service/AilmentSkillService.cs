using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Skill.Interface;
using Utility;

namespace BattleScene.UseCase.Service
{
    internal class AilmentSkillService
    {
        private const float Threshold = 40.0f; // 大きいほど命中しやすくなる
        private readonly ICharacterRepository _characterRepository;
        private readonly TargetDomainService _target;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultCreatorDomainService _resultCreator;
        private readonly IRandomEx _randomEx;

        public ResultEntity Execute(SkillEntity skill)
        {
            var actorId = _orderedItems.FirstCharacterId();
            var ailmentSkill = (IAilmentSkill)skill.FirstSkillService();
            var targetIdList =_target.Get(actorId, skill.AbstractSkill.GetRange())
                .Where(x => IsTarget(x, ailmentSkill.GetLuckRate()))
                .ToImmutableList();

            var ailmentSkillResult = targetIdList.IsEmpty
                ? new AilmentSkillResultValueObject(
                    actorId: _orderedItems.FirstCharacterId(),
                    skillCode: skill.SkillCode)
                : new AilmentSkillResultValueObject(
                    actorId: _orderedItems.FirstCharacterId(),
                    skillCode: skill.SkillCode,
                    ailmentCode: ailmentSkill.GetAilmentsCode(),
                    targetIdList: targetIdList);

            return _resultCreator.Create(ailmentSkillResult);
        }
        
        private bool IsTarget(CharacterId target, float luckRate)
        {
            var actorLuck = _characterRepository.Select(_orderedItems.FirstCharacterId()).Property.Luck;
            var targetLuck = _characterRepository.Select(target).Property.Luck;
            var rate = luckRate * (1.0f + (actorLuck - targetLuck) / Threshold);
            return _randomEx.Probability(rate);
        }
    }
}