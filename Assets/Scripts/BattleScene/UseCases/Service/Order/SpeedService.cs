using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.IDomainService;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service.Order
{
    public class SpeedService : ISpeedService
    {
        private readonly IBuffDomainService _buff;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;

        public SpeedService(
            IBuffDomainService buff,
            CharacterPropertyFactoryService characterPropertyFactory)
        {
            _buff = buff;
            _characterPropertyFactory = characterPropertyFactory;
        }

        public int Get(CharacterId characterId)
        {
            var agility = (float)_characterPropertyFactory.Create(characterId).Agility;
            var speedRate = _buff.GetRate(characterId, BuffCode.Speed);
            var speed = (int)Math.Ceiling(agility * speedRate);
            return speed;
        }
    }
}