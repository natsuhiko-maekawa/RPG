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
        private readonly IPrimeSkillGeneratorService<TPrimeSkillParameter> _primeSkillGenerator;
        private readonly BattleLoggerService _battleLogger;

        public PrimeSkillUseCase(
            IPrimeSkillGeneratorService<TPrimeSkillParameter> primeSkillGenerator,
            BattleLoggerService battleLogger)
        {
            _primeSkillGenerator = primeSkillGenerator;
            _battleLogger = battleLogger;
        }

        public IReadOnlyList<PrimeSkillValueObject> Commit(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var primeSkillList = _primeSkillGenerator.Generate(
                skillCommon: skillCommon,
                primeSkillParameterList: primeSkillParameterList,
                targetIdList: targetIdList);
            _primeSkillGenerator.Register(primeSkillList);
            _battleLogger.Log(primeSkillList);
            return primeSkillList;
        }
    }
}