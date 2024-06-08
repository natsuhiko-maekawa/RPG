using System.Collections.Immutable;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.IResource
{
    public interface IEnemyViewInfoResource
    {
        public ImmutableList<EnemyViewInfoDto> Get();
    }
}