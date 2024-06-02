// using System;
// using System.Collections.Generic;
// using System.Linq;
// using BattleScene.Domain.Code;
// using BattleScene.InterfaceAdapter;
//
// namespace BattleScene.Infrastructure
// {
//     public class LoadingImage : ILoadingImage
//     {
//         public IList<string> GetImageNameList()
//         {
//             return Enum.GetValues(typeof(PlayerImageCode))
//                 .Cast<PlayerImageCode>()
//                 // NoImageは画像がないことを表す列挙子なので除外する
//                 .Where(x => x != PlayerImageCode.NoImage)
//                 .Select(x => x.ToString())
//                 .Concat(Enum.GetValues(typeof(CharacterTypeId))
//                     .Cast<CharacterTypeId>()
//                     .Where(x => x != CharacterTypeId.Girl)
//                     .Select(x => x.ToString()))
//                 .ToList();
//         }
//     }
// }

