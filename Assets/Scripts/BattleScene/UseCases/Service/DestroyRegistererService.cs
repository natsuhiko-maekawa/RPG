using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class DestroyRegistererService : IPrimeSkillRegistererService<DestroyValueObject>
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly IFactory<BodyPartPropertyValueObject, BodyPartCode> _bodyPartPropertyFactory;
        private readonly IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> _bodyPartRepository;
        
        public void Register(DestroyValueObject destroy)
        {
            var bodyPartEntityList = destroy.ActualTargetIdList
                .Select(CreateBodyPartEntity)
                .ToList();
            _bodyPartRepository.Update(bodyPartEntityList);
            return;
            
            BodyPartEntity CreateBodyPartEntity(CharacterId targetId)
            {
                var bodyPartProperty = _bodyPartPropertyFactory.Create(destroy.BodyPartCode);
                var bodyPartEntity = new BodyPartEntity(
                    characterId: targetId,
                    bodyPartCode: destroy.BodyPartCode,
                    count: bodyPartProperty.Count);
                return bodyPartEntity;
            }
        }

        public void Register(IReadOnlyList<DestroyValueObject> destroyList)
        {
            foreach (var destroy in destroyList) Register(destroy);
        }

        public void RegisterLog(IReadOnlyList<DestroyValueObject> destroyList)
        {
            
        }
    }
}