﻿
using UselessFrame.NewRuntime;
using UselessFrame.NewRuntime.ECS;
using UselessFrame.ResourceManager;
using UselessFrame.Runtime;
using UselessFrame.UIElements;
using UselessFrameUnity.Attributes;

namespace TestGame
{
    [CustomModule]
    public class TestWorldModule : ModuleBase
    {
        private World _world;

        protected override void OnInit(object param)
        {
            base.OnInit(param);

            TestUIModule();

            //_world = X.World.Create(WorldSetting.Client(8888));
            //_world.SetHelper(new CreateWorldObjectHelper());
            //TestMove.Instance.world = _world;
        }

        private async void TestUIModule()
        {
            await X.Module.Add(typeof(ResourceModule));
            UIModule uiModule = (UIModule)await X.Module.Add(typeof(UIModule));
            uiModule.OpenUIAsync<TestUI>("Main", 0);
        }
    }
}
