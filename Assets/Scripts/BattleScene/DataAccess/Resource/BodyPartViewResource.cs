using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;

namespace BattleScene.DataAccess.Resource
{
    public class BodyPartViewResource : IResource<BodyPartViewDto, BodyPartCode>
    {
        public BodyPartViewDto Get(BodyPartCode bodyPartCode)
        {
            throw new System.NotImplementedException();
        }
    }
}