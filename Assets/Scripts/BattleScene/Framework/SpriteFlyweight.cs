using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.Framework.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BattleScene.Framework
{
    /// <summary>
    /// SpriteをAddressableからロードしてインスタンスを管理するクラス
    /// </summary>
    public class SpriteFlyweight : Singleton<SpriteFlyweight>
    {
        private readonly Dictionary<string, ValueTask<Sprite>> _spriteCache = new();
        private readonly object _syncObject = new();
        private bool _throwsInvalidKeyException;

        private void Awake()
        {
            // https://docs.unity3d.com/Packages/com.unity.addressables@1.15/manual/ExceptionHandler.html
            ResourceManager.ExceptionHandler = ExceptionHandler;
        }

        /// <summary>
        /// AddressableのパスからSpriteを取得する
        /// </summary>
        /// <param name="path">Addressableのパス</param>
        /// <returns>Spriteのインスタンス</returns>
        public ValueTask<Sprite> GetAsync(string path)
        {
            if (_spriteCache.TryGetValue(path, out var valueTask))
            {
                return valueTask;
            }

            return LoadAsync(path);
        }

        private async ValueTask<Sprite> LoadAsync(string path)
        {
            Task<Sprite> task;
            lock (_syncObject)
            {
                task = Addressables.LoadAssetAsync<Sprite>(path).Task;
                if (_throwsInvalidKeyException)
                {
                    _throwsInvalidKeyException = false;
                    throw new ArgumentException();
                }
            }

            _spriteCache.Add(path, new ValueTask<Sprite>(task));
            var sprite = await task;
            _spriteCache[path] = new ValueTask<Sprite>(sprite);
            return sprite;
        }

        private void ExceptionHandler(AsyncOperationHandle handle, Exception e)
        {
            if (e.GetType() == typeof(InvalidKeyException))
                _throwsInvalidKeyException = true;
        }
    }
}