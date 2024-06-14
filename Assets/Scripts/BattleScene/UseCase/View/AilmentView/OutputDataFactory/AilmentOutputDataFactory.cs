using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.View.AilmentView.OutputData;

namespace BattleScene.UseCase.View.AilmentView.OutputDataFactory
{
    internal class AilmentOutputDataFactory
    {
        private readonly AilmentDomainService _ailment;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemyRepository _enemyRepository;
        private readonly ISlipDamageRepository _slipDamageRepository;

        public AilmentOutputDataFactory(
            AilmentDomainService ailment,
            IAilmentRepository ailmentRepository,
            ICharacterRepository characterRepository,
            IEnemyRepository enemyRepository,
            ISlipDamageRepository slipDamageRepository)
        {
            _ailment = ailment;
            _ailmentRepository = ailmentRepository;
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
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