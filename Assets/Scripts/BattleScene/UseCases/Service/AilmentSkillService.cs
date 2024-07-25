using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    internal class AilmentSkillService
    {
        private const float Threshold = 40.0f; // 大きいほど命中しやすくなる
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        
        private readonly IRandomEx _randomEx;
        private readonly ResultCreatorDomainService _resultCreator;
        private readonly TargetDomainService _target;

        public AilmentSkillService(
            IRepository<CharacterAggregate, CharacterId> characterRepository,
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

        public ResultEntity Execute(SkillValueObject skill, AilmentValueObject ailment)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var targetIdList = _target.Get(actorId, skill.Range)
                .Where(x => IsTarget(x, ailment.LuckRate))
                .ToImmutableList();

            var ailmentSkillResult = targetIdList.IsEmpty
                ? new AilmentResultValueObject(
                    actorId: actorId,
                    skill.SkillCode)
                : new AilmentResultValueObject(
                    actorId: actorId,
                    skill.SkillCode,
                    ailment.AilmentCode,
                    targetIdList);
            
            return _resultCreator.Create(ailmentSkillResult);

            throw new NotImplementedException();
        }

        private bool IsTarget(CharacterId target, float luckRate)
        {
            _orderedItems.First().TryGetCharacterId(out var characterId);
            var actorLuck = _characterRepository.Select(characterId).Property.Luck;
            var targetLuck = _characterRepository.Select(target).Property.Luck;
            var rate = luckRate * (1.0f + (actorLuck - targetLuck) / Threshold);
            return _randomEx.Probability(rate);
        }
    }
}