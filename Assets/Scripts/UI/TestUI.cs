using System.Linq;
using TMPro;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;
using UselessFrame.Net;
using UselessFrame.NewRuntime;
using UselessFrame.NewRuntime.ECS;
using UselessFrame.UIElements;

namespace TestGame
{
    public class TestUI : MonoUI
    {
        [SerializeField]
        private Button connectBtn;

        [SerializeField]
        private TextMeshProUGUI latencyCom;

        [SerializeField]
        private GameObject latencyObj;

        [SerializeField]
        private Image background;

        [SerializeField]
        private GameObject operateRect;

        [SerializeField]
        private Button upBtn;

        [SerializeField]
        private Button downBtn;

        [SerializeField]
        private Button leftBtn;

        [SerializeField]
        private Button rightBtn;

        private World _world;
        private Entity _entity;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            latencyObj.SetActive(false);
            background.enabled = true;
            connectBtn.gameObject.SetActive(true);
            connectBtn.onClick.AddListener(ConnectHandler);
            leftBtn.onClick.AddListener(MoveLeftHandler);
            rightBtn.onClick.AddListener(MoveRightHandler);
            upBtn.onClick.AddListener(MoveUpHandler);
            downBtn.onClick.AddListener(MoveDownHandler);
        }

        private void MoveLeftHandler()
        {
            Move(new Vector2(-1, 0));
        }

        private void MoveRightHandler()
        {
            Move(new Vector2(1, 0));
        }

        private void MoveUpHandler()
        {
            Move(new Vector2(0, 1));
        }

        private void MoveDownHandler()
        {
            Move(new Vector2(0, -1));
        }

        private void Move(Vector2 offset)
        {
            if (_entity == null)
            {
                _entity = _world.Scenes.First().Entities.First();
            }
            MoveMessage message = new MoveMessage()
            {
                Scene = _entity.Scene.Id,
                Entity = _entity.Id,
                DirectionX = offset.x,
                DirectionY = offset.y,
            };
            _world.Trigger(message);
        }

        private void ConnectHandler()
        {
            _world = X.World.Create(WorldSetting.Client(8888));
            _world.SetHelper(new CreateWorldObjectHelper());
            latencyObj.SetActive(true);
            background.enabled = false;
            connectBtn.gameObject.SetActive(false);
            RefreshLentency(0);
            CheckLentency();
        }

        private async void CheckLentency()
        {
            IConnection connection = _world.NetNode as IConnection;
            if (connection != null)
            {
                LatencyResult result = await connection.TestLatency();
                if (CheckOpenState())
                {
                    RefreshLentency(result.DeltaMillTime);
                    await UniTaskExt.Delay(1);
                    CheckLentency();
                }
            }
        }

        private bool CheckOpenState()
        {
            switch (Handle.State.Value)
            {
                case UIState.Open:
                case UIState.OpenBegin:
                case UIState.OpenEnd: return true;

                default: return false;
            }
        }

        private void RefreshLentency(int ms)
        {
            latencyCom.text = $"{ms} ms";
        }
    }
}
