using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.IUseCase;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class PrimeSkillUseCase<TPrimeSkillParameter, TPrimeSkill> : IPrimeSkillUseCase<TPrimeSkillParameter, TPrimeSkill>
        where TPrimeSkill : PrimeSkillValueObject
    {
        private readonly IPrimeSkillGeneratorService<TPrimeSkillParameter, TPrimeSkill> _primeSkillGenerator;
        private readonly IPrimeSkillRegistererService<TPrimeSkill> _primeSkillRegisterer;
        private readonly BattleLoggerService _battleLogger;

        public PrimeSkillUseCase(
            IPrimeSkillGeneratorService<TPrimeSkillParameter, TPrimeSkill> primeSkillGenerator,
            IPrimeSkillRegistererService<TPrimeSkill> primeSkillRegisterer,
            BattleLoggerService battleLogger)
        {
            _primeSkillGenerator = primeSkillGenerator;
            _primeSkillRegisterer = primeSkillRegisterer;
            _battleLogger = battleLogger;
        }

        public IReadOnlyList<TPrimeSkill> Commit(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var primeSkillList = _primeSkillGenerator.Generate(
                skillCommon: skillCommon,
                primeSkillParameterList: primeSkillParameterList,
                targetIdList: targetIdList);
            _primeSkillRegisterer.Register(primeSkillList);
            _battleLogger.Log(primeSkillList);
            return primeSkillList;
        }
    }
}