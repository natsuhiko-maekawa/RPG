using System.Collections.Generic;
using UnityEngine;

namespace BattleScene.Framework
{
    public class SpriteFlyweight : ISpriteFlyweight
    {
        private readonly Dictionary<string, Sprite> _imageDict = new();

        public Sprite Get(string imageName)
        {
            return _imageDict[imageName];
        }

        public void Add(string imageName, Sprite sprite)
        {
            _imageDict.Add(imageName, sprite);
        }
    }
}