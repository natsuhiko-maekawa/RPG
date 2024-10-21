using System.Collections.Generic;
using System.Linq;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace LoadingScene.DataAccess.Addressable
{
    public class AddressableCount
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