using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IUseCase;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class PrimeSkillStartState<TPrimeSkillParameter> : BaseState<TPrimeSkillParameter>
    {
        private readonly IPrimeSkillUseCase<TPrimeSkillParameter> _useCase;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly PrimeSkillOutputState<TPrimeSkillParameter> _primeSkillOutputState;
        private readonly PrimeSkillStopState<TPrimeSkillParameter> _primeSkillStopState;

        public PrimeSkillStartState(
            IPrimeSkillUseCase<TPrimeSkillParameter> useCase,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            PrimeSkillOutputState<TPrimeSkillParameter> primeSkillOutputState,
            PrimeSkillStopState<TPrimeSkillParameter> primeSkillStopState)
        {
            _useCase = useCase;
            _characterCollection = characterCollection;
            _primeSkillOutputState = primeSkillOutputState;
            _primeSkillStopState = primeSkillStopState;
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
                primeSkillParameterList: Context.PrimeSkillParameterList,
                targetIdList: Context.TargetIdList);
            _useCase.RegisterBattleEvent(battleEventList);
            var successBattleEventList = battleEventList
                .Where(x => !x.IsFailure);
            Context.BattleEventQueue = new Queue<BattleEventValueObject>(successBattleEventList);

            BaseState<TPrimeSkillParameter> nextState =
                Context.BattleEventQueue.Count == 0
                && Context.TargetIdList.All(x => _characterCollection.Get(x).IsPlayer)
                && _useCase.IsExecutedDamage()
                    ? _primeSkillStopState
                    : _primeSkillOutputState;
            Context.TransitionTo(nextState);
        }
    }
}