using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.UseCase.Service
{
    public class AgilityToSpeedService
    {
        private readonly IBuffRepository _buffRepository;
        private readonly ICharacterRepository _characterRepository;

        public AgilityToSpeedService(IBuffRepository buffRepository, ICharacterRepository characterRepository)
        {
            _buffRepository = buffRepository;
            _characterRepository = characterRepository;
        }

        public int Convert(CharacterId characterId)
        {
            var speed = (float)_characterRepository.Select(characterId).Property.Agility;
            if (_buffRepository.Select(characterId).Count != 0)
                speed *= _buffRepository.Select(characterId)
                    .Where(x => x.BuffCode == BuffCode.Speed)
                    .DefaultIfEmpty()
                    .Select(x => x.Rate)
                    .Aggregate((x, y) => x * y);

            return (int)Math.Ceiling(speed);
        }
    }
}