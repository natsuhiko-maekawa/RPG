using System;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class CharacterCreatorService : ICharacterCreatorService
    {
        private readonly IFactory<AilmentPropertyValueObject, AilmentCode> _ailmentPropertyFactory;
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly IFactory<BodyPartPropertyValueObject, BodyPartCode> _bodyPartPropertyFactory;
        private readonly IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> _bodyPartRepository;
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;
        private readonly IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> _enhanceRepository;
        private readonly IRepository<SlipEntity, SlipCode> _slipRepository;

        public CharacterCreatorService(
            IFactory<AilmentPropertyValueObject, AilmentCode> ailmentPropertyFactory,
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            IFactory<BodyPartPropertyValueObject, BodyPartCode> bodyPartPropertyFactory,
            IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> bodyPartRepository,
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository,
            IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> enhanceRepository,
            IRepository<SlipEntity, SlipCode> slipRepository)
        {
            _ailmentPropertyFactory = ailmentPropertyFactory;
            _ailmentRepository = ailmentRepository;
            _battlePropertyFactory = battlePropertyFactory;
            _bodyPartPropertyFactory = bodyPartPropertyFactory;
            _bodyPartRepository = bodyPartRepository;
            _buffRepository = buffRepository;
            _enhanceRepository = enhanceRepository;
            _slipRepository = slipRepository;
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

        public void Create(CharacterId[] characterIdList)
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
                _ailmentRepository.Add(ailment);
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
                _bodyPartRepository.Add(bodyPart);
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
                _buffRepository.Add(buff);
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
                _enhanceRepository.Add(enhance);
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
                _slipRepository.Add(slip);
            }
        }
    }
}