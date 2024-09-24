using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;

namespace BattleScene.DataAccess.Factory
{
    public class BodyPartPropertyFactory : IFactory<BodyPartPropertyValueObject, BodyPartCode>
    {
        private readonly IResource<BodyPartPropertyDto, BodyPartCode> _bodyPartPropertyResource;

        public BodyPartPropertyFactory(
            IResource<BodyPartPropertyDto, BodyPartCode> bodyPartPropertyResource)
        {
            _bodyPartPropertyResource = bodyPartPropertyResource;
        }

        public BodyPartPropertyValueObject Create(BodyPartCode key)
        {
            var bodyPartPropertyResource = _bodyPartPropertyResource.Get(key);
            var bodyPartProperty = new BodyPartPropertyValueObject(
                BodyPartCode: bodyPartPropertyResource.Key,
                Count: bodyPartPropertyResource.Count);
            return bodyPartProperty;
        }
    }
}