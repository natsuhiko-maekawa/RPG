using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;

namespace BattleScene.Domain.DomainServices
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