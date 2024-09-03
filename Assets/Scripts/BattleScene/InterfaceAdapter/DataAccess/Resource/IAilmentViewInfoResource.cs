using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.IResource
{
    public interface IAilmentViewInfoResource
    {
        public List<AilmentViewInfoDto> Get();
    }
}