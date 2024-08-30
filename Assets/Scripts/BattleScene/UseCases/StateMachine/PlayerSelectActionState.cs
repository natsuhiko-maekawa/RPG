using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View;
using BattleScene.UseCases.View.GridView;
using BattleScene.UseCases.View.InfoView.OutputBoundary;
using BattleScene.UseCases.View.SelectActionView.OutputBoundary;

namespace BattleScene.UseCases.StateMachine
{
    public class PlayerSelectActionState : AbstractState
    {
        private readonly AttackCounterService _attackCounter;
        private readonly CharactersDomainService _characters;
        private readonly IInfoViewPresenter _infoView;
        private readonly ISelectActionViewPresenter _selectActionView;
        private readonly SkillCreatorService _skillCreator;
        private readonly ISkillRepository _skillRepository;
        private readonly IViewPresenter<GridViewOutputData> _gridView;
        
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