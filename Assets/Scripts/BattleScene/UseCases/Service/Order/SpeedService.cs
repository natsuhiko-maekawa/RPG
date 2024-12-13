using System;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service.Order
{
    public class SpeedService : ISpeedService
    {
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;

        public SpeedService(
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository,
            CharacterPropertyFactoryService characterPropertyFactory)
        {
            _buffRepository = buffRepository;
            _characterPropertyFactory = characterPropertyFactory;
        }

        public int GetSpeed(CharacterId characterId)
        {
            var agility = (float)_characterPropertyFactory.Create(characterId).Agility;
            var speedRate = _buffRepository.Get((characterId, BuffCode.Speed)).Rate;
            var speed = (int)Math.Ceiling(agility * speedRate);
            return speed;
        }

        public int GetAgility(CharacterId characterId)
        {
            var agility = _characterPropertyFactory.Create(characterId).Agility;
            return agility;
        }
    }
}