using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class SkillElementStartState<TSkillElement> : BaseState<TSkillElement>
    {
        private readonly SkillElementUseCase<TSkillElement> _useCase;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly SkillElementOutputState<TSkillElement> _skillElementOutputState;
        private readonly SkillElementStopState<TSkillElement> _skillElementStopState;

        public SkillElementStartState(
            SkillElementUseCase<TSkillElement> useCase,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            SkillElementOutputState<TSkillElement> skillElementOutputState,
            SkillElementStopState<TSkillElement> skillElementStopState)
        {
            _useCase = useCase;
            _characterCollection = characterCollection;
            _skillElementOutputState = skillElementOutputState;
            _skillElementStopState = skillElementStopState;
        }

        /// <summary>
        /// Contextのプロパティに値を設定し、画面への出力を行うステートか終端のステートに遷移する。<br/>
        /// 画面へ出力を行うステートに遷移する条件については、Documents/AilmentMessage.mdを参照のこと。
        /// </summary>
        public override void Start()
        {
            var battleEventList = _useCase.GetBattleEventList(
                actorId: Context.ActorId,
                skillCommon: Context.SkillCommon,
                skillElementList: Context.SkillElementList,
                targetIdList: Context.TargetIdList);
            _useCase.RegisterBattleEvent(battleEventList);
            var successBattleEventList = battleEventList
                .Where(x => !x.IsFailure);
            Context.BattleEventQueue = new Queue<BattleEventValueObject>(successBattleEventList);

            BaseState<TSkillElement> nextState =
                Context.BattleEventQueue.Count == 0
                && Context.TargetIdList.All(x => _characterCollection.Get(x).IsPlayer)
                && _useCase.IsExecutedDamage()
                    ? _skillElementStopState
                    : _skillElementOutputState;
            Context.TransitionTo(nextState);
        }
    }
}