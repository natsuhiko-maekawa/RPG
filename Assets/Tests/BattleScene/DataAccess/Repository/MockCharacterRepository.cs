// using System.Collections.Generic;
// using System.Linq;
// using BattleScene.Domain.Code;
// using BattleScene.Domain.DataAccess;
// using BattleScene.Domain.Entity;
// using BattleScene.Domain.Id;
//
// namespace Tests.BattleScene.DataAccess.Repository
// {
//     public class MockCharacterRepository : IRepository<CharacterEntity, CharacterId>
//     {
//         private readonly HashSet<CharacterEntity> _characterSet;
//         
//         public MockCharacterRepository()
//         {
//             var player = new CharacterEntity(
//                 id: new CharacterId(),
//                 characterTypeCode: CharacterTypeCode.Player,
//                 currentHitPoint: 179,
//                 currentTechnicalPoint: 50);
//             _characterSet.Add(player);
//             
//             var bee = new CharacterEntity(
//                 id:  new CharacterId(),
//                 characterTypeCode: CharacterTypeCode.Bee,
//                 currentHitPoint: 39,
//                 position: 0);
//             _characterSet.Add(bee);
//         }
//         
//         public CharacterEntity Select(CharacterId id)
//         {
//             
//         }
//
//         public IReadOnlyList<CharacterEntity> Select()
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public void Update(CharacterEntity entity)
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public void Update(IReadOnlyList<CharacterEntity> entityList)
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public void Delete()
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public void Delete(CharacterId id)
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public void Delete(IReadOnlyList<CharacterId> idList)
//         {
//             throw new System.NotImplementedException();
//         }
//     }
// }