using System.Collections.Generic;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.IScriptableObject
{
    public interface IBattleSceneScriptableObject
    {
        public List<MessageDto> GetMsgScriptableObject();
        public List<PropertyDataStore> GetPropertyScriptableObject();
    }
}