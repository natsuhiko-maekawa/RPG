using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.IUseCase;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class
        PrimeSkillUseCase<TPrimeSkillParameter> : IPrimeSkillUseCase<TPrimeSkillParameter>
    {
        private readonly IPrimeSkillService<TPrimeSkillParameter> _primeSkill;
        private readonly BattleLoggerService _battleLogger;

        public PrimeSkillUseCase(
            IPrimeSkillService<TPrimeSkillParameter> primeSkill,
            BattleLoggerService battleLogger)
        {
            _primeSkill = primeSkill;
            _battleLogger = battleLogger;
        }

        public IReadOnlyList<BattleEventValueObject> Commit(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var primeSkillList = _primeSkill.Generate(
                skillCommon: skillCommon,
                primeSkillParameterList: primeSkillParameterList,
                targetIdList: targetIdList);
            _primeSkill.Register(primeSkillList);
            _battleLogger.Log(primeSkillList);
            return primeSkillList;
        }
    }
}