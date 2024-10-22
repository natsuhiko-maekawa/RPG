using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class BuffGeneratorService : IPrimeSkillGeneratorService<BuffParameterValueObject>
    {
        private readonly ICollection<BuffEntity, (CharacterId, BuffCode)> _buffCollection;
        private readonly OrderedItemsDomainService _orderedItems;

        public BuffGeneratorService(
            ICollection<BuffEntity, (CharacterId, BuffCode)> buffCollection,
            OrderedItemsDomainService orderedItems)
        {
            _buffCollection = buffCollection;
            _orderedItems = orderedItems;
        }

        public IReadOnlyList<PrimeSkillValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<BuffParameterValueObject> buffParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) MyDebug.LogAssertion("ActorId is null.");

            var buffList = buffParameterList.Select(GetBuff).ToList();
            return buffList;

            PrimeSkillValueObject GetBuff(BuffParameterValueObject buffParameter)
            {
                return PrimeSkillValueObject.CreateBuff(
                    actorId: actorId,
                    targetIdList: targetIdList,
                    skillCode: skillCommon.SkillCode,
                    buffCode: buffParameter.BuffCode,
                    rate: buffParameter.Rate,
                    turn: buffParameter.Turn,
                    lifetimeCode: buffParameter.LifetimeCode);
            }
        }
        
        public void Register(PrimeSkillValueObject buff)
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

        public void Register(IReadOnlyList<PrimeSkillValueObject> buffList)
        {
            foreach (var buff in buffList) Register(buff);
        }
    }
}