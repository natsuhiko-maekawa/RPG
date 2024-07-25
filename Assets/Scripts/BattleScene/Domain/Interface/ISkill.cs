// using System;
// using System.Collections.Immutable;
// using BattleScene.Domain.Code;
//
// namespace BattleScene.Domain.Interface
// {
//     public interface ISkill
//     {
//         public int TechnicalPoint { get; }
//         public int GetTechnicalPoint();
//
//         public ImmutableList<BodyPartCode> GetDependencyList();
//
//         public Code.Range GetRange();
//         
//         [Obsolete]
//         public ImmutableList<ISkillElement> GetSkillService();
//     }
// }