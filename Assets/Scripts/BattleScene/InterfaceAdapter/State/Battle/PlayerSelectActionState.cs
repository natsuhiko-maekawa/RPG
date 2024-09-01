using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View;
using BattleScene.UseCases.View.GridView;

namespace BattleScene.UseCases.State.Battle
{
    public class PlayerSelectActionState : AbstractState
    {
        private readonly AttackCounterService _attackCounter;
        private readonly IViewPresenter<GridViewOutputData> _gridView;

        public PlayerSelectActionState(
            AttackCounterService attackCounter,
            IViewPresenter<GridViewOutputData> gridView)
        {
            _attackCounter = attackCounter;
            _gridView = gridView;
        }

        public override void Start()
        {
            var fatalitySkillEnabled = _attackCounter.IsOverflow();
            var rowList = ImmutableList.Create(
                new Row(ActionCode: ActionCode.Attack, Enabled: true),
                new Row(ActionCode: ActionCode.Skill, Enabled: true),
                new Row(ActionCode: ActionCode.Defence, Enabled: true),
                new Row(ActionCode: ActionCode.FatalitySkill, Enabled: fatalitySkillEnabled));
            var outputData = new GridViewOutputData(rowList);
            _gridView.Start(outputData);
        }

        public override void Select(ActionCode actionCode)
        {
            base.Select(actionCode);
        }
    }
}