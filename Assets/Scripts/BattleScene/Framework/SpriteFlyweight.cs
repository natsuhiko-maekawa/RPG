using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BattleScene.Framework
{
    public class SpriteFlyweight : ISpriteFlyweight
    {
        private readonly Dictionary<string, Sprite> _spritePool = new();

        public async Task<Sprite> Get(string imagePath)
        {
            if (_spritePool.TryGetValue(imagePath, out var sprite))
                return sprite;

            await Load(imagePath);
            return _spritePool[imagePath];
        }
        
        private async Task Load(string imagePath)
        {
            var handle = Addressables.LoadAssetAsync<Sprite>(imagePath);
            var sprite = await handle.Task;
            _spritePool.Add(imagePath, sprite); 
        }

        public void Add(string imageName, Sprite sprite)
        {
            _spritePool.Add(imageName, sprite);
        }
    }
}