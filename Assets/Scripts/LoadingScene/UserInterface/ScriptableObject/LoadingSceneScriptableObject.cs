using System.Collections.Generic;
using LoadingScene.InterfaceAdapter.Repository.Dto;
using LoadingScene.InterfaceAdapter.Repository.IScriptableObject;
using UnityEngine;

namespace LoadingScene.UserInterface.ScriptableObject
{
    public class LoadingSceneScriptableObject : MonoBehaviour, ILoadingSceneScriptableObject
    {
        [SerializeField] private TipsScriptableObject tipsScriptableObject;

        public List<TipsDto> GetTipsScriptableObject()
        {
            return tipsScriptableObject.tipsList;
        }
    }
}