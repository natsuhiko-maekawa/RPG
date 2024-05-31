using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using Utility;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.DomainService
{
    public class TargetDomainService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IHitPointRepository _hitPointRepository;
        private readonly IRandomEx _randomEx;

        public TargetDomainService(
            ICharacterRepository characterRepository,
            IHitPointRepository hitPointRepository,
            IRandomEx randomEx)
        {
            _characterRepository = characterRepository;
            _hitPointRepository = hitPointRepository;
            _randomEx = randomEx;
        }

        public ImmutableList<CharacterId> Get(CharacterId characterId, Range range)
        {
            var targetList = range switch
            {
                Range.Random => GetRandom(characterId),
                Range.Oneself =>
                    _hitPointRepository.Select(characterId).IsSurvive()
                        ? ImmutableList<CharacterId>.Empty
                        : ImmutableList.Create(characterId),
                // TODO プレイヤーが選択したターゲットを返す処理を書くこと
                _ => throw new NotImplementedException()
            };

            return targetList;
        }

        private ImmutableList<CharacterId> GetRandom(CharacterId characterId)
        {
            var targetList = _characterRepository.Select()
                .Select(x => x.CharacterId)
                .Where(x => !Equals(x, characterId) && _hitPointRepository.Select(x).IsSurvive())
                .ToList();
            if (targetList.Count == 0) return ImmutableList<CharacterId>.Empty;
            return ImmutableList.Create(_randomEx.Choice(targetList));
        }
    }
}