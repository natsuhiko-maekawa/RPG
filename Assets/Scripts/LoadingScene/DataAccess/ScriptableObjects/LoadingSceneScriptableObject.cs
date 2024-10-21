using System.Collections.Generic;
using LoadingScene.DataAccess.Dto;
using UnityEngine;

namespace LoadingScene.DataAccess.ScriptableObjects
{
    public class LoadingSceneScriptableObject : MonoBehaviour
    {
        [SerializeField] private TipsScriptableObject tipsScriptableObject;

        public List<TipsDto> GetTipsScriptableObject()
        {
            return tipsScriptableObject.tipsList;
        }
    }
}