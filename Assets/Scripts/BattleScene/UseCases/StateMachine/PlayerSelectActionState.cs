using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OutputDataFactory;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View;
using BattleScene.UseCases.View.GridView;
using BattleScene.UseCases.View.InfoView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.SelectActionView.OutputBoundary;

namespace BattleScene.UseCases.StateMachine
{
    public class PlayerSelectActionState : AbstractState
    {
        private readonly AttackCounterService _attackCounter;
        private readonly CharactersDomainService _characters;
        private readonly IInfoViewPresenter _infoView;
        private readonly SelectActionEventOutputDataFactory _outputDataFactory;
        private readonly ISelectActionViewPresenter _selectActionView;
        private readonly IPlayerImageViewPresenter _playerImageView;
        private readonly SkillCreatorService _skillCreator;
        private readonly ISkillRepository _skillRepository;
        private readonly IViewPresenter<GridViewOutputData> _gridView;
        
        public override void Start()
        {
            var actionList
                = ImmutableList.Create(ActionCode.Attack, ActionCode.Skill, ActionCode.Defence, ActionCode.FatalitySkill);
            var outputData = new GridViewOutputData(actionList);
            _gridView.Start(outputData);
            _playerImageView.Start(_outputDataFactory.CreatePlayerImageOutputData());
        }

        public override void Select(ActionCode actionCode)
        {
            base.Select(actionCode);
        }
    }
}