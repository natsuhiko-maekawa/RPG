using UnityEngine;

namespace BattleScene.Framework
{
    public interface ISpriteFlyweight
    {
        public Sprite Get(string imageName);
        public void Add(string imageName, Sprite sprite);
    }
}