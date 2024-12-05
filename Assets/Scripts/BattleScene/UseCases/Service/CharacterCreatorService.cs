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
        private readonly IFactory<AilmentPropertyValueObject, AilmentCode> _ailmentPropertyFactory;
        private readonly ICollection<AilmentEntity, (CharacterId, AilmentCode)> _ailmentCollection;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly IFactory<BodyPartPropertyValueObject, BodyPartCode> _bodyPartPropertyFactory;
        private readonly ICollection<BodyPartEntity, (CharacterId, BodyPartCode)> _bodyPartCollection;
        private readonly ICollection<BuffEntity, (CharacterId, BuffCode)> _buffCollection;
        private readonly ICollection<EnhanceEntity, (CharacterId, EnhanceCode)> _enhanceCollection;
        private readonly ICollection<SlipEntity, SlipCode> _slipCollection;

        public CharacterCreatorService(
            IFactory<AilmentPropertyValueObject, AilmentCode> ailmentPropertyFactory,
            ICollection<AilmentEntity, (CharacterId, AilmentCode)> ailmentCollection,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            IFactory<BodyPartPropertyValueObject, BodyPartCode> bodyPartPropertyFactory,
            ICollection<BodyPartEntity, (CharacterId, BodyPartCode)> bodyPartCollection,
            ICollection<BuffEntity, (CharacterId, BuffCode)> buffCollection,
            ICollection<EnhanceEntity, (CharacterId, EnhanceCode)> enhanceCollection,
            ICollection<SlipEntity, SlipCode> slipCollection)
        {
            _ailmentPropertyFactory = ailmentPropertyFactory;
            _ailmentCollection = ailmentCollection;
            _battlePropertyFactory = battlePropertyFactory;
            _bodyPartPropertyFactory = bodyPartPropertyFactory;
            _bodyPartCollection = bodyPartCollection;
            _buffCollection = buffCollection;
            _enhanceCollection = enhanceCollection;
            _slipCollection = slipCollection;
        }

        public void Create(CharacterId characterId, bool isPlayer = false)
        {
            InitializeAilmentRepository(characterId);
            InitializeBodyPartRepository(characterId);
            InitializeBuffRepository(characterId);
            InitializeEnhanceRepository(characterId);

            if (!isPlayer) return;
            InitializeSlipRepository();
        }

        public void Create(IEnumerable<CharacterId> characterIdList)
        {
            foreach (var characterId in characterIdList) Create(characterId);
        }

        private void InitializeAilmentRepository(CharacterId characterId)
        {
            var ailmentCodes = Enum.GetValues(typeof(AilmentCode))
                .Cast<AilmentCode>()
                .Where(x => x != AilmentCode.NoAilment);
            foreach (var ailmentCode in ailmentCodes)
            {
                var ailmentProperty = _ailmentPropertyFactory.Create(ailmentCode);
                var ailment = new AilmentEntity(
                    characterId: characterId,
                    ailmentCode: ailmentCode,
                    isSelfRecovery: ailmentProperty.IsSelfRecovery,
                    defaultTurn: ailmentProperty.DefaultTurn);
                _ailmentCollection.Add(ailment);
            }
        }

        private void InitializeBodyPartRepository(CharacterId characterId)
        {
            var bodyPartCodes = Enum.GetValues(typeof(BodyPartCode))
                .Cast<BodyPartCode>()
                .Where(x => x != BodyPartCode.NoBodyPart);
            foreach (var bodyPartCode in bodyPartCodes)
            {
                var count = _bodyPartPropertyFactory.Create(bodyPartCode).Count;
                var bodyPart = new BodyPartEntity(
                    characterId: characterId,
                    bodyPartCode: bodyPartCode,
                    count: count);
                _bodyPartCollection.Add(bodyPart);
            }
        }

        private void InitializeBuffRepository(CharacterId characterId)
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
        }

        private void InitializeEnhanceRepository(CharacterId characterId)
        {
            var enhanceCodes = Enum.GetValues(typeof(EnhanceCode))
                .Cast<EnhanceCode>()
                .Where(x => x != EnhanceCode.NoEnhance);
            foreach (var enhanceCode in enhanceCodes)
            {
                var enhance = new EnhanceEntity(
                    characterId: characterId,
                    enhanceCode: enhanceCode);
                _enhanceCollection.Add(enhance);
            }
        }

        private void InitializeSlipRepository()
        {
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
        }
    }
}