
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityXFrame.Core.UIElements;
using UselessFrame.NewRuntime;
using UselessFrame.NewRuntime.Fiber;
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

        protected override void OnClose()
        {
            base.OnClose();

            _closeBtn.onClick.RemoveListener(CloseHandler);
            _openOtherBtn.onClick.RemoveListener(OpenOtherHandler);
        }

        private async void OpenOtherHandler()
        {
            Debug.Log("trigger close button");
            IUIHandle handle = G.UI.OpenUIAsync<TestUI>("Test", 1);
            await handle.WaitStateTask(UIState.OpenEnd);
            IUI ui = handle.UI;
            for(int i = 0; i < 100; i++)
            {
                await UniTaskExt.Delay(5);
                Debug.Log($"UI Close Test1");
                ui.Close();
                Debug.Log($"UI Close Test");
                await handle.WaitStateTask(UIState.CloseEnd);
                Debug.Log($"UI Close Test2");
                await UniTaskExt.Delay(5);
                ui.Open();
                await handle.WaitStateTask(UIState.OpenEnd);
            }
            
            //await UniTaskExt.Delay(5);
            //
            //for (int i = 0; i < 50; i++)
            //{
            //    G.UI.CloseUIAsync<TestUI>(i + 1);
            //}

            //G.UI.OpenUIAsync<TestUI>("Test", 1);
            //G.UI.CloseUIAsync<TestUI>(1, false);
            //G.UI.OpenUIAsync<TestUI>("Test", 1);
            //G.UI.OpenUIAsync<TestUI>("Test", 1);
        }

        private void CloseHandler()
        {
            Close();
        }
    }
}
