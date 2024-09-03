using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public interface IAilmentViewInfoResource
    {
        public List<AilmentViewInfoDto> Get();
    }
}