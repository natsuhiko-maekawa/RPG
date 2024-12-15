using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entities;
using BattleScene.UseCases.UseCases;

namespace BattleScene.Presenters.States.Skill
{
    public class SkillStartState<TSkillComponent> : BaseState<TSkillComponent>
    {
        private readonly SkillUseCase<TSkillComponent> _useCase;
        private readonly SkillOutputState<TSkillComponent> _skillOutputState;
        private readonly SkillStopState<TSkillComponent> _skillStopState;

        public SkillStartState(
            SkillUseCase<TSkillComponent> useCase,
            SkillOutputState<TSkillComponent> skillOutputState,
            SkillStopState<TSkillComponent> skillStopState)
        {
            _useCase = useCase;
            _skillOutputState = skillOutputState;
            _skillStopState = skillStopState;
        }

        /// <summary>
        /// Contextのプロパティに値を設定し、画面への出力を行うステートか終端のステートに遷移する。<br/>
        /// 画面へ出力を行うステートに遷移する条件については、Documents/AilmentMessage.mdを参照のこと。
        /// </summary>
        public override void Start()
        {
            var battleEventList = _useCase.ExecuteBattleEvent(
                skillCommon: Context.SkillCommon,
                skillComponentList: Context.SkillComponentList,
                targetList: Context.TargetList);
            Context.BattleEventQueue = new Queue<BattleEventEntity>(battleEventList);

            BaseState<TSkillComponent> nextState =
                battleEventList.All(x => x.IsFailure)
                && Context.TargetList.All(x => x.IsPlayer)
                && _useCase.IsExecutedDamage()
                    ? _skillStopState
                    : _skillOutputState;
            Context.TransitionTo(nextState);
        }
    }
}