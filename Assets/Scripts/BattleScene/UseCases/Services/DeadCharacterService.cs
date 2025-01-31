using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.Services
{
    public class DeadCharacterService : IDeadCharacterService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public DeadCharacterService(
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public bool IsAnyCharacterDeadInThisTurn()
        {
            var value = _characterRepository.Get()
                .Where(x => x.IsSurvive)
                .Any(x => x.CurrentHitPoint == 0);
            return value;
        }

        public bool IsPlayerDeadInThisTurn()
        {
            var value = _characterRepository.Get()
                .Single(x => x.IsPlayer)
                .CurrentHitPoint == 0;
            return value;
        }

        public IReadOnlyList<CharacterEntity> GetDeadCharacterInThisTurn()
        {
            var value = _characterRepository.Get()
                .Where(x => x.IsSurvive)
                .Where(x => x.CurrentHitPoint == 0)
                .ToArray();
            return value;
        }

        public Dead GetDeadInThisTurn()
        {
            if (IsPlayerDeadInThisTurn()) return Dead.Player;
            if (_characterRepository.Get()
                .Where(x => !x.IsPlayer)
                .Where(x => x.IsSurvive)
                .All(x => x.CurrentHitPoint == 0)) return Dead.Enemies;
            if (IsAnyCharacterDeadInThisTurn()) return Dead.Enemy;
            return Dead.None;
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

        public bool IsAllEnemyDead()
        {
            var value = _characterRepository.Get()
                .Where(x => !x.IsPlayer)
                .All(x => !x.IsSurvive);
            return value;
        }
    }

    public enum Dead
    {
        None,
        Player,
        Enemy,
        Enemies,
    }
}