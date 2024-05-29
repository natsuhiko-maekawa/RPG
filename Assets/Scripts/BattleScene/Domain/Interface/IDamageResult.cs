using System.Collections.Immutable;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Interface
{
    public interface IDamageResult : IResult
    {
        public ImmutableList<DamageValueObject> DamageList { get; }
        public int GetTotal();
    }
}