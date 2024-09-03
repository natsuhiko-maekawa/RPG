using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class BodyPartViewInfoResource : IResource<BodyPartViewInfoDto, BodyPartCode>
    {
        public BodyPartViewInfoDto Get(BodyPartCode bodyPartCode)
        {
            throw new System.NotImplementedException();
        }
    }
}