using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.DomainService
{
    public class EnemiesDomainService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public EnemiesDomainService(IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public IReadOnlyList<CharacterEntity> Get()
        {
            return _characterRepository.Get()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .ToArray();
        }

        public IReadOnlyList<CharacterEntity> GetSurvive()
        {
            var surviveEnemyArray = _characterRepository.Get()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .Where(x => x.IsSurvive)
                .ToArray();
            return surviveEnemyArray;
        }

        public CharacterEntity GetByPosition(int position)
        {
            return _characterRepository.Get()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .Single(x => x.Position == position);
        }
    }
}