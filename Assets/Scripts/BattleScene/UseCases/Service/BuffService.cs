using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class BuffService : ISkillElementService<BuffValueObject>
    {
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;

        public BuffService(
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository)
        {
            _buffRepository = buffRepository;
        }

        public IReadOnlyList<BattleEventValueObject> GenerateBattleEvent(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<BuffValueObject> buffParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var buffList = buffParameterList.Select(GetBuff).ToList();
            return buffList;

            BattleEventValueObject GetBuff(BuffValueObject buffParameter)
            {
                return BattleEventValueObject.CreateBuff(
                    actorId: actorId,
                    targetIdList: targetIdList,
                    skillCode: skillCommon.SkillCode,
                    buffCode: buffParameter.BuffCode,
                    rate: buffParameter.Rate,
                    turn: buffParameter.Turn,
                    lifetimeCode: buffParameter.LifetimeCode);
            }
        }

        public void Register(BattleEventValueObject buffEvent)
        {
            foreach (var characterId in buffEvent.TargetIdList)
            {
                var buff = _buffRepository.Get((characterId, buffEvent.BuffCode));
                buff.Set(
                    turn: buffEvent.Turn,
                    rate: buffEvent.Rate,
                    lifetimeCode: buffEvent.LifetimeCode);
            }
        }

        public void RegisterBattleEvent(IReadOnlyList<BattleEventValueObject> buffList)
        {
            foreach (var buff in buffList) Register(buff);
        }
    }
}