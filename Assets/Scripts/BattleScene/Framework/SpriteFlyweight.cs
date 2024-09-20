using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utility;

namespace BattleScene.Framework
{
    public class SpriteFlyweight : Singleton<SpriteFlyweight>
    {
        private readonly Dictionary<string, ValueTask<Sprite>> _spriteCache = new();
        private bool _throwsException;

        private void Start()
        {
            ResourceManager.ExceptionHandler += ExceptionHandler;
        }
        
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
            if (_throwsException)
            {
                _throwsException = false;
                throw new ArgumentException();
            }
            
            _spriteCache.Add(imagePath, new ValueTask<Sprite>(task));
            var sprite = await task;
            _spriteCache[imagePath] = new ValueTask<Sprite>(sprite);
            return sprite;
        }

        // https://baba-s.hatenablog.com/entry/2020/03/12/110000
        private void ExceptionHandler(AsyncOperationHandle handle, Exception e)
        {
            _throwsException = true;
        }
    }
}