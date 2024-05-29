using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoadingScene.InterfaceAdapter.Repository.IAddressable;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LoadingScene.UserInterface.Addressable
{
    public class SpriteAddressable : ISpriteAddressable
    {
        private readonly ISpriteFlyweight _spriteFlyweight;
        private readonly List<AsyncOperationHandle<Sprite>> _handleList = new();
        private int _resourceNum;
        
        public SpriteAddressable(
            ISpriteFlyweight spriteFlyweight)
        {
            _spriteFlyweight = spriteFlyweight;
        }

        public float GetProgress(int addressableCount)
        {
            return _handleList
                .Where(x => x.Status == AsyncOperationStatus.Succeeded)
                .Sum(x => x.PercentComplete) / addressableCount;
        }

        public Task Load(IList<string> imageNameList)
        {
            var taskList = new List<Task>();
            foreach (var imageName in imageNameList)
            {
                var handle = Addressables.LoadAssetAsync<Sprite>($"{imageName}[{imageName}]");
                taskList.Add(handle.Task);
                _handleList.Add(handle);
                handle.Completed += x => _spriteFlyweight.Add(imageName, x.Result);
            }

            return Task.WhenAll(taskList);
        }
    }
}