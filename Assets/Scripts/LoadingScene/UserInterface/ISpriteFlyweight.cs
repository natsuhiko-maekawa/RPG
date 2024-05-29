using UnityEngine;

namespace LoadingScene.UserInterface
{
    public interface ISpriteFlyweight
    {
        public void Add(string imageName, Sprite sprite);
    }
}