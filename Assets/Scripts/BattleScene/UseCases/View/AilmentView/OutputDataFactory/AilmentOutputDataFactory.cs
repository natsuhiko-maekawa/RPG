using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.UseCases.View.AilmentView.OutputData;

namespace BattleScene.UseCases.View.AilmentView.OutputDataFactory
{
    internal class AilmentOutputDataFactory
    {
        private readonly IAilmentRepository _ailmentRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IRepository<SlipDamageEntity, SlipDamageId> _slipDamageRepository;

        public AilmentOutputDataFactory(
            IAilmentRepository ailmentRepository,
            ICharacterRepository characterRepository,
            IRepository<SlipDamageEntity, SlipDamageId> slipDamageRepository)
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
                AilmentCodeList: _ailmentRepository.Select(characterId).Select(x => x.AilmentCode).ToImmutableList(),
                SlipDamageCodeList: _slipDamageRepository.Select().Select(x => x.SlipDamageCode).ToImmutableList());
        }
    }
}