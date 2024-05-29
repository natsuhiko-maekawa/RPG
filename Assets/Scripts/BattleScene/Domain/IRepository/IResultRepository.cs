using System.Collections.Immutable;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.IRepository
{
    public interface IResultRepository
    {
        public ImmutableList<ResultEntity> Select();
        public void Update(ResultEntity resultEntity);
    }
}