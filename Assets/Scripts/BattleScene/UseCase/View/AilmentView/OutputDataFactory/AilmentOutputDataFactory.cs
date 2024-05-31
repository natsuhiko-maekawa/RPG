using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.AilmentView.OutputData;
using BattleScene.UseCase.View.PlayerImageView.OutputData;

namespace BattleScene.UseCase.View.AilmentView.OutputDataFactory
{
    internal class AilmentOutputDataFactory
    {
        private readonly AilmentDomainService _ailment;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemyRepository _enemyRepository;
        private readonly ISlipDamageRepository _slipDamageRepository;
        private readonly ToAilmentNumberService _toAilmentNumber;

        public ImmutableList<AilmentOutputData> Create()
        {
            var ailmentOutputDataList = _characterRepository.Select()
                .Select(x => x.IsPlayer()
                    ? CreatePlayerAilmentOutputData(x.CharacterId)
                    : CreateEnemyAilmentOutputData(x.CharacterId))
                .ToImmutableList();
            return ailmentOutputDataList;
        }

        public ImmutableList<AilmentOutputData> Create(IList<CharacterId> characterIdList)
        {
            return characterIdList
                .Select(Create)
                .ToImmutableList();
        }

        public AilmentOutputData Create(CharacterId characterId)
        {
            var ailmentOutputData = _characterRepository.Select(characterId).IsPlayer()
                ? CreatePlayerAilmentOutputData(characterId)
                : CreateEnemyAilmentOutputData(characterId);
            return ailmentOutputData;
        }

        [Obsolete]
        public PlayerImageOutputData CreatePlayerImageOutputData(
            IList<AilmentSkillResultValueObject> ailmentSkillResultList)
        {
            var ailmentCode = ailmentSkillResultList
                .First(x => _characterRepository.Select(x.ActorId).IsPlayer())
                .AilmentCode;
            var playerImageCode = _ailmentViewInfoFactory.Create(ailmentCode).PlayerImageCode;
            return new PlayerImageOutputData(playerImageCode);
        }

        private AilmentOutputData CreatePlayerAilmentOutputData(CharacterId characterId)
        {
            var slipDamageNumberList
                = _slipDamageRepository.Select()
                    .Select(x => _toAilmentNumber.SlipDamage(x.SlipDamageCode))
                    .ToImmutableList();

            return new AilmentOutputData(
                true,
                default,
                _ailmentRepository.Select(characterId)
                    .Select(x => _toAilmentNumber.Ailment(x.AilmentCode))
                    .Concat(slipDamageNumberList)
                    .ToImmutableList());
        }

        private AilmentOutputData CreateEnemyAilmentOutputData(CharacterId characterId)
        {
            return new AilmentOutputData(
                false,
                _enemyRepository.Select(characterId).EnemyNumber,
                _ailment.GetOrdered(characterId)
                    .Select(x => _toAilmentNumber.Ailment(x.AilmentCode))
                    .ToImmutableList());
        }
    }
}