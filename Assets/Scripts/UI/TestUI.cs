
using UnityEngine;
using UnityEngine.UI;
using UselessFrameUnity;
using UselessFrame.UIElements;
using Cysharp.Threading.Tasks;
using UselessFrame.NewRuntime;
using UselessFrame.NewRuntime.ECS;
using TMPro;
using DG.Tweening;

namespace TestGame
{
    public class TestUI : MonoUI
    {
        [SerializeField]
        private TMP_InputField serverInput;

        [SerializeField]
        private Button connectBtn;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            connectBtn.onClick.AddListener(ConnectHandler);
        }

        private void ConnectHandler()
        {
            World world = X.World.Create(WorldSetting.Client(8888));
            world.SetHelper(new CreateWorldObjectHelper());
        }
    }
}
