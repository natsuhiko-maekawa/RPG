using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.View.AilmentView.OutputData;

namespace BattleScene.UseCase.View.AilmentView.OutputDataFactory
{
    internal class AilmentOutputDataFactory
    {
        private readonly IAilmentRepository _ailmentRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly ISlipDamageRepository _slipDamageRepository;

        public AilmentOutputDataFactory(
            IAilmentRepository ailmentRepository,
            ICharacterRepository characterRepository,
            ISlipDamageRepository slipDamageRepository)
        {
            _ailmentRepository = ailmentRepository;
            _characterRepository = characterRepository;
            _slipDamageRepository = slipDamageRepository;
        }

        public ImmutableList<AilmentOutputData> Create()
        {
            var ailmentOutputDataList = _characterRepository.Select()
                .Select(x => Create(x.CharacterId))
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