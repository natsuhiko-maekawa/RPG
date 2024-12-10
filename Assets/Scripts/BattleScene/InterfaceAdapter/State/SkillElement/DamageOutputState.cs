using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PresenterFacade;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class DamageOutputState : SkillElementOutputState<DamageValueObject>
    {
        private readonly DamageOutputUseCase _useCase;
        private readonly DamageOutputPresenterFacade _facade;
        private readonly CharacterDeadState<DamageValueObject> _characterDeadState;
        private readonly SkillElementBreakState<DamageValueObject> _skillElementBreakState;
        private readonly SkillElementStopState<DamageValueObject> _skillElementStopState;

        public DamageOutputState(
            DamageOutputUseCase useCase,
            DamageOutputPresenterFacade facade,
            CharacterDeadState<DamageValueObject> characterDeadState,
            SkillElementBreakState<DamageValueObject> skillElementBreakState,
            SkillElementStopState<DamageValueObject> skillElementStopState)
        {
            _useCase = useCase;
            _facade = facade;
            _characterDeadState = characterDeadState;
            _skillElementBreakState = skillElementBreakState;
            _skillElementStopState = skillElementStopState;
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
                return _skillElementBreakState;

            if (_useCase.AnyDeadInThisTurn()) return _characterDeadState;

            return _skillElementStopState;
        }
    }
}