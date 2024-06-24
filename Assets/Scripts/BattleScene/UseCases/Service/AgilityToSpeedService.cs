using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.UseCases.Service
{
    public class AgilityToSpeedService
    {
        private readonly IRepository<BuffEntity, BuffId> _buffRepository;
        private readonly ICharacterRepository _characterRepository;

        public AgilityToSpeedService(
            IRepository<BuffEntity, BuffId> buffRepository,
            ICharacterRepository characterRepository)
        {
            _buffRepository = buffRepository;
            _characterRepository = characterRepository;
        }

        public int Convert(CharacterId characterId)
        {
            var speed = (float)_characterRepository.Select(characterId).Property.Agility;
            if (_buffRepository.Select()
                    .Count(x => Equals(x.CharacterId, characterId)) != 0)
            {
                var buffId = new BuffId(characterId, BuffCode.Speed);
                speed *= _buffRepository.Select(buffId)
                    .Rate;
            }
            
            return (int)Math.Ceiling(speed);
        }
    }
}