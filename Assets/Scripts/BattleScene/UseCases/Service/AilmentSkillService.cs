using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.Interface;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    internal class AilmentSkillService
    {
        private const float Threshold = 40.0f; // 大きいほど命中しやすくなる
        private readonly ICharacterRepository _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRandomEx _randomEx;
        private readonly ResultCreatorDomainService _resultCreator;
        private readonly TargetDomainService _target;

        public AilmentSkillService(
            ICharacterRepository characterRepository,
            OrderedItemsDomainService orderedItems,
            IRandomEx randomEx,
            ResultCreatorDomainService resultCreator,
            TargetDomainService target)
        {
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
            _randomEx = randomEx;
            _resultCreator = resultCreator;
            _target = target;
        }

        public ResultEntity Execute(SkillEntity skill)
        {
            var actorId = _orderedItems.FirstCharacterId();
            var ailmentSkill = (IAilmentSkill)skill.FirstSkillService();
            var targetIdList = _target.Get(actorId, skill.AbstractSkill.GetRange())
                .Where(x => IsTarget(x, ailmentSkill.GetLuckRate()))
                .ToImmutableList();

            var ailmentSkillResult = targetIdList.IsEmpty
                ? new AilmentSkillResultValueObject(
                    _orderedItems.FirstCharacterId(),
                    skill.SkillCode)
                : new AilmentSkillResultValueObject(
                    _orderedItems.FirstCharacterId(),
                    skill.SkillCode,
                    ailmentSkill.GetAilmentsCode(),
                    targetIdList);

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