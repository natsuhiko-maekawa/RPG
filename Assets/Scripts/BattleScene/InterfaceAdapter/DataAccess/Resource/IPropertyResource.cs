using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public interface IPropertyResource
    {
        public List<PropertyDto> Get();
    }
}