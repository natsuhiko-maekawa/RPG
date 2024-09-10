﻿using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class CharacterPropertyFactoryService
    {
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public CharacterPropertyFactoryService(
            IFactory<PropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _characterRepository = characterRepository;
        }

        public PropertyValueObject Crate(CharacterId characterId)
        {
            var characterTypeCode = _characterRepository.Select(characterId).CharacterTypeCode;
            var characterProperty = _characterPropertyFactory.Create(characterTypeCode);
            return characterProperty;
        }
    }
}