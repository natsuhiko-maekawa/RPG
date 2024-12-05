using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class
        SkillElementUseCase<TSkillElement>
    {
        private readonly ISkillElementService<TSkillElement> _skillElement;
        private readonly BattleLoggerService _battleLogger;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;

        public SkillElementUseCase(
            ISkillElementService<TSkillElement> skillElement,
            BattleLoggerService battleLogger,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository)
        {
            _skillElement = skillElement;
            _battleLogger = battleLogger;
            _battleLogRepository = battleLogRepository;
        }

        public IReadOnlyList<BattleEventValueObject> GetBattleEventList(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TSkillElement> skillElementList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var battleEventList = _skillElement.GenerateBattleEvent(
                actorId: actorId,
                skillCommon: skillCommon,
                primeSkillParameterList: skillElementList,
                targetIdList: targetIdList);
            return battleEventList;
        }

        public void RegisterBattleEvent(IReadOnlyList<BattleEventValueObject> battleEventList)
        {
            _skillElement.RegisterBattleEvent(battleEventList);
            _battleLogger.Log(battleEventList);
        }

        public bool IsExecutedDamage()
        {
            var value = _battleLogRepository.Get()
                .Where(x => x.Turn == _battleLogRepository.Get().Max().Turn)
                .Any(x => x.AttackList.Count != 0);
            return value;
        }
    }
}