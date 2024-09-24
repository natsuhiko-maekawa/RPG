﻿using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Interface;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class PrimeSkillStartState<TPrimeSkillParameter, TPrimeSkill>
        : BaseState<TPrimeSkillParameter, TPrimeSkill> where TPrimeSkill : PrimeSkillValueObject
    {
        private readonly IPrimeSkill<TPrimeSkillParameter, TPrimeSkill> _primeSkill;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly PrimeSkillOutputState<TPrimeSkillParameter, TPrimeSkill> _primeSkillOutputState;
        private readonly PrimeSkillStopState<TPrimeSkillParameter, TPrimeSkill> _primeSkillStopState;

        public PrimeSkillStartState(
            IPrimeSkill<TPrimeSkillParameter, TPrimeSkill> primeSkill,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            PrimeSkillOutputState<TPrimeSkillParameter, TPrimeSkill> primeSkillOutputState,
            PrimeSkillStopState<TPrimeSkillParameter, TPrimeSkill> primeSkillStopState)
        {
            _primeSkill = primeSkill;
            _characterRepository = characterRepository;
            _primeSkillOutputState = primeSkillOutputState;
            _primeSkillStopState = primeSkillStopState;
        }

        public override void Start()
        {
            var primeSkillList = _primeSkill.Commit(
                skillCommon: Context.SkillCommon,
                primeSkillParameterList: Context.PrimeSkillParameterList,
                targetIdList: Context.TargetIdList);
            var primeSkillListExceptFailure = primeSkillList
                .Where(x => !x.IsFailure);
            Context.PrimeSkillQueue = new Queue<TPrimeSkill>(primeSkillListExceptFailure);

            BaseState<TPrimeSkillParameter, TPrimeSkill> nextState =
                Context.PrimeSkillQueue.Count == 0 &&
                Context.TargetIdList.All(x => _characterRepository.Select(x).IsPlayer)
                    ? _primeSkillStopState
                    : _primeSkillOutputState;
            Context.TransitionTo(nextState);
        }
    }
}