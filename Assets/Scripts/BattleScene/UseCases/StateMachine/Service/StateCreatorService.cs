// using System;
// using VContainer;
//
// namespace BattleScene.UseCases.StateMachine.Service
// {
//     internal class StateCreatorService
//     {
//         private readonly IObjectResolver _container;
//
//         public StateCreatorService(
//             IObjectResolver container)
//         {
//             _container = container;
//         }
//         
//         public State Create(StateCode stateCode)
//         {
//             return stateCode switch
//             {
//                 // StateCode.Initialize => _container.Resolve<InitializeStateCreatorService>().Create(),
//                 StateCode.EnemySkill => _container.Resolve<EnemySkillStateCreatorService>().Create(),
//                 _ => throw new ArgumentOutOfRangeException()
//             };
//         }
//     }
// }