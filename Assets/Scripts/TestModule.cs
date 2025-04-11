using Cysharp.Threading.Tasks;
using UnityEngine;
using UselessFrame.Runtime;
using UselessFrameUnity.Attributes;

namespace TestGame
{
    [CustomModule]
    public class TestModule : ModuleBase, IUpdater
    {
        protected override void OnInit(object param)
        {
            base.OnInit(param);
            Debug.Log($"Test Module OnInit");
        }

        protected override async UniTask OnStart()
        {
            Debug.Log($"Test Module OnStart");
            await UniTask.CompletedTask;
        }

        protected override async UniTask OnDestroy()
        {
            Debug.Log($"Test Module OnDestroy");
            await UniTask.CompletedTask;
        }

        public void OnUpdate(float detalTime)
        {

        }
    }
}