using System.Collections.Generic;
using System.Linq;
using LoadingScene.InterfaceAdapter.Repository.IAddressable;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace LoadingScene.UserInterface.Addressable
{
    public class AddressableCount : IAddressableCount
    {
#if UNITY_EDITOR
        public int Get()
        {
            var assets = new List<AddressableAssetEntry>();
            AddressableAssetSettingsDefaultObject.Settings.GetAllAssets(assets, true,
                group => group.Name == AddressableAssetSettings.DefaultLocalGroupName);
            // GetAllAssetsメソッドの第4引数でも以下のLINQと同じフィルタをかけることができるが、なぜか正常に動作しなかったため別で記述
            return assets.Count(x => x.IsSubAsset);
        }
#endif
    }
}