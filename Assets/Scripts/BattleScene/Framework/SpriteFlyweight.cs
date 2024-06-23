using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BattleScene.Framework
{
    public class SpriteFlyweight : ISpriteFlyweight
    {
        private readonly Dictionary<string, ValueTask<Sprite>> _spriteCache = new();

        public ValueTask<Sprite> Get(string imagePath)
        {
            if (_spriteCache.TryGetValue(imagePath, out var valueTask))
            {
                return valueTask;
            }
            
            return Load(imagePath);
        }
        
        private async ValueTask<Sprite> Load(string imagePath)
        {
            var task = Addressables.LoadAssetAsync<Sprite>(imagePath).Task;
            _spriteCache.Add(imagePath, new ValueTask<Sprite>(task));
            var sprite = await task;
            _spriteCache[imagePath] = new ValueTask<Sprite>(sprite);
            return sprite;
        }
    }
}