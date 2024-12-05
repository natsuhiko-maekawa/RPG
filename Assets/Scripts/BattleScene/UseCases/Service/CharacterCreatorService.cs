using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class CharacterCreatorService : ICharacterCreatorService
    {
        private readonly ICollection<AilmentEntity, (CharacterId, AilmentCode)> _ailmentCollection;
        private readonly IFactory<AilmentPropertyValueObject, AilmentCode> _ailmentPropertyFactory;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly ICollection<BuffEntity, (CharacterId, BuffCode)> _buffCollection;
        private readonly ICollection<SlipEntity, SlipCode> _slipCollection;

        public CharacterCreatorService(
            ICollection<AilmentEntity, (CharacterId, AilmentCode)> ailmentCollection,
            IFactory<AilmentPropertyValueObject, AilmentCode> ailmentPropertyFactory,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            ICollection<BuffEntity, (CharacterId, BuffCode)> buffCollection,
            ICollection<SlipEntity, SlipCode> slipCollection)
        {
            _ailmentCollection = ailmentCollection;
            _ailmentPropertyFactory = ailmentPropertyFactory;
            _battlePropertyFactory = battlePropertyFactory;
            _buffCollection = buffCollection;
            _slipCollection = slipCollection;
        }

        public void Create(CharacterId characterId)
        {
            var buffCodes = Enum.GetValues(typeof(BuffCode))
                .Cast<BuffCode>()
                .Where(x => x != BuffCode.NoBuff);
            foreach (var buffCode in buffCodes)
            {
                var buff = new BuffEntity(
                    characterId: characterId,
                    buffCode: buffCode);
                _buffCollection.Add(buff);
            }

            var defaultTurn = _battlePropertyFactory.Create().SlipDefaultTurn;
            var slipCodes = Enum.GetValues(typeof(SlipCode))
                .Cast<SlipCode>()
                .Where(x => x != SlipCode.NoSlip);
            foreach (var slipCode in slipCodes)
            {
                var slip = new SlipEntity(
                    slipCode: slipCode,
                    defaultTurn: defaultTurn);
                _slipCollection.Add(slip);
            }

            var ailmentCodes = Enum.GetValues(typeof(AilmentCode))
                .Cast<AilmentCode>()
                .Where(x => x != AilmentCode.NoAilment);
            foreach (var ailmentCode in ailmentCodes)
            {
                var ailmentProperty = _ailmentPropertyFactory.Create(ailmentCode);
                var ailment = new AilmentEntity(
                    characterId: characterId,
                    ailmentCode: ailmentCode,
                    isSelfRecovery: ailmentProperty.IsSelfRecovery);
                _ailmentCollection.Add(ailment);
            }
        }

        public void Create(IEnumerable<CharacterId> characterIdList)
        {
            foreach (var characterId in characterIdList) Create(characterId);
        }
    }
}