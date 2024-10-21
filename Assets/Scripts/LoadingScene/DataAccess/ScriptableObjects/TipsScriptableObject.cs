using System.Collections.Generic;
using LoadingScene.DataAccess.Dto;
using UnityEngine;

namespace LoadingScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Tips")]
    internal class TipsScriptableObject : ScriptableObject
    {
        public List<TipsDto> tipsList = new();
    }
}