using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IUseCase;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class PrimeSkillStartState<TPrimeSkillParameter, TPrimeSkill>
        : BaseState<TPrimeSkillParameter, TPrimeSkill> where TPrimeSkill : PrimeSkillValueObject
    {
        private readonly IPrimeSkillUseCase<TPrimeSkillParameter, TPrimeSkill> _primeSkillUseCase;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly PrimeSkillOutputState<TPrimeSkillParameter, TPrimeSkill> _primeSkillOutputState;
        private readonly PrimeSkillStopState<TPrimeSkillParameter, TPrimeSkill> _primeSkillStopState;

        public PrimeSkillStartState(
            IPrimeSkillUseCase<TPrimeSkillParameter, TPrimeSkill> primeSkillUseCase,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            PrimeSkillOutputState<TPrimeSkillParameter, TPrimeSkill> primeSkillOutputState,
            PrimeSkillStopState<TPrimeSkillParameter, TPrimeSkill> primeSkillStopState)
        {
            _primeSkillUseCase = primeSkillUseCase;
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
            var primeSkillList = _primeSkillUseCase.Commit(
                skillCommon: Context.SkillCommon,
                primeSkillParameterList: Context.PrimeSkillParameterList,
                targetIdList: Context.TargetIdList);
            var primeSkillListExceptFailure = primeSkillList
                .Where(x => !x.IsFailure);
            Context.PrimeSkillQueue = new Queue<TPrimeSkill>(primeSkillListExceptFailure);

            BaseState<TPrimeSkillParameter, TPrimeSkill> nextState =
                Context.PrimeSkillQueue.Count == 0
                && Context.TargetIdList.All(x => _characterCollection.Get(x).IsPlayer)
                && Context.ExecutedPrimeSkillCodeList.Contains(PrimeSkillCode.Damage)
                    ? _primeSkillStopState
                    : _primeSkillOutputState;
            Context.TransitionTo(nextState);
        }
    }
}