using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.View.AilmentView.OutputData;

namespace BattleScene.UseCases.View.AilmentView.OutputDataFactory
{
    internal class AilmentOutputDataFactory
    {
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<SlipDamageEntity, SlipDamageCode> _slipDamageRepository;

        public AilmentOutputDataFactory(
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<SlipDamageEntity, SlipDamageCode> slipDamageRepository)
        {
            _ailmentRepository = ailmentRepository;
            _characterRepository = characterRepository;
            _slipDamageRepository = slipDamageRepository;
        }

        public ImmutableList<AilmentOutputData> Create()
        {
            var ailmentOutputDataList = _characterRepository.Select()
                .Select(x => Create(x.Id))
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
            return new AilmentOutputData(
                CharacterId: characterId,
                AilmentCodeList: _ailmentRepository.Select()
                    .Where(x => Equals(x.CharacterId, characterId))
                    .Select(x => x.AilmentCode).ToImmutableList(),
                SlipDamageCodeList: _slipDamageRepository.Select().Select(x => x.Id).ToImmutableList());
        }
    }
}