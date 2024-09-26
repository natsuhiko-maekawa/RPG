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
    public class BuffRegistererService : IPrimeSkillRegistererService<BuffValueObject>
    {
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;

        public BuffRegistererService(
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository)
        {
            _buffRepository = buffRepository;
        }

        public void Register(BuffValueObject buff)
        {
            var buffEntityList = buff.TargetIdList
                .Select(CreateBuffEntity)
                .ToList();
            _buffRepository.Update(buffEntityList);
            
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

        public void Register(IReadOnlyList<BuffValueObject> buffList)
        {
            foreach (var buff in buffList) Register(buff);
        }
    }
}