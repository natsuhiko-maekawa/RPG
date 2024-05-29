// using LoadingScene.Adapter.Repository.Dto;
// using LoadingScene.Adapter.Repository.IJson;
// using UnityEngine;
// using Utility;
//
// namespace LoadingScene.Infrastructure.Json
// {
//     public class SettingsJson : ISettingsJson
//     {
//         private readonly string _path = $"{Application.dataPath}/Settings";
//         
//         public SettingsDto Get()
//         {
//             return JsonUtilityEx.Load<SettingsDto>(_path);
//         }
//
//         public void Set(SettingsDto settingsDto)
//         {
//             JsonUtilityEx.Save(settingsDto, _path);
//         }
//     }
// }