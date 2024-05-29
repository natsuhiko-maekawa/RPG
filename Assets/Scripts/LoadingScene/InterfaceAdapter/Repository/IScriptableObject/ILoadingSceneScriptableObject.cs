using System.Collections.Generic;
using LoadingScene.InterfaceAdapter.Repository.Dto;

namespace LoadingScene.InterfaceAdapter.Repository.IScriptableObject
{
    public interface ILoadingSceneScriptableObject
    {
        public List<TipsDto> GetTipsScriptableObject();
    }
}