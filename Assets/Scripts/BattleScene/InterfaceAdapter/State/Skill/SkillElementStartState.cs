using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class SkillElementStartState<TSkillElement> : BaseState<TSkillElement>
    {
        private readonly SkillElementUseCase<TSkillElement> _useCase;
        private readonly SkillElementOutputState<TSkillElement> _skillElementOutputState;
        private readonly SkillElementStopState<TSkillElement> _skillElementStopState;

        public SkillElementStartState(
            SkillElementUseCase<TSkillElement> useCase,
            SkillElementOutputState<TSkillElement> skillElementOutputState,
            SkillElementStopState<TSkillElement> skillElementStopState)
        {
            _useCase = useCase;
            _skillElementOutputState = skillElementOutputState;
            _skillElementStopState = skillElementStopState;
        }

        /// <summary>
        /// Contextのプロパティに値を設定し、画面への出力を行うステートか終端のステートに遷移する。<br/>
        /// 画面へ出力を行うステートに遷移する条件については、Documents/AilmentMessage.mdを参照のこと。
        /// </summary>
        public override void Start()
        {
            var battleEventList = _useCase.ExecuteBattleEvent(
                skillCommon: Context.SkillCommon,
                skillElementList: Context.SkillElementList,
                targetList: Context.TargetList);
            Context.BattleEventQueue = new Queue<BattleEventEntity>(battleEventList);

            BaseState<TSkillElement> nextState =
                battleEventList.All(x => x.IsFailure)
                && Context.TargetList.All(x => x.IsPlayer)
                && _useCase.IsExecutedDamage()
                    ? _skillElementStopState
                    : _skillElementOutputState;
            Context.TransitionTo(nextState);
        }
    }
}