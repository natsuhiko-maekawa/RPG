// using BattleScene.Domain.Code;
// using BattleScene.InterfaceAdapter.Presenter;
//
// namespace BattleScene.InterfaceAdapter.State.Battle
// {
//     public class SelectTargetStateFactory
//     {
//         private readonly SkillStateFactory _skillStateFactory;
//         private readonly TargetViewPresenter _targetView;
//
//         public SelectTargetStateFactory(
//             SkillStateFactory skillStateFactory,
//             TargetViewPresenter targetView)
//         {
//             _skillStateFactory = skillStateFactory;
//             _targetView = targetView;
//         }
//
//         public SelectTargetState Create(SkillCode skillCode)
//         {
//             return new SelectTargetState(skillState: _skillStateFactory,
//                 targetView: _targetView,
//                 skillCode: skillCode);
//         }
//     }
// }