using System.Collections.Generic;
using BattleScene.Infrastructure.Factory.Dto;

namespace BattleScene.Infrastructure.IScriptableObject
{
    public interface IBattleSceneScriptableObject
    {
        public List<MessageDto> GetMsgScriptableObject();
        public List<PropertyDataStore> GetPropertyScriptableObject();
    }
}