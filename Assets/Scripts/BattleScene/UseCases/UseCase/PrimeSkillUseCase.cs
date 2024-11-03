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
        private readonly ISkillElementService<TPrimeSkillParameter> _primeSkill;
        private readonly BattleLoggerService _battleLogger;
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;

        public PrimeSkillUseCase(
            ISkillElementService<TPrimeSkillParameter> primeSkill,
            BattleLoggerService battleLogger,
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection)
        {
            _primeSkill = primeSkill;
            _battleLogger = battleLogger;
            _battleLogCollection = battleLogCollection;
        }

        public IReadOnlyList<BattleEventValueObject> GetBattleEventList(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var battleEventList = _primeSkill.GenerateBattleEvent(
                actorId: actorId,
                skillCommon: skillCommon,
                primeSkillParameterList: primeSkillParameterList,
                targetIdList: targetIdList);
            return battleEventList;
        }

        public void RegisterBattleEvent(IReadOnlyList<BattleEventValueObject> battleEventList)
        {
            _primeSkill.RegisterBattleEvent(battleEventList);
            _battleLogger.Log(battleEventList);
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