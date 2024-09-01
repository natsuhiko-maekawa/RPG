using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    public class AilmentSkillService
    {
        private const float Threshold = 40.0f; // 大きいほど命中しやすくなる
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRepository<ResultEntity, ResultId> _resultRepository;
        private readonly IRepository<TurnEntity, TurnId> _turnRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        
        private readonly IRandomEx _randomEx;
        private readonly TargetDomainService _target;

        public AilmentSkillService(
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            OrderedItemsDomainService orderedItems,
            IRandomEx randomEx,
            TargetDomainService target)
        {
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
            _randomEx = randomEx;
            _target = target;
        }
        
        public void Execute(SkillValueObject skill, AilmentValueObject ailment)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var targetIdList = _target.Get(actorId, skill.Range)
                .Where(x => IsTarget(x, ailment.LuckRate))
                .ToImmutableList();

            var resultId = new ResultId();
            var currentTurn = _turnRepository.Select()
                .OrderByDescending(x => x.Turn)
                .Select(x => x.Turn)
                .First();
            var nextSequence = _resultRepository.Select()
                .OrderByDescending(x => x.Sequence)
                .Select(x => x.Sequence)
                .First();
            var result = new ResultEntity(
                id: resultId,
                turn: currentTurn,
                sequence: nextSequence,
                targetIdList: targetIdList,
                ailmentCode: ailment.AilmentCode);
            _resultRepository.Update(result);
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