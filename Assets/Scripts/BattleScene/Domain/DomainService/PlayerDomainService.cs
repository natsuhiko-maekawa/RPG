using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DomainService
{
    public class PlayerDomainService
    {
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _propertyFactory;
        private readonly IFactory<PlayerPropertyValueObject, CharacterTypeCode> _playerPropertyFactory;
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        private readonly IRepository<TechnicalPointEntity, CharacterId> _technicalPointRepository;

        public PlayerDomainService(
            IFactory<PropertyValueObject, CharacterTypeCode> propertyFactory,
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory,
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository,
            IRepository<TechnicalPointEntity, CharacterId> technicalPointRepository
            )
        {
            _propertyFactory = propertyFactory;
            _playerPropertyFactory = playerPropertyFactory;
            _actionTimeRepository = actionTimeRepository;
            _characterRepository = characterRepository;
            _hitPointRepository = hitPointRepository;
            _technicalPointRepository = technicalPointRepository;
        }

        public void Add()
        {
            PropertyValueObject property = _propertyFactory.Create(CharacterTypeCode.Player);
            var characterId = new CharacterId();
            var player = new CharacterAggregate(characterId, property);
            _characterRepository.Update(player);
            
            var hitPoint = new HitPointAggregate(characterId, property.HitPoint);
            _hitPointRepository.Update(hitPoint);

            var playerProperty = _playerPropertyFactory.Create(CharacterTypeCode.Player);
            var technicalPoint = new TechnicalPointEntity(characterId, playerProperty.TechnicalPoint);
            _technicalPointRepository.Update(technicalPoint);

            var actionTime = new ActionTimeEntity(characterId);
            _actionTimeRepository.Update(actionTime);
        }
        
        public CharacterAggregate Get()
        {
            return _characterRepository.Select().First(x => x.IsPlayer());
        }

        public CharacterId GetId()
        {
            return _characterRepository.Select().First(x => x.IsPlayer()).Id;
        }
    }
}