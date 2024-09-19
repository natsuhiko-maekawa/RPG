using System.Collections.Generic;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.DataAccess.Repository
{
    public class CharacterRepository : Repository<CharacterEntity, CharacterId>
    {
        private readonly IEnumerable<IObserver<CharacterEntity>> _characterObserver;

        public CharacterRepository(
            IEnumerable<IObserver<CharacterEntity>> characterObserver)
        {
            _characterObserver = characterObserver;
        }

        public override void Update(CharacterEntity entity)
        {
            foreach (var characterObserver in _characterObserver)
            {
               characterObserver.Observe(entity);
            }
            base.Update(entity);
        }
    }
}