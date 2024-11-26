using TMPro;

namespace BattleScene.Framework.IService
{
    public interface IMyTextMeshProService
    {
        public void SetTextZeroAlloc(ref TMP_Text tmpText, string[] message);
    }
}