using System.Linq;
using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.PresenterFacades;
using BattleScene.UseCases.Services;
using BattleScene.UseCases.UseCases;

namespace BattleScene.Presenters.States.Skill
{
    public class DamageOutputState : SkillOutputState<DamageValueObject>
    {
        private readonly DamageOutputUseCase _useCase;
        private readonly DamageOutputPresenterFacade _facade;
        private readonly CharacterDeadState<DamageValueObject> _characterDeadState;
        private readonly SkillBreakState<DamageValueObject> _skillBreakState;
        private readonly SkillStopState<DamageValueObject> _skillStopState;

        public DamageOutputState(
            DamageOutputUseCase useCase,
            DamageOutputPresenterFacade facade,
            CharacterDeadState<DamageValueObject> characterDeadState,
            SkillBreakState<DamageValueObject> skillBreakState,
            SkillStopState<DamageValueObject> skillStopState)
        {
            _useCase = useCase;
            _facade = facade;
            _characterDeadState = characterDeadState;
            _skillBreakState = skillBreakState;
            _skillStopState = skillStopState;
        }

        public override void Start()
        {
            _facade.Output(Context.BattleEventQueue.Peek());
        }

        public override void Select()
        {
            var nextState = GetNextState();
            Context.TransitionTo(nextState);
        }

        private BaseState<DamageValueObject> GetNextState()
        {
            if (Context.BattleEventQueue.Peek().AttackList
                .All(x => !x.IsHit))
                return _skillBreakState;

            Context.Dead = _useCase.GetDeadInThisTurn();
            if (Context.Dead is Dead.Player or Dead.Enemies) return _characterDeadState;

            return _skillStopState;
        }
    }
}