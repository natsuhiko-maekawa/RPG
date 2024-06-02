using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.InterfaceAdapter.DataAccess.IResource;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class MessageFactory : IMessageFactory
    {
        private readonly IBattleSceneScriptableObject _battleSceneScriptableObject;

        public MessageFactory(
            IBattleSceneScriptableObject battleSceneScriptableObject)
        {
            _battleSceneScriptableObject = battleSceneScriptableObject;
        }

        public string GetMessage(MessageCode messageCode)
        {
            return _battleSceneScriptableObject.GetMsgScriptableObject()
                .First(x => x.key == messageCode)
                .message;
        }
    }
}