using TMPro;

namespace BattleScene.Framework.IServices
{
    public interface IMyTextMeshProService
    {
        public void SetTextZeroAlloc(ref TMP_Text tmpText, string[] message);
    }
}