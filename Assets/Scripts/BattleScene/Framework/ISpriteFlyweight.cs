using System.Threading.Tasks;
using UnityEngine;

namespace BattleScene.Framework
{
    public interface ISpriteFlyweight
    {
        public ValueTask<Sprite> Get(string imagePath);
    }
}