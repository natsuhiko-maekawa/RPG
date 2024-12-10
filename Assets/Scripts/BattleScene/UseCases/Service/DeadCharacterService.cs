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
                .Any(x => !x.IsSurvive);
            return value;
        }

        public IReadOnlyList<CharacterId> GetDeadCharacterIdInThisTurn()
        {
            var value = _characterRepository.Get()
                .Where(x => !x.IsSurvive)
                .Select(x => x.Id)
                .ToArray();
            return value;
        }

        public void DeleteDeadCharacter()
        {
            _characterRepository.Remove(GetDeadCharacterIdInThisTurn());
        }
    }
}