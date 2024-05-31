using LoadingScene.UserInterface;
using UnityEngine;

namespace Mediator
{
    public class SpriteFlyweight : ISpriteFlyweight
    {
        private readonly BattleScene.UserInterface.ISpriteFlyweight _spriteFlyweight;

        public SpriteFlyweight(
            BattleScene.UserInterface.ISpriteFlyweight spriteFlyweight)
        {
            _spriteFlyweight = spriteFlyweight;
        }

        public void Add(string imageName, Sprite sprite)
        {
            _spriteFlyweight.Add(imageName, sprite);
        }
    }
}