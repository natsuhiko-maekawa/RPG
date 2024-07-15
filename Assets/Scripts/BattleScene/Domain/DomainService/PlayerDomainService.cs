using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility.Interface;
using static BattleScene.Domain.Code.CharacterTypeId;

namespace BattleScene.Domain.DomainService
{
    public class PlayerDomainService
    {
        private readonly IPropertyFactory _propertyFactory;
        private readonly IRandomEx _randomEx;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        
        public void Add()
        {
            PropertyValueObject property = _propertyFactory.Get(Player);
            var characterId = new CharacterId();
            var player = new CharacterAggregate(characterId, property);
            _characterRepository.Update(player);
            
            var hitPoint =new HitPointAggregate(characterId, property.HitPoint);
            _hitPointRepository.Update(hitPoint);
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