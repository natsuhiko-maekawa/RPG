using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class BodyPartViewResource : IResource<BodyPartViewDto, BodyPartCode>
    {
        public BodyPartViewDto Get(BodyPartCode bodyPartCode)
        {
            throw new System.NotImplementedException();
        }
    }
}