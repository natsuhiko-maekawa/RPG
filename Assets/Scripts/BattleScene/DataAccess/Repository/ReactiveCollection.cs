using System.Collections.Generic;

namespace BattleScene.DataAccess.Repository
{
    public partial class Collection<TEntity, TId>
    {
        private readonly IEnumerable<IObserver<TEntity>> _observers;
        
        public Collection(
            IEnumerable<IObserver<TEntity>> observers)
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