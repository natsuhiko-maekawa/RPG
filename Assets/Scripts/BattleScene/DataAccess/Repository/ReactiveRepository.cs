using System.Collections.Generic;

namespace BattleScene.DataAccess.Repository
{
    public partial class Repository<TEntity, TId>
    {
        private readonly IEnumerable<IObserver<TEntity>> _observers;
        
        public Repository(
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