using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.IResource
{
    public interface IBattleSceneScriptableObject
    {
        public List<MessageDto> GetMsgScriptableObject();
        public List<PropertyDataStore> GetPropertyScriptableObject();
    }
}