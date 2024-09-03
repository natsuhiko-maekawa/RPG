using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DomainService
{
    public class PlayerDomainService
    {
        private readonly IPropertyFactory _propertyFactory;
        // private readonly IFactory<PlayerPropertyDto, CharacterTypeCode> _playerPropertyFactory;
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        private readonly IRepository<TechnicalPointAggregate, CharacterId> _technicalPointRepository;

        public PlayerDomainService(
            IPropertyFactory propertyFactory, 
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository,
            IRepository<TechnicalPointAggregate, CharacterId> technicalPointRepository
            )
        {
            _propertyFactory = propertyFactory;
            _actionTimeRepository = actionTimeRepository;
            _characterRepository = characterRepository;
            _hitPointRepository = hitPointRepository;
            _technicalPointRepository = technicalPointRepository;
        }

        public void Add()
        {
            PropertyValueObject property = _propertyFactory.Get(CharacterTypeCode.Player);
            var characterId = new CharacterId();
            var player = new CharacterAggregate(characterId, property);
            _characterRepository.Update(player);
            
            var hitPoint = new HitPointAggregate(characterId, property.HitPoint);
            _hitPointRepository.Update(hitPoint);
            
            // var technicalPoint = new TechnicalPointAggregate(characterId);

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