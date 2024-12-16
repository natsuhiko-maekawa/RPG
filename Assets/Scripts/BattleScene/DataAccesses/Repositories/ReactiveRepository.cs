using System.Collections.Generic;

namespace BattleScene.DataAccesses.Repositories
{
    public partial class Repository<TEntity, TId>
    {
        private readonly IEnumerable<IReactive<TEntity>> _observers;

        public Repository(
            IEnumerable<IReactive<TEntity>> observers)
        {
            _observers = observers;
        }

        partial void Observe(TEntity entity)
        {
            foreach (var observer in _observers)
            {
                observer.Observe(entity);
            }
        }
    }
}