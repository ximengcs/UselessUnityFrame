
using UnityEngine.UI;
using UnityXFrame.Core.UIElements;
using UselessFrame.NewRuntime;
using UselessFrame.UIElements;
using UselessFrameUnity;

namespace TestGame
{
    public class TestUI : UI
    {
        private Image _background;
        private Button _closeBtn;
        private Button _openOtherBtn;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _background = GetCom<UIComFinder>().GetUI<Image>("Background");
            _closeBtn = GetCom<UIComFinder>().GetUI<Button>("CloseBtn");
            _openOtherBtn = GetCom<UIComFinder>().GetUI<Button>("OpenOtherBtn");
            _closeBtn.onClick.AddListener(CloseHandler);
            _openOtherBtn.onClick.AddListener(OpenOtherHandler);

            var color = X.Random.RandHSVColor();
            _background.color = new UnityEngine.Color(color.x / 255f, color.y / 255f, color.z / 255f, 1);
        }

        private void OpenOtherHandler()
        {
            G.UI.OpenUIAsync<TestUI>("Test", 1);
        }

        private void CloseHandler()
        {
            Close();
        }
    }
}
