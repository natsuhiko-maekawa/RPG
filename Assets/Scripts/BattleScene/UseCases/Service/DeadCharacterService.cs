using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class DeadCharacterService : IDeadCharacterService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public DeadCharacterService(
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public bool DeadInThisTurn()
        {
            var value = _characterRepository.Get()
                .Where(x => x.IsSurvive)
                .Any(x => x.CurrentHitPoint == 0);
            return value;
        }

        public IReadOnlyList<CharacterEntity> GetDeadInThisTurn()
        {
            var value = _characterRepository.Get()
                .Where(x => x.IsSurvive)
                .Where(x => x.CurrentHitPoint == 0)
                .ToArray();
            return value;
        }

        public void ConfirmedDead()
        {
            foreach (var deadCharacter in _characterRepository.Get()
                         .Where(x => x.IsSurvive)
                         .Where(x => x.CurrentHitPoint == 0))
            {
                deadCharacter.IsSurvive = false;
            }
        }
    }
}