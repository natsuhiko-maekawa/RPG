// using System;
// using System.Collections.Generic;
// using System.Collections.Immutable;
// using BattleScene.Domain.Code;
// using BattleScene.Domain.Entity;
// using BattleScene.Domain.Id;
//
// namespace BattleScene.Domain.IRepository
// {
//     [Obsolete]
//     public interface IBuffRepository
//     {
//         public ImmutableList<BuffEntity> Select(CharacterId characterId);
//         public BuffEntity Select(CharacterId characterId, BuffCode buffCode);
//         public void Update(BuffEntity buffEntity);
//         public void Delete(IList<BuffEntity> buffEntityList);
//     }
// }