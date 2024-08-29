using System.Collections.Immutable;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.IRepository
{
    public interface ITurnRepository
    {
        public ImmutableList<TurnEntity> Select();
    }
}