using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
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
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;

        public PrimeSkillUseCase(
            IPrimeSkillService<TPrimeSkillParameter> primeSkill,
            BattleLoggerService battleLogger,
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection)
        {
            _primeSkill = primeSkill;
            _battleLogger = battleLogger;
            _battleLogCollection = battleLogCollection;
        }

        public IReadOnlyList<BattleEventValueObject> Commit(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var primeSkillList = _primeSkill.Generate(
                actorId: actorId,
                skillCommon: skillCommon,
                primeSkillParameterList: primeSkillParameterList,
                targetIdList: targetIdList);
            _primeSkill.Register(primeSkillList);
            _battleLogger.Log(primeSkillList);
            return primeSkillList;
        }

        public bool IsExecutedDamage()
        {
            var value = _battleLogCollection.Get()
                .Where(x => x.Turn == _battleLogCollection.Get().Max().Turn)
                .Any(x => x.AttackList.Count != 0);
            return value;
        }
    }
}