using System;
using System.Threading.Tasks;
using UnityEngine;

namespace BattleScene.Framework
{
    public interface ISpriteFlyweight
    {
        public Task<Sprite> Get(string imagePath);
        [Obsolete]
        public void Add(string imageName, Sprite sprite);
    }
}