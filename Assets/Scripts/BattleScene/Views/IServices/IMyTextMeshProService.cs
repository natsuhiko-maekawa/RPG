using TMPro;

namespace BattleScene.Views.IServices
{
    public interface IMyTextMeshProService
    {
        public void SetTextZeroAlloc(TMP_Text tmpText, string[] message);
    }
}