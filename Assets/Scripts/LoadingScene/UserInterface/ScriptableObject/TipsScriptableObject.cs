using System.Collections.Generic;
using LoadingScene.InterfaceAdapter.Repository.Dto;
using UnityEngine;

namespace LoadingScene.UserInterface.ScriptableObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Tips")]
    internal class TipsScriptableObject : UnityEngine.ScriptableObject
    {
        public List<TipsDto> tipsList = new();
    }
}