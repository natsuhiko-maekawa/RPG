﻿using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.Domain.DomainService
{
    public class PlayerDomainService
    {
        private readonly IPropertyFactory _propertyFactory;
        private readonly IRandomEx _randomEx;
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;

        public PlayerDomainService(
            IPropertyFactory propertyFactory, 
            IRandomEx randomEx, 
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository)
        {
            _propertyFactory = propertyFactory;
            _randomEx = randomEx;
            _actionTimeRepository = actionTimeRepository;
            _characterRepository = characterRepository;
            _hitPointRepository = hitPointRepository;
        }

        public void Add()
        {
            PropertyValueObject property = _propertyFactory.Get(CharacterTypeId.Player);
            var characterId = new CharacterId();
            var player = new CharacterAggregate(characterId, property);
            _characterRepository.Update(player);
            
            var hitPoint = new HitPointAggregate(characterId, property.HitPoint);
            _hitPointRepository.Update(hitPoint);

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