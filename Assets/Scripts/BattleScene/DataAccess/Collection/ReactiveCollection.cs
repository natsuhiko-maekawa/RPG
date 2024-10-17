using System.Collections.Generic;

namespace BattleScene.DataAccess.Collection
{
    public partial class Collection<TEntity, TId>
    {
        private readonly IEnumerable<IReactive<TEntity>> _observers;

        public Collection(
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