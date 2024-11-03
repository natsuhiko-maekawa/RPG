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
        private readonly ICollection<BuffEntity, (CharacterId, BuffCode)> _buffCollection;

        public BuffService(
            ICollection<BuffEntity, (CharacterId, BuffCode)> buffCollection)
        {
            _buffCollection = buffCollection;
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
        
        public void Register(BattleEventValueObject buff)
        {
            var buffEntityList = buff.TargetIdList
                .Select(CreateBuffEntity)
                .ToList();
            _buffCollection.Add(buffEntityList);

            return;

            BuffEntity CreateBuffEntity(CharacterId characterId)
            {
                var buffEntity = new BuffEntity(
                    characterId: characterId,
                    buffCode: buff.BuffCode,
                    turn: buff.Turn,
                    lifetimeCode: buff.LifetimeCode,
                    rate: buff.Rate
                );
                return buffEntity;
            }
        }

        public void RegisterBattleEvent(IReadOnlyList<BattleEventValueObject> buffList)
        {
            foreach (var buff in buffList) Register(buff);
        }
    }
}