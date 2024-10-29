using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IDomainService;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service.Order
{
    public class SpeedService : ISpeedService
    {
        private readonly ICollection<BuffEntity, (CharacterId, BuffCode)> _buffCollection;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;

        public SpeedService(
            ICollection<BuffEntity, (CharacterId, BuffCode)> buffCollection,
            CharacterPropertyFactoryService characterPropertyFactory)
        {
            _buffCollection = buffCollection;
            _characterPropertyFactory = characterPropertyFactory;
        }

        public int Get(CharacterId characterId)
        {
            var agility = (float)_characterPropertyFactory.Create(characterId).Agility;
            var speedRate = _buffCollection.Get((characterId, BuffCode.Speed)).Rate;
            var speed = (int)Math.Ceiling(agility * speedRate);
            return speed;
        }
    }
}